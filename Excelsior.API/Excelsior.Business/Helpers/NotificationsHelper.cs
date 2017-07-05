using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Excelsior.Business.Helpers
{
    public class NotificationsHelper
    {
        public static void SendNotifications(long userId, string notificationName, long? studyId = null, long? affiliationId = null, long? seriesId = null, bool? isMultiModality = false)
        {
            var settings = new Settings();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var dataQueue = queueStorage.GetQueueReference("process-data");
            dataQueue.CreateIfNotExists();
            dataQueue.AddMessage(new CloudQueueMessage(string.Format("{0},{1},{2},{3},{4},{5}", notificationName, userId, studyId, affiliationId, seriesId, isMultiModality)));
        }

        public static void SendUploadConfirmationEmail(long userId, long seriesId)
        {
            var settings = new Settings();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var dataQueue = queueStorage.GetQueueReference("process-data");
            dataQueue.CreateIfNotExists();
            dataQueue.AddMessage(new CloudQueueMessage(string.Format("{0},{1},{2}", "uploadconfirmation", userId, seriesId)));
        }

        public static void NotifyNewQueryMessage(QRY_Message message)
        {
            Notify("querynewmessagenotification", message.UserID, message.MessageID);
        }

        public static void NotifyQueryResolved(QRY_Message message)
        {
            Notify("queryresolvenotification", message.UserID, message.MessageID);
        }

        private static void Notify(string reqType, long? userId, long? id)
        {
            var settings = new Settings();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var dataQueue = queueStorage.GetQueueReference("process-data");
            dataQueue.CreateIfNotExists();
            dataQueue.AddMessage(new CloudQueueMessage(string.Format("{0},{1},{2}", reqType, userId, id)));
        }

        public static void NotifyTechnicianCertification(long userId, CERT_User certUser)
        {
            Notify("techniciancertificationnotification", userId, certUser.CertUserID);
        }

        public static void NotifyEquipmentCertification(long userId, CERT_Equipment certEquipment)
        {
            Notify("equipmentcertificationnotification", userId, certEquipment.CertEquipmentID);
        }

        public static void SendForgotUserNameEmail(string emailTo)
        {
            var settings = new Settings();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var dataQueue = queueStorage.GetQueueReference("process-data");
            dataQueue.CreateIfNotExists();
            dataQueue.AddMessage(new CloudQueueMessage(string.Format("{0},{1},{2},{3}", "forgotusername", 0, 0, emailTo)));

        }

        public static void SendForgotAnswerEmail(string username)
        {
            var settings = new Settings();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            var storageClient = storageAccount.CreateCloudBlobClient();
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var dataQueue = queueStorage.GetQueueReference("process-data");
            dataQueue.CreateIfNotExists();
            dataQueue.AddMessage(new CloudQueueMessage(string.Format("{0},{1},{2},{3}", "forgotanswer", 0, 0, username)));
        }

        public static bool SendEMail(EmailBaseDto email, string sender, string sysEmailAccountName, string sysEmailAccountPort, string sysEmailAccountHost, string sysEmailAccountPwd)
        {
            try
            {
                var body = $"From adress:<b>{email.From}</b> <br/> {email.Body}";

                MailMessage message = new MailMessage();

                message.From = new MailAddress(sysEmailAccountName);
                message.To.Add(new MailAddress(sender));
                message.Subject = email.subject;
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = sysEmailAccountHost;
                smtp.Port = Convert.ToInt32(sysEmailAccountPort);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                var cred = new NetworkCredential();
                cred.UserName = sysEmailAccountName;
                cred.Password = sysEmailAccountPwd;
                smtp.Credentials = cred;

                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending email.", ex);
            }
        }
    }
}