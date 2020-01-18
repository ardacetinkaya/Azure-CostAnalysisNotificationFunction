namespace NotificationFunction
{
    using CostLibrary;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using SendGrid.Helpers.Mail;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class Notification
    {

        [FunctionName("Notification")]
        public async static Task Run([BlobTrigger("cost/report/{name}", Connection = "StorageConnection")]Stream myBlob, string name,
            [SendGrid(ApiKey = "SendGridKeyAppSettingName")] IAsyncCollector<SendGridMessage> messageCollector,ILogger log)
        {

            try
            {
                FileAnalysis fa = new FileAnalysis(log);

                var contents = await fa.ReadFile(myBlob, name);

                var output = contents.ToStringTable(
                    new[] { "Service Name", "Resource Group", "Meter Category" },
                    a => a.ServiceName, a => a.ResourceGroup, a => a.MeterCategory);

                var message = new SendGridMessage();
                message.AddTo("");
                message.AddContent("text/html", output);
                message.SetFrom(new EmailAddress("noreply@productx.com"));
                message.SetSubject("Cost Analysis");

                await messageCollector.AddAsync(message);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }


        }
    }
}
