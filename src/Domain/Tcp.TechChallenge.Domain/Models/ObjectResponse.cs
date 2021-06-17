namespace Tcp.TechChallenge.Domain.Models
{
    using FluentValidation.Results;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class ObjectResponse<T>
    {
        public bool Successed { get; set; }
        public T Result { get; set; }
        public IList<Error> Errors { get; set; }

        public ObjectResponse(bool success)
        {
            this.Successed = success;
        }

        public static ObjectResponse<T> Fail(List<ValidationFailure> validationFailure)
        {
            return new ObjectResponse<T>(false)
            {
                Errors = validationFailure.Select(x => new Error(x.ErrorMessage)).ToList()
            };
        }

        public static ObjectResponse<T> FailWithError(string error)
        {
            return new ObjectResponse<T>(false)
            {
                Errors = new[] { new Error(error) }
            };
        }

        public static ObjectResponse<T> InternalError() 
            => new ObjectResponse<T>(false) { Errors = new[] { new Error("Erro interno") } };
        
        public static ObjectResponse<T> Success()
        {
            return new ObjectResponse<T>(true);
        }        

        public static ObjectResponse<T> Success(T result)
        {
            return new ObjectResponse<T>(true)
            {
                Result = result
            };
        }

        public void Deconstruct(out bool success, out T result, out IList<Error> errors)
            => (success, result, errors) = (Successed, Result, Errors);
    }

    public class Error
    {
        public Error(string description)
        {
            this.Description = description;
        }
        public string Description { get; set; }
    }
}
