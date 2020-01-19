# Azure-CostAnalysisNotificationFunction
 
With Azure Cost Analysis, it is easy to see costs of resources. Also Azure Cost Analysis data can be exported to Azure Storage as scheduled.

This repo is a demostration for the Azure Function which is triggered by BLOB Storage. The function takes the cost data from BLOB storage, processes the data and then sends the results as an e-mail.

This is just for some simple scenarios such as; proccess the cost data, share the cost information, follow-up the usage...etc.
<p align="center">
   <img src="https://github.com/ardacetinkaya/Azure-CostAnalysisNotificationFunction/blob/master/Images/AzureCostAnalysis.png" data-canonical-src="https://github.com/ardacetinkaya/Azure-CostAnalysisNotificationFunction/blob/master/Images/AzureCostAnalysis.png" height="400" />
</p>

Beside this simple implementation, there is also some other options to touch costs in Azure;
- <a href="https://docs.microsoft.com/en-us/azure/cost-management-billing/manage/usage-rate-card-overview" target="_blank">Azure Billing REST API</a> 
- <a href="https://docs.microsoft.com/en-us/azure/cost-management-billing/manage/consumption-api-overview" target="_blank">Azure Consumption REST API</a> 
- <a href="https://docs.microsoft.com/en-us/cli/azure/billing?view=azure-cli-latest" target="_blank">az CLI</a> 

might work for you for more sophisticated scenarios. But these options are kind of a preview state and changes a lot and they are not so reliable so far. At least, they did not make me feel safe 😀

