using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ImagesBusiness
    {
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _amazonS3;

        public ImagesBusiness(IConfiguration configuration)
        {
            _configuration = configuration;
            BasicAWSCredentials credentials = new BasicAWSCredentials(_configuration["S3Config:AccessKey"], _configuration["S3Config:SecretKey"]);
            _amazonS3 = new AmazonS3Client(credentials, RegionEndpoint.USEast1);
        }

        public async Task<string> UploadFileAsync(IFormFile image)
        {
            
            var request = new PutObjectRequest
            {
                BucketName = _configuration["S3Config:BucketName"],
                Key = image.FileName,
                InputStream = image.OpenReadStream(),
                CannedACL = S3CannedACL.PublicRead
            };


            var response = await _amazonS3.PutObjectAsync(request);

            var url = "https://s3.amazonaws.com/" + _configuration["S3Config:BucketName"] + "/" + image.FileName;

            return url.Replace(" ", "+");
        }
    }
}
