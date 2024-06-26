using System.Net;
using System.Text.Json;

namespace Health.Safety.API.Middlewares
{
    /// <summary>
    /// Exceptions and errors handeling middleware
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        #region Properties
        private readonly RequestDelegate Next;
        #endregion

        #region Initialisation
        /// <summary>
        /// Exceptions and errors handeling middleware constructor
        /// </summary>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Invoke method
        /// <param name="context"></param>
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception error)
            {
                await RecordException(context, error);
            }
        }
        #endregion

        #region Private Functions
        private async Task RecordException(HttpContext context, Exception error)
        {
            var apiResponse = new
            {
                Status = HttpStatusCode.InternalServerError,
                Data = (object)null!,
                Success = false,
                Title = error.Message,
                Errors = new { Errors = new List<string> { error.Message, error.StackTrace ?? string.Empty } }
            };

            //Prepare 500 internal serever error for API
            HttpResponse response = context?.Response!;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            string result = JsonSerializer.Serialize(apiResponse);
            await response.WriteAsync(result);
        }
        #endregion
    }
}
