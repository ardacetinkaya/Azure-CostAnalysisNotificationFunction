namespace NotificationFunction
{
    using CostLibrary;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using SendGrid.Helpers.Mail;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public static class Notification
    {

        [FunctionName("Notification")]
        public async static Task Run([BlobTrigger("cost/report/{name}", Connection = "StorageConnection")]Stream myBlob, string name,
            [SendGrid(ApiKey = "SendGridKeyAppSettingName")] IAsyncCollector<SendGridMessage> messageCollector, ILogger log, ExecutionContext context)
        {

            try
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                FileAnalysis fa = new FileAnalysis(log);
                

                var contents = await fa.ReadFile(myBlob, name);
                log.LogInformation($"# of records {contents.Count}");

                var filtered = contents.ToList().GroupBy(g => new
                {
                    g.ServiceName,
                    g.ResourceGroup,
                    g.UsageDateTime.Year,
                    g.UsageDateTime.Month

                }).OrderByDescending(o=>o.First().UsageDateTime).Select(s => new
                {
                    UsageDate = $"{s.Key.Year}/{String.Format("{0:00}", s.Key.Month)}",
                    s.Key.ResourceGroup,
                    s.Key.ServiceName,
                    Sum = s.Sum(s => Math.Round(s.PreTaxCost, 2)),
                    s.First().Currency
                });
                
                var output = filtered.ToList().ToHtmlTable();

                var message = new SendGridMessage();
                message.AddTo(config["Sender"]);
                message.AddContent("text/html", output);
                message.SetFrom(new EmailAddress("noreply@productx.com"));
                message.SetSubject("Azure - Cost Analysis");

                await messageCollector.AddAsync(message);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }


        }
    }
}
