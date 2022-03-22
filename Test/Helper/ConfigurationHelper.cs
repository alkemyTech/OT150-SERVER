using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    internal class ConfigurationHelper
    {
        public readonly IConfiguration configuration;
        public ConfigurationHelper()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:secret", "fhdsakfhsjkfhksdhfshfkjshzfkjszhfjkhsfvhxzfggbfhdsakfhsjkfhksdhfshfkjshzfkjszhfjkhsfvhxzfggb"},
                {"ConnectionStrings", "Server=localhost;Initial Catalog=AceleracionOng ;Trusted_Connection=True;"},
                {"MailParams:SendGridKey", ""},
                {"MailParams:FromMail", "ongsomosmas4@gmail.com"},
                {"MailParams:FromMailDescription", "ONG Somos Mas"},
                {"MailParams:PathTemplate", "Templates/htmlpage.html"},
                {"MailParams:ReplaceMailTitle", "{mail_title}"},
                {"MailParams:ReplaceMailBody", "{mail_body}"},
                {"MailParams:ReplaceMailContact", "{mail_contact}"},
                {"MailParams:WelcomeMailTitle", "Bienvenido a Ong Somos Mas!"},
                {"MailParams:WelcomeMailBody", "<p>¡Te damos la bienvenida a Ong Somos Mas!</p><p>Ahora puedes acceder a nuestro sitio, conocer nuestro trabajo, actividades y a nuestros colaboradores.</p>"},
                {"MailParams:WelcomeMailContact", "NUESTROEMAIL@gmail.com"},
                {"SendGridAPIKey",""},
                {"S3Config:BucketName", ""},
                {"S3Config:AccessKey", ""},
                {"S3Config:SecretKey", ""},
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
