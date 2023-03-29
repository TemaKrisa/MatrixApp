using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Matrix.Class
{
    public static class EmailClass
    {
        private static string GenerateCode() // Данный класс отвечает за генерацию кода отправляемого сообщения
        {
            Random r = new();
            string code = r.Next(111, 999).ToString();
            return code;
        }

        public static string EmailCheck(string email) //Данный метод осуществляет отправку сообщения с кодом на электронную почту
        {
            string code = GenerateCode();
            using CosmeticshopContext dc = new();
            EmailDatum? q = dc.EmailData.SingleOrDefault();
            MailMessage message = new()
            {
                From = new MailAddress(q.EmailAdress),
                Subject = "Подтверждение эл. почты для нового аккаунта",
                IsBodyHtml = true
            };
            message.To.Add(email);
            //HTML шаблон для отправки сообщения
            message.Body = @"
<head>
    <title>
    </title>
    <!--[if !mso]><!-- -->                       
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <!--<![endif]-->
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <style type=""text/css"">
        #outlook a {
            padding: 0;
        }
        .ReadMsgBody {
            width: 100%;
        }
        .ExternalClass {
            width: 100%;
        }
        .ExternalClass * {
            line-height: 100%;
        }
        body {
            margin: 0;
            padding: 0;
            -webkit-text-size-adjust: 100%;
            -ms-text-size-adjust: 100%;
        }

        table,
        td {
            border-collapse: collapse;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }
        img {
            border: 0;
            height: auto;
            line-height: 100%;
            outline: none;
            text-decoration: none;
            -ms-interpolation-mode: bicubic;
        }
        p {
            display: block;
            margin: 13px 0;
        }
    </style>
    <!--[if !mso]><!-->
    <style type=""text/css"">
        @media only screen and (max-width:480px) {
            @-ms-viewport {
                width: 320px;
            }
            @viewport {
                width: 320px;
            }
        }
    </style>
    <!--<![endif]-->
    <!--[if mso]>
        <xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG/>
          <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
        </xml>
        <![endif]-->
    <!--[if lte mso 11]>
        <style type=""text/css"">
          .outlook-group-fix { width:100% !important; }
        </style>
        <![endif]-->
    <style type=""text/css"">
        @media only screen and (min-width:480px) {
            .mj-column-per-100 {
                width: 100% !important;
            }
        }
    </style>
    <style type=""text/css"">
    </style>
</head>
<body style=""background-color:#f9f9f9;"">
    <div style=""background-color:#f9f9f9;"">
        <!--[if mso | IE]>
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->
        <div style=""background:#f9f9f9;background-color:#f9f9f9;Margin:0px auto;max-width:600px;"">
            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""background:#f9f9f9;background-color:#f9f9f9;width:100%;"">
                <tbody>
                    <tr>
                        <td style=""border-bottom:#333957 solid 5px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
        </tr>
                  </table>            
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->
        <div style=""background:#fff;background-color:#fff;Margin:0px auto;max-width:600px;"">
            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""background:#fff;background-color:#fff;width:100%;"">
                <tbody>
                    <tr>
                        <td style=""border:#dddddd solid 1px;border-top:0px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
            <td
               style=""vertical-align:bottom;width:600px;""
            >
          <![endif]-->
                            <div class=""mj-column-per-100 outlook-group-fix"" style=""font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""vertical-align:bottom;"" width=""100%"">
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px;"">
                                                <tbody>
                                                    <tr>
                                                        <td style=""width:64px;"">
                                                            <img height=""auto"" src=""https://cdn.discordapp.com/attachments/769897243472953399/1090644536985931786/Logo.png"" style=""border:0;display:block;outline:none;text-decoration:none;width:100%;"" width=""64"" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:32px;font-weight:bold;line-height:1;text-align:center;color:#555;"">
                                              Пожалуйста подтвердите ваш email
                                            </div>
                                      </td>
                                  </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:0;word-break:break-word;"">
                                        </td>
                                   </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:20px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:16px;line-height:22px;text-align:center;color:#555;"">
                                                Пожалуйста введите следующий код для подтверждения: " + code + @".
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-top:30px;padding-bottom:40px;word-break:break-word;"">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:26px;font-weight:bold;line-height:1;text-align:center;color:#555;"">
                                                Нужна помощь?
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:14px;line-height:22px;text-align:center;color:#555;"">
                                                Пожалуйста, отправьте отзыв или информацию об ошибке<br><a href=""mailto:testmail53@list.ru"" style=""color:#2F67F6"">testmail53@list.ru</a>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--[if mso | IE]>
            </td>
        </tr>
                  </table>
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->
        <div style=""Margin:0px auto;max-width:600px;"">
            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width:100%;"">
                <tbody>
                    <tr>
                        <td style=""direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
            <td
               style=""vertical-align:bottom;width:600px;""
            >
          <![endif]-->
                            <div class=""mj-column-per-100 outlook-group-fix"" style=""font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;"">

                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"">
                                    <tbody>
                                        <tr>
                                            <td style=""vertical-align:bottom;padding:0;"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"">
                                                    <tr>
                                                        <td align=""center"" style=""font-size:0px;padding:0;word-break:break-word;"">
                                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:12px;font-weight:300;line-height:1;text-align:center;color:#575757;"">
                                                                ????
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <!--[if mso | IE]>
            </td>
        </tr>
                  </table>
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      <![endif]-->
    </div>
</body>

";
            using SmtpClient smtpClient = new(q.Host, q.Port)
            {
                Credentials = new NetworkCredential(q.EmailAdress, q.AuthToken),
                EnableSsl = true
            };
            smtpClient.Send(message);
            return code;
        }

        public static string ResetPas(string email) //Данный метод отвечает за отправку содержимого переменной body по электронному адресу email
        {
            string code = GenerateCode();
            using CosmeticshopContext dc = new();
            EmailDatum? q = dc.EmailData.SingleOrDefault();
            MailMessage message = new()
            {
                From = new MailAddress(q.EmailAdress),
                Subject = "Подтверждение эл. почты для нового аккаунта",
                IsBodyHtml = true
            };
            message.To.Add(email);
            message.Body = @"
<head>
    <title>
    </title>
    <!--[if !mso]><!-- -->
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <!--<![endif]-->
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <style type=""text/css"">
        #outlook a {
            padding: 0;
        }
        .ReadMsgBody {
            width: 100%;
        }
        .ExternalClass {
            width: 100%;
        }
        .ExternalClass * {
            line-height: 100%;
        }
        body {
            margin: 0;
            padding: 0;
            -webkit-text-size-adjust: 100%;
            -ms-text-size-adjust: 100%;
        }
        table,
        td {
            border-collapse: collapse;
            mso-table-lspace: 0pt;
            mso-table-rspace: 0pt;
        }

        img {
            border: 0;
            height: auto;
            line-height: 100%;
            outline: none;
            text-decoration: none;
            -ms-interpolation-mode: bicubic;
        }

        p {
            display: block;
            margin: 13px 0;
        }
    </style>
    <!--[if !mso]><!-->
    <style type=""text/css"">
        @media only screen and (max-width:480px) {
            @-ms-viewport {
                width: 320px;
            }
            @viewport {
                width: 320px;
            }
        }
    </style>
    <!--<![endif]-->
    <!--[if mso]>
        <xml>
        <o:OfficeDocumentSettings>
          <o:AllowPNG/>
          <o:PixelsPerInch>96</o:PixelsPerInch>
        </o:OfficeDocumentSettings>
        </xml>
        <![endif]-->
    <!--[if lte mso 11]>
        <style type=""text/css"">
          .outlook-group-fix { width:100% !important; }
        </style>
        <![endif]-->
    <style type=""text/css"">
        @media only screen and (min-width:480px) {
            .mj-column-per-100 {
                width: 100% !important;
            }
        }
    </style>
    <style type=""text/css"">
    </style>
</head>
<body style=""background-color:#f9f9f9;"">
    <div style=""background-color:#f9f9f9;"">
        <!--[if mso | IE]>
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->


        <div style=""background:#f9f9f9;background-color:#f9f9f9;Margin:0px auto;max-width:600px;"">

            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""background:#f9f9f9;background-color:#f9f9f9;width:100%;"">
                <tbody>
                    <tr>
                        <td style=""border-bottom:#333957 solid 5px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
        </tr>
                  </table>
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->
        <div style=""background:#fff;background-color:#fff;Margin:0px auto;max-width:600px;"">
            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""background:#fff;background-color:#fff;width:100%;"">
                <tbody>
                    <tr>
                        <td style=""border:#dddddd solid 1px;border-top:0px;direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
            <td
               style=""vertical-align:bottom;width:600px;""
            >
          <![endif]-->
                            <div class=""mj-column-per-100 outlook-group-fix"" style=""font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""vertical-align:bottom;"" width=""100%"">
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px;"">
                                                <tbody>
                                                    <tr>
                                                        <td style=""width:64px;"">
                                                            <img height=""auto"" src=""https://cdn.discordapp.com/attachments/769897243472953399/1077651212863807508/new-registration-icon11.png"" style=""border:0;display:block;outline:none;text-decoration:none;width:100%;"" width=""64"" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:38px;font-weight:bold;line-height:1;text-align:center;color:#555;"">
                                                Упс!
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:18px;line-height:1;text-align:center;color:#555;"">
                                                Похоже вы забыли свой пароль.
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;word-break:break-word;"">
                                            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""border-collapse:collapse;border-spacing:0px;"">
                                                <tbody>
                                                    <tr>
                                                        <td style=""width:128px;"">
                                                            <img height=""auto"" src=""https://i.imgur.com/247tYSw.png"" style=""border:0;display:block;outline:none;text-decoration:none;width:100%;"" width=""128"" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-top:30px;padding-bottom:50px;word-break:break-word;"">
                                            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""border-collapse:separate;line-height:100%;"">
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;"">
                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:18px;line-height:1;text-align:center;color:#555;"">
                                                Для изменения пароля пожалуйста введите следующий код: " + code + @".
                                            </div>
                                        </td>
                                    </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align=""center"" style=""font-size:0px;padding:10px 25px;padding-bottom:40px;word-break:break-word;"">

                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:16px;line-height:20px;text-align:center;color:#7F8FA4;"">
                                               Если вы не собирались сбросить пароль просто проигнорируйте данное сообщение.
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--[if mso | IE]>
            </td>
        </tr>
                  </table>
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      <table
         align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""width:600px;"" width=""600""
      >
        <tr>
          <td style=""line-height:0px;font-size:0px;mso-line-height-rule:exactly;"">
      <![endif]-->
        <div style=""Margin:0px auto;max-width:600px;"">
            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width:100%;"">
                <tbody>
                    <tr>
                        <td style=""direction:ltr;font-size:0px;padding:20px 0;text-align:center;vertical-align:top;"">
                            <!--[if mso | IE]>
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
        <tr>
            <td
               style=""vertical-align:bottom;width:600px;""
            >
          <![endif]-->
                            <div class=""mj-column-per-100 outlook-group-fix"" style=""font-size:13px;text-align:left;direction:ltr;display:inline-block;vertical-align:bottom;width:100%;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"">
                                    <tbody>
                                        <tr>
                                            <td style=""vertical-align:bottom;padding:0;"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" width=""100%"">
                                                    <tr>
                                                        <td align=""center"" style=""font-size:0px;padding:0;word-break:break-word;"">
                                                            <div style=""font-family:'Helvetica Neue',Arial,sans-serif;font-size:12px;font-weight:300;line-height:1;text-align:center;color:#575757;"">
                                                                ????
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <!--[if mso | IE]>
            </td>
        </tr>
                  </table>
                <![endif]-->
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--[if mso | IE]>
          </td>
        </tr>
      </table>
      <![endif]-->
    </div>
</body>
";
            using SmtpClient smtpClient = new(q.Host, q.Port)
            {
                Credentials = new NetworkCredential(q.EmailAdress, q.AuthToken),
                EnableSsl = true
            };
            smtpClient.Send(message);
            return code;
        }
    }

}
