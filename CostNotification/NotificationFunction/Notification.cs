namespace NotificationFunction
{
    using CsvHelper;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;

    public static class Notification
    {
        class CostData
        {
            public string Resource { get; set; }
            public string Cost { get; set; }
            public string Name { get; set; }
            public string ResourceGroup { get; set; }
            public string ConsumedService { get; set; }
            public string ResourceType { get; set; }
        }

        [FunctionName("Notification")]
        public static void Run([BlobTrigger("cost/report/{name}", Connection = "StorageConnection")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            using (var memoryStream = new MemoryStream())
            {
                using (var tr = new StreamReader(myBlob))
                {
                    using (var csv = new CsvReader(tr))
                    {
                        if (csv.Read())
                        {
                            log.LogInformation("Reading CSV");
                            csv.ReadHeader();
                            while (csv.Read())
                            {
                                var record = new CostData
                                {
                                    ConsumedService = csv.GetField("ConsumedService"),
                                    ResourceGroup= csv.GetField("ConsumedService"),
                                    ResourceType= csv.GetField("ConsumedService"),
                                    Cost = csv.GetField("ConsumedService")
                                };
                            }
                        }
                    }
                }
            }
        }
    }
}
