using System;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace DispelDLL.Infrastructure
{
    public class Utilidades
    {
        public static string StrTokenMD5(string emailUsuario)
        {
            string keyMd5Token = string.Concat(emailUsuario, DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"));

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inBytes = Encoding.ASCII.GetBytes(keyMd5Token);
                byte[] hashBytes = md5.ComputeHash(inBytes);

                StringBuilder stringBilder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBilder.Append(hashBytes[i].ToString("X2"));
                }

                return stringBilder.ToString();
            }
        }

        public static bool EnviaEmailRecuperaSenha(string emailUsuario, string urlRecuperaSenha)
        {
            using (MailMessage mailMessage = new MailMessage("", emailUsuario))
            {
                mailMessage.Subject = "Dispel - Redefinir Senha";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = Utilidades.CorpoEmailRecuperaSenha(urlRecuperaSenha);
                                
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;

                // Mecanismo que utilza as credencias sem/com certificado SSL
                System.Net.NetworkCredential credencial = new System.Net.NetworkCredential("", "");
                smtpClient.Credentials = credencial;                
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                smtpClient.Send(mailMessage);
                return true;
            }
        }

        public static string CorpoEmailRecuperaSenha(string urlRecuperaSenha)
        {
            string body =
                         "<html>" +
                            "<head>" +
                                "<meta http-equiv='content-type' content='text/html;' charset = 'UTF-8'>" +
                             "</head>" +
                            "<body style='margin: 0;'>" +
                               "<div style='font-family: Verdana, Arial, Helvetica, sans-serif; width: 100%; padding: 2.222%;'>" +
                                   "<p style='font-size: 1.2rem; font-weight: 400; line-height: 2rem; color: #3f51b5; font-family: Verdana, Arial, Helvetica, sans-serif;'>Olá,</p>" +
                                   "<p style='font-size: 1.2rem; font-weight: 400; line-height: 2rem; color: #3f51b5; font-family: Verdana, Arial, Helvetica, sans-serif;'>Não se preocupe, seu sigilo será sempre mantido.<br/> Basta clicar no botão abaixo para que possamos redefinir a sua senha.</p>" +
                               "</div>" +
                               "<div style='font-family: Verdana, Arial, Helvetica, sans-serif; width: 100%; padding: 2.222%;'>" +
                                   "<a style='background-color: #3f51b5; color: #fff; border-color: rgba(26,35,126,1); text-decoration: none; padding: 12px; font-family: Verdana, Arial, Helvetica, sans-serif' href='https://" + urlRecuperaSenha + "' target='_blank'>Redefinir Senha</a> " +
                               "</div>" +
                            "</body>" +
                         "</html>";
            return body;
        }

        public static DateTime ConverterParaHrBrasilia(DateTime hrServidor)
        {
            TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(hrServidor, hrBrasilia);
        }

        public static string VerificarCustoPreco(string custo, string preco, out string critica)
        {
            critica = string.Empty;

            if (!string.IsNullOrWhiteSpace(custo) && !string.IsNullOrWhiteSpace(preco))
            {
                if (Convert.ToDecimal(custo) > Convert.ToDecimal(preco))
                    critica = string.Format("Não é possível cadastrar o valor de Custo R$ {0} maior do que o Preço R$ {1}", custo, preco);
            }

            return critica;
        }
    }
}
