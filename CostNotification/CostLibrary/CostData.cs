using System;

namespace CostLibrary
{
    public class CostData
    {
        public string SubscriptionGuid { get; set; }
        public string ResourceGroup    { get; set; }
        public string ResourceLocation { get; set; }
        public DateTime UsageDateTime    { get; set; }
        public string MeterCategory    { get; set; }
        public string MeterSubcategory { get; set; }
        public string MeterId          { get; set; }
        public string MeterName        { get; set; }
        public string MeterRegion      { get; set; }
        public string UsageQuantity    { get; set; }
        public string ResourceRate     { get; set; }
        public decimal PreTaxCost       { get; set; }
        public string ConsumedService  { get; set; }
        public string ResourceType     { get; set; }
        public string InstanceId       { get; set; }
        public string Tags             { get; set; }
        public string OfferId          { get; set; }
        public string AdditionalInfo   { get; set; }
        public string ServiceInfo1     { get; set; }
        public string ServiceInfo2     { get; set; }
        public string ServiceName      { get; set; }
        public string ServiceTier      { get; set; }
        public string Currency         { get; set; }
        public string UnitOfMeasure { get; set; }

        //SubscriptionGuid
        //ResourceGroup
        //ResourceLocation
        //UsageDateTime
        //MeterCategory
        //MeterSubcategory
        //MeterId
        //MeterName
        //MeterRegion
        //UsageQuantity
        //ResourceRate
        //PreTaxCost
        //ConsumedService
        //ResourceType
        //InstanceId
        //Tags
        //OfferId
        //AdditionalInfo
        //ServiceInfo1
        //ServiceInfo2
        //ServiceName
        //ServiceTier
        //Currency
        //UnitOfMeasure

    }
}
