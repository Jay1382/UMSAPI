using Newtonsoft.Json;
using System.Text;

namespace UMS.API.Middleware
{
    public class SecurePayload
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly PayloadEncryptDecryptService _securityService;

        public SecurePayload(RequestDelegate next, IConfiguration config, PayloadEncryptDecryptService securityService)
        {
            _next = next;
            _config = config;
            _securityService = securityService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (Convert.ToBoolean(_config.GetSection("SecurePayload")["action"]) == true)
            {
                List<string> excludedApis = new List<string> {
                "/api/FileManager/InsertUploadFiles",
                "/api/FileManager/DownloadFiles"
                };

                string url = httpContext.Request.Path + httpContext.Request.QueryString;
                if (!excludedApis.Any(x => url.Contains(x)))
                {
                    string requestBody = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
                    var originalBodyStream = httpContext.Response.Body;
                    if (requestBody.Contains("\"input_data\":"))
                    {
                        try
                        {
                            dynamic jsonObject = JsonConvert.DeserializeObject(requestBody);
                            string inputData = jsonObject.input_data;
                            string decryptedBody = _securityService.DecryptRequest(inputData);
                            //SecurityService securityService = new SecurityService(_config);
                            //string decryptedBody = securityService.DecryptRequest(inputData);
                            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(decryptedBody));
                            httpContext.Request.ContentLength = decryptedBody.Length;

                            using (var responseBody = new MemoryStream())
                            {
                                httpContext.Response.Body = responseBody;
                                await _next(httpContext);
                                responseBody.Seek(0, SeekOrigin.Begin);
                                string responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                                string encryptedData = _securityService.EncryptResponse(responseContent);
                                object finalResponse = new { output_data = encryptedData };
                                string jsonResponse = JsonConvert.SerializeObject(finalResponse);
                                byte[] bytes = Encoding.UTF8.GetBytes(jsonResponse);
                                await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error decrypting request body: {ex.Message}");
                        }
                        finally
                        {
                            httpContext.Response.Body = originalBodyStream;
                        }
                    }
                    else
                    {
                        using (var responseBody = new MemoryStream())
                        {
                            httpContext.Response.Body = responseBody;
                            await _next(httpContext);
                            responseBody.Seek(0, SeekOrigin.Begin);
                            string responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                            string encryptedData = _securityService.EncryptResponse(responseContent);
                            //SecurityService securityService = new SecurityService(_config);
                            //string encryptedData = securityService.EncryptResponse(responseContent);
                            object finalResponse = new { output_data = encryptedData };
                            string jsonResponse = JsonConvert.SerializeObject(finalResponse);
                            byte[] bytes = Encoding.UTF8.GetBytes(jsonResponse);
                            await originalBodyStream.WriteAsync(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
