using Microsoft.AspNetCore.Mvc;
using Shop.Models.Dtos;
using Shop.API.Repositories.Contracts;


namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorageRepository _storage;

        public StorageController(IAzureStorageRepository storage)
        {
            _storage = storage;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<BlobDto>>> Get()
        {
            // Get all files at the Azure Storage Location and return them
            List<BlobDto> files = await _storage.ListAsync();

            // Returns an empty array if no files are present at the storage container
            return Ok(files);
        }

        [HttpPost()]
        // Set the maximum size of the request to 50MB
        [RequestSizeLimit(bytes: 52428800)]
        public async Task<ActionResult<BlobResponseDto>> Upload(IFormFile file)
        {
            BlobResponseDto? response = await _storage.UploadAsync(file);

            // Check if we got an error
            if (response.Error == true)
            {
                // We got an error during upload, return an error with details to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // Return a success message to the client about successfull upload
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        // Rename the Endpoint to Download
        [HttpGet("Download/{filename}")]
        [ActionName("Download")]
        public async Task<ActionResult<BlobDto>> Download([FromRoute]string filename)
        {
            BlobDto? file = await _storage.DownloadAsync(filename);

            // Check if file was found
            if (file == null)
            {
                // Was not, return error message to client
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                // File was found, return it to client
                return File(file.Content, file.ContentType, file.Name);
            }
        }

        // Get the properties of a file without downloading it
        /*[HttpGet("Get/{filename}")]
        [ActionName("Get")]
        public async Task<ActionResult<BlobDto>> Get([FromRoute]string filename)
        {
            BlobDto? file = await _storage.GetAsync(filename);

            // Check if file was found
            if (file == null)
            {
                // Was not, return error message to client
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be found.");
            }
            else
            {
                // File was found, return it to client
                return Ok(file);
            }
        }*/
        // A dual role endpoint that allows for both downloading and getting file properties using the same endpoint
        // and a query parameter to determine the action
        [HttpGet("{filename}")]
        public async Task<ActionResult<BlobDto>> Get([FromRoute]string filename, [FromQuery]bool download = false)
        {
            // Unescape the filename
            filename = Uri.UnescapeDataString(filename);

            if (download)
            {
                // Download the file
                BlobDto? file = await _storage.DownloadAsync(filename);

                // Check if file was found
                if (file == null)
                {
                    // Was not, return error message to client
                    return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be found.");
                }

                // Return the file for download
                return File(file.Content, file.ContentType, file.Name);
            }
            else
            {
                // Get the properties of the file
                BlobDto? file = await _storage.GetAsync(filename);

                // Check if file was found
                if (file == null)
                {
                    // Was not, return error message to client
                    return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be found.");
                }

                // Return the file properties
                return Ok(file);
            }
        }

        [HttpDelete("{filename}")]
        public async Task<ActionResult<BlobResponseDto>> Delete([FromRoute]string filename)
        {
            // Unescape the filename
            BlobResponseDto response = await _storage.DeleteAsync(filename);

            // Check if we got an error
            if (response.Error == true)
            {
                // Return an error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            } else
            {
                // File has been successfully deleted
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }
    }
}
