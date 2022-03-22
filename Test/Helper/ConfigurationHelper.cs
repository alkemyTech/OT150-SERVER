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
                {"S3Config:BucketName", "cohorte-febrero-b35bfd02"},
                {"S3Config:AccessKey", "AKIAS2JWQJCDIGZRCIJY"},
                { "S3Config:SecretKey", "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"},
                { "JWT:secret", "fhdsakfhsjkfhksdhfshfkjshzfkjszhfjkhsfvhxzfggbfhdsakfhsjkfhksdhfshfkjshzfkjszhfjkhsfvhxzfggb"},
                {"ConnectionStrings", "Server=localhost;Initial Catalog=AceleracionOng ;Trusted_Connection=True;"},
                {"MailParams:SendGridKey", "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"},
                {"MailParams:FromMail", "ongsomosmas4@gmail.com"},
                {"MailParams:FromMailDescription", "ONG Somos Mas"},
                {"MailParams:PathTemplate", "Templates/htmlpage.html"},
                {"MailParams:ReplaceMailTitle", "{mail_title}"},
                {"MailParams:ReplaceMailBody", "{mail_body}"},
                {"MailParams:ReplaceMailContact", "{mail_contact}"},
                {"MailParams:WelcomeMailTitle", "Bienvenido a Ong Somos Mas!"},
                {"MailParams:WelcomeMailBody", "<p>¡Te damos la bienvenida a Ong Somos Mas!</p><p>Ahora puedes acceder a nuestro sitio, conocer nuestro trabajo, actividades y a nuestros colaboradores.</p>"},
                {"MailParams:WelcomeMailContact", "NUESTROEMAIL@gmail.com"},
                {"SendGridAPIKey","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"}
            };

            configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
