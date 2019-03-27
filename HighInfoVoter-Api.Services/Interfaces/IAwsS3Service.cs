using HighInfoVoter_Api.Models.View;

namespace HighInfoVoter_Api.Services.Interfaces
{
    public interface IAwsS3Service
    {
        void UploadAll();
        SignedUrlView SelectByKey(string key);
    }
}
