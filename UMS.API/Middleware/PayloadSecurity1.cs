using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace UMS.API.Middleware
{
    public class PayloadSecurity1
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public PayloadSecurity1(RequestDelegate next, IConfiguration config)
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
                string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                if (requestBody.Contains("\"input_data\":"))
                {
                    try
                    {
                        dynamic jsonObject = JsonConvert.DeserializeObject(requestBody);
                        string inputData = jsonObject.input_data;
                        PayloadEncryptDecryptService securityService = new PayloadEncryptDecryptService(_config);
                        string decryptedBody = securityService.DecryptRequest(inputData);
                        httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(decryptedBody));
                        httpContext.Request.ContentLength = decryptedBody.Length;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error decrypting request body: {ex.Message}");
                    }
                }
            }
            await _next(httpContext);
        }
    }
}
