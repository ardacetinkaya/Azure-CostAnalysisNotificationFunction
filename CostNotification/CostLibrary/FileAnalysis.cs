namespace CostLibrary
{
    using CsvHelper;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;

    public class FileAnalysis
    {
        private readonly ILogger _log;

        public FileAnalysis(ILogger log)
        {
            _log = log;
        }

        public async Task<List<CostData>> ReadFile(Stream file, string name)
        {
            List<CostData> costs = null;

            using var strmReader = new StreamReader(file);
            using var csv = new CsvReader(strmReader, CultureInfo.InvariantCulture);
            if (csv.Read())
            {
                costs = new List<CostData>();

                _log.LogInformation("Reading CSV");


                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new CostData
                    {

                        SubscriptionGuid = csv.GetField("SubscriptionGuid"),
                        ResourceGroup = csv.GetField("ResourceGroup"),
                        ResourceLocation = csv.GetField("ResourceLocation"),
                        UsageDateTime = csv.GetField("UsageDateTime"),
                        MeterCategory = csv.GetField("MeterCategory"),
                        MeterSubcategory = csv.GetField("MeterSubcategory"),
                        MeterId = csv.GetField("MeterId"),
                        MeterName = csv.GetField("MeterName"),
                        MeterRegion = csv.GetField("MeterRegion"),
                        UsageQuantity = csv.GetField("UsageQuantity"),
                        ResourceRate = csv.GetField("ResourceRate"),
                        PreTaxCost = csv.GetField("PreTaxCost"),
                        ConsumedService = csv.GetField("ConsumedService"),
                        ResourceType = csv.GetField("ResourceType"),
                        InstanceId = csv.GetField("InstanceId"),
                        Tags = csv.GetField("Tags"),
                        OfferId = csv.GetField("OfferId"),
                        AdditionalInfo = csv.GetField("AdditionalInfo"),
                        ServiceInfo1 = csv.GetField("ServiceInfo1"),
                        ServiceInfo2 = csv.GetField("ServiceInfo2"),
                        ServiceName = csv.GetField("ServiceName"),
                        ServiceTier = csv.GetField("ServiceTier"),
                        Currency = csv.GetField("Currency"),
                        UnitOfMeasure = csv.GetField("UnitOfMeasure"),
                    };
                    costs.Add(record);

                }
            }
            return costs;
        }
    }
}
