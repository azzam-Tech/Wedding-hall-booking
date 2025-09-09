namespace RoyalHall.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string uploadFolderPath);
        void DeleteImage(string imagePath);
    }
}
