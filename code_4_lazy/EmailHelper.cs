using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace code_4_lazy
{
    public class EmailHelper
    {
        public EmailHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void enviarEmail(string assunto, string mensagem, string email_destinatario, string caminho_anexo = "")
        {
            //Define os dados do e-mail
            string nomeRemetente = "Code4Lazy";
            string emailDestinatario = email_destinatario;
            string assuntoMensagem = assunto;
            string conteudoMensagem = mensagem;

            //Dados do SMTP
            string host = ConfigurationManager.AppSettings["smtp_host"].ToString();
            string userName = ConfigurationManager.AppSettings["smtp_userName"].ToString();
            string password = ConfigurationManager.AppSettings["smtp_password"].ToString();
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["smtp_port"].ToString()); 

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new System.Net.Mail.MailAddress("contato@code4lazy.com", nomeRemetente);

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add("rafaelgrilli92@hotmail.com");

            //Enviar cópia oculta para.
            objEmail.Bcc.Add("rafaelgrilli92@hotmail.com");

            //Define a prioridade do e-mail.
            objEmail.Priority = System.Net.Mail.MailPriority.Normal;

            //Define o formato do e-mail HTML
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            // ANEXO
            if (caminho_anexo != "")
            {
                // Cria o anexo para o e-mail
                Attachment anexo = new Attachment(caminho_anexo, System.Net.Mime.MediaTypeNames.Application.Octet);

                // Anexa o arquivo a mensagemn
                objEmail.Attachments.Add(anexo);
            }

            System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient(host, port);
            NetworkCredential credentials = new NetworkCredential(userName, password);
            objSmtp.Credentials = credentials;
            objSmtp.EnableSsl = false;

            //Enviamos o e-mail através do método .send()
            objSmtp.Send(objEmail);
            objEmail.Dispose();
        }
    }
}