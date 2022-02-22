using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ImagesBusiness
    {
        private readonly IConfiguration _configuration;

        public ImagesBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> UploadFileAsync(IFormFile image)
        {
            var credentials = new BasicAWSCredentials(_configuration["S3Config:AccessKey"], _configuration["S3Config:SecretKey"]);


            IAmazonS3 client = new AmazonS3Client(credentials, RegionEndpoint.SAEast1);
            var request = new PutObjectRequest
            {
                BucketName = _configuration["S3Config:BucketName"],
                Key = image.FileName,
                InputStream = image.OpenReadStream(),

            };

            var response = await client.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
