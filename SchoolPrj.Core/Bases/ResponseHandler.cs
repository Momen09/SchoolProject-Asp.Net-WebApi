using Microsoft.Extensions.Localization;
using SchoolPrj.Core.Resources;

namespace SchoolPrj.Core.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public ResponseHandler(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Meta = Meta,
                succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Success],
            };
        }
        public Response<T> Deleted<T>(string message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                succeeded = true,
                Message = message == null ? _stringLocalizer[SharedResourcesKeys.Deleted] : message,

            };
        }
        public Response<T> Updated<T>(T entity)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Updated],
            };
        }
        //public Response<T> Faild<T>(string message, List<string> errors = null)
        //{
        //    return new Response<T>()
        //    {
        //        StatusCode = System.Net.HttpStatusCode.BadRequest,
        //        succeeded = false,
        //        Message = message,
        //        Errors = errors
        //    };
        //}
        public Response<T> NotFound<T>(string message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                succeeded = false,
                Message = message == null ? "Not Found" : message,
            };
        }
        public Response<T> Created<T>(T entity, object Meta = null)
        {
            return new Response<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                succeeded = true,
                Message = _stringLocalizer[SharedResourcesKeys.Created],
                Meta = Meta,
            };
        }
        public Response<T> Unauthorized<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                succeeded = false,
                Message = "Unauthorized",
            };
        }
        public Response<T> BadRequest<T>(string message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                succeeded = false,
                Message = message,
            };
        }
        public Response<T> UnprocessableEntity<T>(string message)
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                succeeded = false,
                Message = message == null ? "UnprocessableEntity" : message,
            };
        }
    }
}
