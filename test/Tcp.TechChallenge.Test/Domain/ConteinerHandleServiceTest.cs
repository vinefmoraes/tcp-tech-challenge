namespace Tcp.TechChallenge.Test.Domain
{
    using FluentAssertions;
    using FluentValidation.Results;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Domain.Conversion.Support;
    using Tcp.TechChallenge.Domain.Models;
    using Tcp.TechChallenge.Domain.Services;
    using Tcp.TechChallenge.Domain.Services.Impl;
    using Tcp.TechChallenge.Domain.Validations.Support;
    using Tcp.TechChallenge.Infra.Models;
    using Tcp.TechChallenge.Infra.Repositories;

    [TestClass]
    public class ConteinerHandleServiceTest
    {
        private readonly IConteinerHandleService service;
        private readonly Mock<IConverterService> converterServiceMock = new Mock<IConverterService>();
        private readonly Mock<IValidationService> validationServiceMock = new Mock<IValidationService>();
        private readonly Mock<IConteinerRepository> repositoryMock = new Mock<IConteinerRepository>();

        public ConteinerHandleServiceTest()
        {
            service = new ConteinerHandleService(
                converterServiceMock.Object, 
                validationServiceMock.Object,
                repositoryMock.Object);
        }

        [TestMethod]
        public void FindAll_Success_ReturnSucess() {

            var expected = new[] { new ConteinerRequest() };
            repositoryMock.Setup(x => x.FindAll()).Returns(new[] { new Conteiner() });
            converterServiceMock.Setup(x => 
                    x.Convert<IList<Conteiner>, IList<ConteinerRequest>>(It.IsAny<IList<Conteiner>>()))
            .Returns(expected);

            var result = service.FindAll();
            result.Should().NotBeEmpty().And.BeEquivalentTo(expected);
        }

        [TestMethod]
        public void FindByIdentifier_Success_ReturnSucess() {

            var expected = new ConteinerRequest();
            repositoryMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(new Conteiner());
            converterServiceMock.Setup(x =>
                    x.TryConvert(It.IsAny<Conteiner>(), out expected)).Verifiable();

            var result = service.FindByIdentifier("");
            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse <ConteinerRequest>.Success(expected));
        
        }

        [TestMethod]
        public void FindByIdentifier_NotFound_ReturnError() {
            repositoryMock.Setup(x => x.FindById(It.IsAny<string>())).Returns((Conteiner)null);
            var result = service.FindByIdentifier("");
            result.Should()
                .BeEquivalentTo(ObjectResponse<ConteinerRequest>.FailWithError("Conteiner não encontrado"));
        }

        [TestMethod]
        public async Task Insert_Success_ReturnSucess() {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            repositoryMock.Setup(x => x.FindById(It.IsAny<string>())).Returns((Conteiner)null);
            
            Conteiner expected = new Conteiner();
            converterServiceMock.Setup(x =>
                    x.TryConvert(It.IsAny<ConteinerRequest>(), out expected)).Verifiable();
            var result = await service.Insert(new ConteinerRequest());

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<int>.Success());
            repositoryMock.Verify(x => x.Save(expected), Times.Once);
        }

        [TestMethod]
        public async Task Insert_ValidationFails_ReturnError() {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                    .Returns(new FluentValidation.Results.ValidationResult(
                        new[] { new ValidationFailure("Numero", "Numero nao pode ser nulo")}    
                    ));
            var result = await service.Insert(new ConteinerRequest());

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<int>.Fail(new List<ValidationFailure> { new ValidationFailure("Numero", "Numero nao pode ser nulo") }));
        }

        [TestMethod]
        public async Task Insert_ConteinerAlreadyExists_ReturnError() {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                .Returns(new ValidationResult());

            repositoryMock.Setup(x => x.FindById(It.IsAny<string>())).Returns(new Conteiner());

            var result = await service.Insert(new ConteinerRequest());

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<int>.FailWithError("Conteiner já existe"));
            repositoryMock.Verify(x => x.Save(It.IsAny<Conteiner>()), Times.Never);
            converterServiceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task Edit_Success_ReturnSucess()
        {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                .Returns(new FluentValidation.Results.ValidationResult());

            Conteiner expected = new Conteiner();
            converterServiceMock.Setup(x =>
                    x.TryConvert(It.IsAny<ConteinerRequest>(), out expected)).Verifiable();
            var result = await service.Edit("123", new ConteinerRequest() { Number = "123"});

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<bool>.Success());
            repositoryMock.Verify(x => x.Edit(expected), Times.Once);
        }

        [TestMethod]
        public async Task Edit_ValidationFails_ReturnError()
        {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                    .Returns(new ValidationResult(
                        new[] { new ValidationFailure("Numero", "Numero nao pode ser nulo") }
                    ));
            var result = await service.Edit("",new ConteinerRequest());

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<bool>.Fail(new List<ValidationFailure> { new ValidationFailure("Numero", "Numero nao pode ser nulo") }));
        }

        [TestMethod]
        public async Task Edit_ConteinerNumberDifferent_ReturnError()
        {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
                .Returns(new ValidationResult());

            var result = await service.Edit("123", new ConteinerRequest() { Number = "4321" });

            result.Should().NotBeNull().And.BeEquivalentTo(ObjectResponse<bool>.FailWithError("Conteiner nao encontrado"));
            repositoryMock.Verify(x => x.Edit(It.IsAny<Conteiner>()), Times.Never);
            converterServiceMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task Edit_ThrowsError_ReturnInternalError()
        {
            validationServiceMock.Setup(x => x.Validate(It.IsAny<ConteinerRequest>()))
               .Throws<Exception>();

            var result = await service.Edit("123", new ConteinerRequest() { Number = "4321" });

            result.Should().BeEquivalentTo(ObjectResponse<bool>.InternalError());
        }

        [TestMethod]
        public async Task Delete_Success_ReturnSucess() {
            repositoryMock.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(true);
            var result = await service.Delete("");
            result.Should().BeEquivalentTo(ObjectResponse<bool>.Success(true));
        }

        [TestMethod]
        public async Task Delete_ErrorOnRepository_ReturnError()
        {
            repositoryMock.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(false);
            var result = await service.Delete("");
            result.Should().BeEquivalentTo(ObjectResponse<bool>.FailWithError("Conteiner nao pode ser excluido"));
        }

        [TestMethod]
        public async Task Delete_ThrowsError_ReturnInternalError()
        {
            repositoryMock.Setup(x => x.Delete(It.IsAny<string>()))
               .Throws<Exception>();

            var result = await service.Delete("");
            result.Should().BeEquivalentTo(ObjectResponse<bool>.InternalError());
        }
    }
}
