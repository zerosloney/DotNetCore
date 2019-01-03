using DotNetCore.Objects;
using DotNetCore.Objects.Results;
using FluentValidation;
using System.Linq;

namespace DotNetCore.Validation
{
    public abstract class Validator<T> : AbstractValidator<T>
    {
        protected Validator()
        {
        }

        protected Validator(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        private string ErrorMessage { get; set; }

        public IResult<string> Valid(T instance)
        {
            if (Equals(instance, default(T)))
            {
                return new ErrorResult<string>(ErrorMessage);
            }

            var result = Validate(instance);

            if (result.IsValid)
            {
                return new SuccessResult<string>(string.Empty);
            }

            ErrorMessage = ErrorMessage ?? string.Join(" ", result.Errors.Select(x => x.ErrorMessage));

            return new ErrorResult<string>(ErrorMessage);
        }
    }
}
