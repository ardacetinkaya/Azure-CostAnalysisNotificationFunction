namespace CostLibrary
{
    using CsvHelper;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class FileAnalysis
    {
        private readonly ILogger _log;
        private List<CostData> _fileContent = null;

        public FileAnalysis(ILogger log)
        {
            _log = log;
        }

        public async Task<List<CostData>> ReadFile(Stream file, string name)
        {
            try
            {
                using var strmReader = new StreamReader(file);
                using var csv = new CsvReader(strmReader, CultureInfo.InvariantCulture);
                if (csv.Read())
                {
                    _fileContent = new List<CostData>();

                    _log.LogInformation("Reading CSV");


                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = new CostData
                        {

                            SubscriptionGuid = csv.GetField("SubscriptionGuid"),
                            ResourceGroup = csv.GetField("ResourceGroup"),
                            ResourceLocation = csv.GetField("ResourceLocation"),
                            UsageDateTime = Convert.ToDateTime(csv.GetField("UsageDateTime")),
                            MeterCategory = csv.GetField("MeterCategory"),
                            MeterSubcategory = csv.GetField("MeterSubcategory"),
                            MeterId = csv.GetField("MeterId"),
                            MeterName = csv.GetField("MeterName"),
                            MeterRegion = csv.GetField("MeterRegion"),
                            UsageQuantity = csv.GetField("UsageQuantity"),
                            ResourceRate = csv.GetField("ResourceRate"),
                            PreTaxCost = decimal.Parse(csv.GetField("PreTaxCost")),
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
                        _fileContent.Add(record);

                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"Unable to parse file. {ex.Message}");
            }

            return _fileContent;
        }

        public void AnalyseCost()
        {
            if(_fileContent!=null)
            {
                
            }
        }
    }
}
