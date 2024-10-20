using Newtonsoft.Json;
using System.Text;

namespace UMS.API.Middleware
{
    public class PayloadSecurity
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public PayloadSecurity(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            List<string> excludedApis = new List<string> {
                "/api/FileManager/InsertUploadFiles",
                "/api/FileManager/DownloadFiles"
            };

            string url = httpContext.Request.Path + httpContext.Request.QueryString;
            if (!excludedApis.Any(x => url.Contains(x)))
            {
                var originalBodyStream = httpContext.Response.Body;
                try
                {
                    using (var responseBody = new MemoryStream())
                    {
                        httpContext.Response.Body = responseBody;
                        await _next(httpContext);
                        responseBody.Seek(0, SeekOrigin.Begin);
                        string responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                        PayloadEncryptDecryptService securityService = new PayloadEncryptDecryptService(_config);
                        string encryptedData = securityService.EncryptResponse(responseContent);
                        object finalResponse = new { output_data = encryptedData };
                        string jsonResponse = JsonConvert.SerializeObject(finalResponse);
                        byte[] bytes = Encoding.UTF8.GetBytes(jsonResponse);
                        await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                finally
                {
                    httpContext.Response.Body = originalBodyStream;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }

    }
}
