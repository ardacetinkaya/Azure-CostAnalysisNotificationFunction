namespace NotificationFunction
{
    using CostLibrary;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class Notification
    {

        [FunctionName("Notification")]
        public async static Task Run([BlobTrigger("cost/report/{name}", Connection = "StorageConnection")]Stream myBlob, string name, ILogger log)
        {
            
            try
            {
                FileAnalysis fa = new FileAnalysis(log);

                var contents = await fa.ReadFile(myBlob, name);
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
            }
       

        }
    }
}
