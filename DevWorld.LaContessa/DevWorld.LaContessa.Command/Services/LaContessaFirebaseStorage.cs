using Firebase.Storage;

namespace DevWorld.LaContessa.Command.Services
{
    public class LaContessaFirebaseStorage : ILaContessaFirebaseStorage
    {
        public async Task<string?> StoreImageData(string base64Image, string folderName, string imageName)
        {
            try
            {
                var stream = new MemoryStream(Convert.FromBase64String(base64Image));

                var firebaseStorageOptions = new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                };

                var task = new FirebaseStorage(
                        "lacontessa.appspot.com",
                        firebaseStorageOptions
                    )
                    .Child(folderName)
                    .Child(imageName)
                    .PutAsync(stream);

                return await task;
            }
            catch (Exception)
            {
                var firebaseStorageOptions = new FirebaseStorageOptions
                {
                    ThrowOnCancel = true,
                };

                var task = new FirebaseStorage(
                        "lacontessa.appspot.com",
                        firebaseStorageOptions
                    )
                    .Child(folderName)
                    .Child("defaultImage.png")
                    .GetDownloadUrlAsync();

                return await task;
            }
        }
    }
}
