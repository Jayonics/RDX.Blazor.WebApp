using Azure.Storage.Blobs.Models;
using Shop.Models.Dtos;

namespace Shop.API.Repositories.Contracts
{
    public interface IStorageRepository
    {
        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> UploadAsync(IFormFile file);

        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> UploadForceAsync(IFormFile file);

        /// <summary>
        /// This method uploads a file submitted as a byte array with the request
        /// </summary>
        /// <param name="data">File for upload</param>
        /// <param name="fileName">Filename</param>
        Task<BlobContentInfo> UploadAsync(string fileName, byte[] data);

        /// <summary>
        /// This method downloads a file with the specified filename
        /// </summary>
        /// <param name="blobFilename">Filename</param>
        /// <returns>Blob</returns>
        Task<BlobDto> DownloadAsync(string blobFilename);

        /// <summary>
        /// This method deleted a file with the specified filename
        /// </summary>
        /// <param name="blobFilename">Filename</param>
        /// <returns>Blob with status</returns>
        Task<BlobResponseDto> DeleteAsync(string blobFilename);

        /// <summary>
        /// This method returns a list of all files located in the container
        /// </summary>
        /// <returns>Blobs in a list</returns>
        Task<List<BlobDto>> ListAsync();

        /// <summary>
        /// This method returns the properties of a file with the specified filename
        /// </summary>
        /// <param name="blobFilename">Filename</param>
        /// <returns>BlobDto</returns>
        Task<BlobDto> GetAsync(string blobFilename);

        /// <summary>
        /// This method exposes all properties of a file with the specified filename
        /// </summary>
        /// <param name="blobFilename">Filename</param>
        /// <returns>Property value</returns>
        Task<BlobProperties> GetPropertiesAsync(string blobFilename);
    }
}
