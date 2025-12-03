using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Entities;
using Newtonsoft.Json;
using static Application.Common.Services.IServerFileManager;

namespace Web.Services;

public class ServerFileManager : IServerFileManager
{
    private IConfiguration _configuration;
    private IAuthenticationService _authenticationService;
    private readonly ILogger<ServerFileManager> _logger;
    //private readonly IAuditRepository _auditRepository;
    private readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".webp", "image/webp" },
        { ".jfif", "image/jpeg" },
        { ".tiff", "image/tiff" },
        { ".tif", "image/tiff" }
    };

    public string GetMimeType(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        if (extension != null && MimeTypes.TryGetValue(extension, out string mimeType))
        {
            return mimeType;
        }
        return "application/octet-stream"; // Default MIME type
    }

    private string GetFileExtension(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("File name cannot be null or empty.");
        }

        int lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex == -1 || lastDotIndex == fileName.Length - 1)
        {
            return string.Empty; // No extension found or dot is the last character
        }

        string extension = fileName.Substring(lastDotIndex + 1);
        return extension;
    }

    private bool CheckIfIsImage(string fileName)
    {
        var fileType = GetFileExtension(fileName);
        return fileType.ToLower() == "jpeg"
            || fileType.ToLower() == "png"
            || fileType.ToLower() == "jpg"
            || fileType.ToLower() == "gif"
            || fileType.ToLower() == "webp"
            || fileType.ToLower() == "jfif"
            || fileType.ToLower() == "tiff"
            || fileType.ToLower() == "tif";
    }
    private bool CheckIfIsPdf(string fileName)
    {
        var fileType = GetFileExtension(fileName);
        return fileType.ToLower() == "pdf";
    }

    private class DmsResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FileId { get; set; }
    }

    public ServerFileManager(IConfiguration configuration,
        IAuthenticationService authenticationService,
        ILogger<ServerFileManager> logger)
    {
        _configuration = configuration;
        _authenticationService = authenticationService;
        _logger = logger;
        //_auditRepository = auditRepository;
    }

    //private async Task SaveAuditLog(string referenceId, string message)
    //{
    //    var user = _authenticationService.GetAuthorizedUser();
    //    var ip = _authenticationService.GetUserIp();
    //    var auditLog = new AuditLog(user.Name, ip, AuditLog.ReferenceTypes.COURT_LETTER, referenceId, AuditLog.ReferenceTypes.COURT_LETTER, message);
    //    await _auditRepository.Save(auditLog);
    //}

    //public bool IsPdfFile(byte[] fileBytes)
    //{
    //    if (fileBytes == null || fileBytes.Length < 4)
    //    {
    //        return false;
    //    }

    //    // PDF files start with "%PDF-" (25 50 44 46 in ASCII)
    //    byte[] pdfHeader = new byte[] { 0x25, 0x50, 0x44, 0x46 };

    //    for (int i = 0; i < pdfHeader.Length; i++)
    //    {
    //        if (fileBytes[i] != pdfHeader[i])
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    public async Task<FileInfoEntity> PreviewFile(string fileId)
    {
        string baseUrl = _configuration["Dms:Url"];
        string apiKey = _configuration["Dms:Api_Key"];
        var client = new HttpClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/files/preview/{fileId}");
        httpRequest.Headers.Add("x-api-key", apiKey);
        httpRequest.Headers.Add("user-name", _authenticationService.GetUserName());
        httpRequest.Headers.Add("ip-address", _authenticationService.GetUserIp());
        var response = await client.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        if (response.IsSuccessStatusCode)
        {
            response.Headers.TryGetValues("filename", out IEnumerable<string> headers);
            var contentType = response.Content.Headers.ContentType?.MediaType;
            // Read and parse the JSON content from the response body
            string jsonContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON content into an object
            var fileInfo = JsonConvert.DeserializeObject<FileInfoEntity>(jsonContent);

            if (fileInfo.Filename == null)
                throw new Exception("The system couldnot load preview for this file!");

            fileInfo.IsImage = CheckIfIsImage(fileInfo.Filename);
            fileInfo.IsPdf = CheckIfIsPdf(fileInfo.Filename);

            return fileInfo;
        }
        else
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new DmsException(
                "Failed with status code: " + response.StatusCode,
                null,
                "DMS_SERVICE",
                100,
                content);
        }
    }

    public async Task<bool> Delete(string fileId)
    {
        try
        {
            string baseUrl = _configuration["Dms:Url"];
            string apiKey = _configuration["Dms:Api_Key"];
            var client = new HttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{baseUrl}/api/files/{fileId}");
            httpRequest.Headers.Add("x-api-key", apiKey);
            httpRequest.Headers.Add("user-name", _authenticationService.GetUserName());
            httpRequest.Headers.Add("ip-address", _authenticationService.GetUserIp());
            var response = await client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Delete Faild: " + ex.Message);
            throw new DmsException(
                ex.Message,
                null,
                "DMS_SERVICE",
                100,
                "Delete Failed");
        }
    }

    public async Task<FileInfoEntity> Download(string fileId)
    {
        try
        {
            string baseUrl = _configuration["Dms:Url"];
            string apiKey = _configuration["Dms:Api_Key"];
            var client = new HttpClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/files/download/{fileId}");
            httpRequest.Headers.Add("x-api-key", apiKey);
            httpRequest.Headers.Add("user-name", _authenticationService.GetUserName());
            httpRequest.Headers.Add("ip-address", _authenticationService.GetUserIp());
            var response = await client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                response.Headers.TryGetValues("x-file-name", out IEnumerable<string> headers);
                string fileName = headers.FirstOrDefault();
                if (fileName == null)
                    throw new Exception("The header (x-file-name) is not received!");

                fileName = Uri.UnescapeDataString(fileName);
                byte[] filebytes = await response.Content.ReadAsByteArrayAsync();

                bool isImage = CheckIfIsImage(fileName);
                bool isPdf = CheckIfIsPdf(fileName);
                string ext = GetFileExtension(fileName);
                ext = ext.ToLower();


                if (isImage)
                {
                    var base64String = Convert.ToBase64String(filebytes);
                    string src = $"data:image/jpeg;base64,{base64String}";
                    return new FileInfoEntity
                    {
                        _id = fileId,
                        Filename = fileName,
                        Filebytes = filebytes,
                        Src = src,
                        IsImage = true,
                        Base64String = base64String,
                        IsTiff = ext == "tif" || ext == "tiff" || ext == "tfif",
                    };
                }
                return new FileInfoEntity
                {
                    Filename = fileName,
                    Filebytes = filebytes,
                    IsPdf = isPdf
                };
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new DmsException(
                    "Failed with status code: " + response.StatusCode,
                    null,
                    "DMS_SERVICE",
                    100,
                    content);
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Download Failed: " + ex.Message);
            throw new DmsException(
                    ex.Message,
                    ex,
                    "DMS_SERVICE",
                    100,
                    "No Body Content");
        }
    }

    public async Task<Tuple<bool, string, string>> SaveCourtLetterToDms(IFormFile file, string team, string letterNumber, string letterDate, string courtName, string receivedDate, string courtletterId)
    {
        return await SaveToDms(file, new List<Tuple<string, string>>
        {
            new Tuple<string, string>("folder", team),
            new Tuple<string, string>("court", courtName),
            new Tuple<string, string>("letterNumber", letterNumber),
            new Tuple<string, string>("letterDate", letterDate),
            new Tuple<string, string>("receivedDate", receivedDate),
            new Tuple<string, string>("courtLetterId", courtletterId),
            new Tuple<string, string>("category", Categories.COURT_LETTER),
        });
    }

    public async Task<Tuple<bool, string, string>> SaveCustomerFormToDms(IFormFile file, string fileName, string customerFormId, string lostMsisdn, string shopName)
    {
        return await SaveToDms(file, new List<Tuple<string, string>>
        {
            new Tuple<string, string>("customerFormId", customerFormId),
            new Tuple<string, string>("lostMsisdn", lostMsisdn),
            new Tuple<string, string>("shopName", shopName),
            new Tuple<string, string>("folder", "POS"),
            new Tuple<string, string>("category", Categories.CUSTOMER_FORM),
        }, fileName);
    }



    private async Task<Tuple<bool, string, string>> SaveToDms(IFormFile file, List<Tuple<string, string>> nameValues, string fileName = null, bool isCorrespondence = false)
    {
        try
        {
            string baseUrl = _configuration["Dms:Url"];
            string apiKey = _configuration["Dms:Api_Key"];
            string fullUrl;
            var client = new HttpClient();
            if (!isCorrespondence)
                fullUrl = $"{baseUrl}/api/files";
            else
                fullUrl = $"{baseUrl}/api/files";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, fullUrl);

            httpRequest.Headers.Add("x-api-key", apiKey);
            httpRequest.Headers.Add("user-name", _authenticationService.GetUserName());
            httpRequest.Headers.Add("ip-address", _authenticationService.GetUserIp());
            var content = new MultipartFormDataContent();

            // Convert IFormFile to StreamContent
            using var fileStream = file.OpenReadStream();
            var streamContent = new StreamContent(fileStream);
            content.Add(streamContent, "file", file.FileName);

            content.Add(new StringContent(_authenticationService.GetUserName()), "createdBy");
            foreach (var nameValue in nameValues)
            {
                content.Add(new StringContent(nameValue.Item2), nameValue.Item1);
            }

            httpRequest.Content = content;

            // Send the request
            var response = await client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            // Check response status
            //if (!response.IsSuccessStatusCode)
            //{
            //    return new Tuple<bool, string, string>(false, "An error occured!", null);
            //}

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DmsResponse>(json);

            if (result.Status == "success")
            {
                return new Tuple<bool, string, string>(true, "File uploaded successfully!", result.FileId);
            }

            return new Tuple<bool, string, string>(false, result.Message, null);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            _logger.LogCritical(ex, "Upload Failed: " + ex.Message);
            throw new DmsException(
                    ex.Message,
                    ex,
                    "DMS_SERVICE",
                    100,
                    "Upload Failed");
        }
    }

    public async Task<Tuple<bool, string, string>> SaveNSFile(IFormFile file, string fileName, string requestId, string team)
    {
        var list = new List<Tuple<string, string>> {
            new Tuple<string, string>("customerFormId", requestId),
            new Tuple<string, string>("category", Categories.CUSTOMER_FORM),
            new Tuple<string, string>("folder", team),
        };



        return await SaveToDms(file, list, fileName, isCorrespondence: false);
    }
}