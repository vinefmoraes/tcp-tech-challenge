using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tcp.TechChallenge.Domain.Conversion.Support;
using Tcp.TechChallenge.Domain.Models;
using Tcp.TechChallenge.Domain.Validations;
using Tcp.TechChallenge.Domain.Validations.Support;
using Tcp.TechChallenge.Infra.Repositories;

namespace Tcp.TechChallenge.Domain.Services.Impl
{
    public class ConteinerHandleService : IConteinerHandleService
    {
        private readonly IConverterService _converterService;
        private readonly IValidationService _validationService;
        private readonly IConteinerRepository _conteinerRepository;

        public ConteinerHandleService(
            IConverterService converterService,
            IValidationService validationService,
            IConteinerRepository conteinerRepository)
        {
            _converterService = converterService;
            _validationService = validationService;
            _conteinerRepository = conteinerRepository;
        }

        public IList<ConteinerRequest> FindAll()
        {
            var result = _conteinerRepository.FindAll();
            _converterService.Convert(result, out IList<ConteinerRequest> results);
            return results;
        }

        public ObjectResponse<ConteinerRequest> FindByIdentifier(string conteinerIdentifier)
        {
            var response = _conteinerRepository.FindById(conteinerIdentifier);
            if (response == null)
            {
                return ObjectResponse<ConteinerRequest>.FailWithError("Conteiner não encontrado");
            }

            _converterService.Convert(response, out ConteinerRequest result);
            return ObjectResponse<ConteinerRequest>.Success(result);
        }
        

        public async Task<ObjectResponse<int>> Insert(ConteinerRequest conteiner)
        {
            var validationResult = _validationService.Validate(conteiner);
            if (validationResult.IsValid)
            {
                if (_conteinerRepository.FindById(conteiner.Number) != null)
                    return ObjectResponse<int>.FailWithError("Conteiner já existe");

                _converterService.Convert(conteiner, out Infra.Models.Conteiner conteinerParams);
                await _conteinerRepository.Save(conteinerParams);
                return ObjectResponse<int>.Success();
            }

            return ObjectResponse<int>.Fail(validationResult.Errors);
        }

        public async Task<ObjectResponse<bool>> Edit(string conteinerIdentifier, ConteinerRequest conteiner)
        {
            try
            {
                var validationResult = _validationService.Validate(conteiner);
                if (validationResult.IsValid)
                {
                    if (conteiner.Number != conteinerIdentifier)
                        return ObjectResponse<bool>.FailWithError("Conteiner nao encontrado");

                    _converterService.Convert(conteiner, out Infra.Models.Conteiner conteinerParams);
                    await _conteinerRepository.Edit(conteinerParams);
                    return ObjectResponse<bool>.Success();
                }

                return ObjectResponse<bool>.Fail(validationResult.Errors);
            } catch
            {
                return ObjectResponse<bool>.InternalError();
            }
            
        }

        public async Task<ObjectResponse<bool>> Delete(string conteinerIdentifier)
        {
            try
            {
                var result = await _conteinerRepository.Delete(conteinerIdentifier);
                return ObjectResponse<bool>.Success(result);
            }
            catch
            {
                return ObjectResponse<bool>.InternalError();
            }
        }
    }
}
