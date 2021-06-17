namespace Tcp.TechChallenge.Test.App
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Tcp.TechChallenge.Api.Controllers;
    using Tcp.TechChallenge.Domain.Models;
    using Tcp.TechChallenge.Domain.Services;
    using Tcp.TechChallenge.Infra.Models;

    [TestClass]
    public class ConteinerControllerTest
    {

        private readonly ConteinerController controller;
        private readonly Mock<IConteinerHandleService> serviceMock = new Mock<IConteinerHandleService>();

        public ConteinerControllerTest()
        {
            controller = new ConteinerController();
        }

        [TestMethod]
        public void FindAll_Returns_All()
        {
            serviceMock.Setup(x => x.FindAll()).Returns(new[] {
                new ConteinerRequest(),
                new ConteinerRequest(),
                new ConteinerRequest()
            });
            var result = controller.GetAllConteiners(serviceMock.Object);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            CollectionAssert.AllItemsAreNotNull((result as OkObjectResult).Value as System.Collections.ICollection);
        }

        [TestMethod]
        public async Task Create_InvalidRequest_ReturnsBadRequest()
        {
            serviceMock.Setup(x => x.Insert(It.IsAny<ConteinerRequest>()))
                .ReturnsAsync(ObjectResponse<int>.FailWithError("Campos invalidos"));

            var result = await controller.Create(serviceMock.Object, new ConteinerRequest());
            
            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult).Value.Should()
                .BeEquivalentTo(new[] { new Error("Campos invalidos") });
        }

        [TestMethod]
        public async Task Create_ValidRequest_ReturnsSuccess()
        {
            var expected = new ConteinerRequest();
            serviceMock.Setup(x => x.Insert(It.IsAny<ConteinerRequest>()))
                .ReturnsAsync(ObjectResponse<int>.Success());

            var result = await controller.Create(serviceMock.Object, expected);

            result.Should().BeOfType<CreatedResult>();
            (result as CreatedResult).Value.Should()
                .Be(expected);
        }

        [TestMethod]
        public async Task Edit_Fails_ReturnsError()
        {
            serviceMock.Setup(x => x.Edit(It.IsAny<string>(), It.IsAny<ConteinerRequest>()))
                .ReturnsAsync(ObjectResponse<bool>.FailWithError("Conteiner nao existe"));

            var result = await controller.Edit(serviceMock.Object,"", new ConteinerRequest());

            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult).Value.Should()
                .BeEquivalentTo(new[] { new Error("Conteiner nao existe") });
        }

        [TestMethod]
        public async Task Edit_Ok_ReturnsSuccess()
        {
            serviceMock.Setup(x => x.Edit(It.IsAny<string>(), It.IsAny<ConteinerRequest>()))
                .ReturnsAsync(ObjectResponse<bool>.Success(true));

            var result = await controller.Edit(serviceMock.Object, "", new ConteinerRequest());
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task Delete_Fails_ReturnsError()
        {
            serviceMock.Setup(x => x.Delete(It.IsAny<string>()))
                .ReturnsAsync(ObjectResponse<bool>.FailWithError("Conteiner nao existe"));

            var result = await controller.Delete(serviceMock.Object, "");

            result.Should().BeOfType<BadRequestObjectResult>();
            (result as BadRequestObjectResult).Value.Should()
                .BeEquivalentTo(new[] { new Error("Conteiner nao existe") });
        }

        [TestMethod]
        public async Task Delete_Ok_ReturnsSuccess()
        {
            serviceMock.Setup(x => x.Delete(It.IsAny<string>()))
                .ReturnsAsync(ObjectResponse<bool>.Success(true));

            var result = await controller.Delete(serviceMock.Object, "");
            result.Should().BeOfType<AcceptedResult>();
        }
    }
}
