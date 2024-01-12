namespace DevWorld.LaContessa.Command.Services
{
    public interface ILaContessaFirebaseStorage
    {
        public Task<string?> StoreImageData(string base64Image, string folderName, string imageName);
    }
}
