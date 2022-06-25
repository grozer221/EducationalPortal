using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace EducationalPortal.Server.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService()
        {
            cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
        }

        public async Task<string> UploadFileAsync(IFormFile file, bool withHash = true)
        {
            string fileName = withHash ? $"{Guid.NewGuid()}_{file.FileName}" : file.FileName;
            var uploadParams = CreateUploadParams<RawUploadParams>(file, $"{nameof(file)}s/{fileName}");
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }

        public async Task<string> UploadImageAsync(IFormFile image, string imageName, int width, int height)
        {
            var uploadParams = CreateUploadParams<ImageUploadParams>(image, $"{nameof(image)}s/{imageName}");
            uploadParams.Transformation = new Transformation()
                .Height(height).Width(width).Crop("scale");
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }

        public async Task<string> UploadVideoAsync(IFormFile video, string videoName)
        {
            var uploadParams = CreateUploadParams<VideoUploadParams>(video, $"{nameof(video)}s/{videoName}");
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult.Url.ToString();
        }

        public Task DeleteAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Empty url");
            string publicId = string.Join("/", url.Split("/")[^2..]);
            var deletionParams = new DeletionParams(publicId);
            return cloudinary.DestroyAsync(deletionParams);
        }

        private T CreateUploadParams<T>(IFormFile file, string name)
            where T : RawUploadParams
        {
            Stream stream = file.OpenReadStream();
            var uploadParams = (T)Activator.CreateInstance(typeof(T));
            uploadParams.File = new FileDescription(name, stream);
            uploadParams.PublicId = name;
            uploadParams.Overwrite = true;
            return uploadParams;
        }
    }
}
