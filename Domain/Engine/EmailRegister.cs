using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Domain.Engine
{
    // Почтовые установки
    public static class EmailSettings
    {
        //public static string MailToDefault = "165@list.ru, kvv5858@mail.ru";
        //public static string MailToDefault = "kvv5858@mail.ru";
        public static MailAddress MailFromAddress = new MailAddress("info@abz4.ru");
        public static bool UseSsl = true;
        public static string Username = "info@abz4.ru";
        public static string Password = "m955kc150RUS";
        public static string ServerName = "smtp.yandex.ru";
        public static int ServerPort = 587;
    }

    public static class EmailSend
    {
        public static async Task SendEmailAsync(string mail,string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = EmailSettings.UseSsl;
                smtpClient.Host = EmailSettings.ServerName;
                smtpClient.Port = EmailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(EmailSettings.Username, EmailSettings.Password);

                MailMessage mailMessage=new MailMessage(EmailSettings.MailFromAddress,new MailAddress(mail));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                await smtpClient.SendMailAsync(mailMessage);
                //mailMessage.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Отправка Mail: " + e.Message);
            }
        }

        public static void SendEmail(string mail, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.EnableSsl = EmailSettings.UseSsl;
                smtpClient.Host = EmailSettings.ServerName;
                smtpClient.Port = EmailSettings.ServerPort;
                smtpClient.DeliveryFormat = SmtpDeliveryFormat.SevenBit;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(EmailSettings.Username, EmailSettings.Password);

                MailMessage mailMessage = new MailMessage(EmailSettings.MailFromAddress, new MailAddress(mail));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                smtpClient.Send(mailMessage);
                //mailMessage.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Отправка Mail: " + e.Message);
            }
        }

        public static async Task EMailRegAsync(string mail, string psw)
        {
            string subject = "Регистрация АБЗ-4";
            StringBuilder body = new StringBuilder()
                .AppendLine("Здравствуйте!")
                .AppendLine("Вы зарегестрированы в личном кабинете АБЗ-4")
                .AppendLine("")
                .AppendLine("Ваш логин: " + mail)
                .AppendLine("Ваш пароль: " + psw)
                .AppendLine("")
                .AppendLine("https://lk.abz4.ru")
                .AppendLine("")
                .AppendLine("Письмо создано автоматически. Не надо на него отвечать");
            await SendEmailAsync(mail, subject, body.ToString());
        }


        public static async Task EMailFPassw(string mail, string psw)
        {
            string subject = "Восстановление пароля АБЗ-4";
            StringBuilder body = new StringBuilder()
                .AppendLine("Здравствуйте!")
                .AppendLine("Вы запросили восстановление пароля в личном кабинете АБЗ-4")
                .AppendLine("")
                .AppendLine("Ваш логин: " + mail)
                .AppendLine("Ваш новый пароль: " + psw)
                .AppendLine("")
                .AppendLine("Письмо создано автоматически. Не надо на него отвечать");
            //SendEmail(mail, subject, body.ToString());
            //SendEmailAsync(mail, subject, body.ToString()).GetAwaiter();
            await SendEmailAsync(mail, subject, body.ToString());
        }

        public static void EMailReg(string mail, string psw)
        {
            string subject = "Регистрация АБЗ-4";
            StringBuilder body = new StringBuilder()
                .AppendLine("Здравствуйте!")
                .AppendLine("Вы зарегестрированы в личном кабинете АБЗ-4")
                .AppendLine("")
                .AppendLine("Ваш логин: " + mail)
                .AppendLine("Ваш пароль: " + psw)
                .AppendLine("")
                .AppendLine("https://lk.abz4.ru")
                .AppendLine("")
                .AppendLine("Письмо создано автоматически. Не надо на него отвечать");

            //SendEmailAsync(mail, subject, body.ToString()).GetAwaiter();
            //Для отладки
            //mail = EmailSettings.MailToDefault;

            SendEmail(mail, subject, body.ToString());
        }
    }


}
