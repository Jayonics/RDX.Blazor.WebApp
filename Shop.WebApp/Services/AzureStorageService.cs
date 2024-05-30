using Microsoft.AspNetCore.Components.Forms;
using Shop.Models.Dtos;
using Shop.WebApp.Services.Contracts;

namespace Shop.WebApp.Services
{
    public class AzureStorageService : IAzureStorageService
    {

        private readonly HttpClient _httpClient;
        readonly ILogger<AzureStorageService> _logger;

        public AzureStorageService(HttpClient httpClient, ILogger<AzureStorageService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public class FormFileFromBrowserFile : IFormFile
        {
            private readonly IBrowserFile _file;
            public FormFileFromBrowserFile(IBrowserFile file)
            {
                _file = file;
                Name = file.Name;
                FileName = file.Name;
                Length = file.Size;
                LastModified = file.LastModified.UtcDateTime;
                Headers = new HeaderDictionary();
                ContentType = file.ContentType;
            }

            public string ContentType { get; }
            public string ContentDisposition => throw new NotImplementedException();
            public IHeaderDictionary Headers { get; }
            public long Length { get; }
            public string Name { get; }
            public string FileName { get; }
            public DateTimeOffset LastModified { get; }
            public Stream OpenReadStream() => _file.OpenReadStream(52428800); // 50MB
            public Stream OpenReadStream(long maxAllowedSize) => _file.OpenReadStream(maxAllowedSize);
            public void CopyTo(Stream target) => _file.OpenReadStream().CopyTo(target);
            public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) => await _file.OpenReadStream().CopyToAsync(target);
        }

        public async Task<BlobResponseDto> UploadAsync(IBrowserFile file)
        {
            // Convert IBrowserFile to IFormFile
            IFormFile formFile = new FormFileFromBrowserFile(file);

            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(formFile.OpenReadStream()), "file", formFile.FileName);

            var response = await _httpClient.PostAsync("api/Storage", formData);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlobResponseDto>();
            }

            return null;
        }

        public async Task<BlobResponseDto> UploadAsync(IFormFile file)
        {
            var formData = new MultipartFormDataContent();
            formData.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

            var response = await _httpClient.PostAsync("api/Storage", formData);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlobResponseDto>();
            }

            return null;
        }

        public async Task<BlobDto> DownloadAsync(string blobFilename)
        {
            var response = await _httpClient.GetAsync($"api/Storage/{blobFilename}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlobDto>();
            }

            return null;
        }

        public async Task<BlobResponseDto> DeleteAsync(string blobFilename)
        {
            var response = await _httpClient.DeleteAsync($"api/Storage/{blobFilename}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlobResponseDto>();
            }

            return null;
        }

        public async Task<List<BlobDto>> ListAsync()
        {
            var response = await _httpClient.GetAsync("api/Storage");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BlobDto>>();
            }

            return null;
        }
    }
}
