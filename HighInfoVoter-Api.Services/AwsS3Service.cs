using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using HighInfoVoter_Api.Models.View;
using HighInfoVoter_Api.Services.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HighInfoVoter_Api.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private IConfigService _configService;

        private static string _accessKey;
        private static string _secretKey;
        private static string _bucketName;
        private static string _dirName;

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;
        private static IAmazonS3 s3Client;

        public AwsS3Service(IConfigService configService)
        {
            _configService = configService;
            _accessKey = _configService.GetByKey("S3_ACCESSKEY_ID").ConfigValue;
            _secretKey = _configService.GetByKey("S3_SECRET_ACCESSKEY").ConfigValue;
            _bucketName = _configService.GetByKey("S3_BUCKET_NAME").ConfigValue;
            _dirName = _configService.GetByKey("S3_MEMBER_PORTRAIT_PATH").ConfigValue;
        }

        public void UploadAll()
        {
            s3Client = new AmazonS3Client(_accessKey, _secretKey, bucketRegion);
            UploadFiles();
        }

        public SignedUrlView SelectByKey(string key)
        {
            s3Client = new AmazonS3Client(_accessKey, _secretKey, bucketRegion);
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = _bucketName,
                Key = key,
                Expires = DateTime.Now.AddMinutes(60),
                Protocol = Protocol.HTTPS
            };
            SignedUrlView view = new SignedUrlView();
            view.SignedUrl = s3Client.GetPreSignedURL(request);
            return view;
        }

        private static void UploadFiles()
        {
            const string sourceDir = @"c:\scrapings\";

            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                var files = Directory.GetFiles(sourceDir);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    fileTransferUtility.Upload(file, _bucketName, fileName);
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new AmazonS3Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
