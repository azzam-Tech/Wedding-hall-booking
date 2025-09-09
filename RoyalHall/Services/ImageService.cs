namespace RoyalHall.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string uploadFolderPath)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null; // أو يمكنك رمي استثناء
            }

            string uniqueFileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            string filePath = Path.Combine(_environment.WebRootPath, uploadFolderPath, uniqueFileName);

            // إنشاء المجلد إذا لم يكن موجودًا
            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, uploadFolderPath));

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // إرجاع المسار النسبي الذي يمكن استخدامه في علامة <img>
            return Path.Combine("/", uploadFolderPath, uniqueFileName).Replace("\\", "/");
        }

        public void DeleteImage(string imagePath)
        {
            if (!string.IsNullOrEmpty(imagePath))
            {
                string absolutePath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/').Replace("/", "\\"));
                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                }
            }
        }
    }
}
