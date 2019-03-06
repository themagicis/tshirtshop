using System.Collections.Generic;
using System.Linq;

namespace TShirtShop.Services.Results
{
    public class ServiceResult
    {
        protected List<ResultError> errors = new List<ResultError>();

        public bool Succeeded { get; protected set; }

        public IEnumerable<ResultError> Errors => errors;

        public static ServiceResult Success()
        {
            var result = new ServiceResult { Succeeded = true };
            return result;
        }

        public static ServiceResult Failed(params ResultError[] errors)
        {
            var result = new ServiceResult { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }

        public static ServiceResult Failed(IEnumerable<ResultError> errors)
        {
            return Failed(errors.ToArray());
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; protected set; }

        public static ServiceResult<T> Success(T data)
        {
            var result = new ServiceResult<T> { Succeeded = true, Data = data };
            return result;
        }

        public static new ServiceResult<T> Failed(params ResultError[] errors)
        {
            var result = new ServiceResult<T> { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }

        public static new ServiceResult<T> Failed(IEnumerable<ResultError> errors)
        {
            return Failed(errors.ToArray());
        }
    }
}
