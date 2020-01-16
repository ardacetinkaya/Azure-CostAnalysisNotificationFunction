provider "azurerm" {
  version = "=1.38.0"
  subscription_id = ""
  client_id       = ""
  client_secret   = ""
  tenant_id       = ""
}

resource "azurerm_resource_group" "costmanagement" {
  name     = "azure-costmanagement-resources"
  location = "West Europe"
}

resource "azurerm_storage_account" "costmanagement" {
  name                     = "costfunctionstorage"
  resource_group_name      = "${azurerm_resource_group.costmanagement.name}"
  location                 = "${azurerm_resource_group.costmanagement.location}"
  account_kind             = "StorageV2"
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "costmanagement" {
  name                = "azure-costmanagement-service-plan"
  location            = "${azurerm_resource_group.costmanagement.location}"
  resource_group_name = "${azurerm_resource_group.costmanagement.name}"
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "costmanagement" {
  name                      = "azure-costmanagement-functions"
  location                  = "${azurerm_resource_group.costmanagement.location}"
  resource_group_name       = "${azurerm_resource_group.costmanagement.name}"
  app_service_plan_id       = "${azurerm_app_service_plan.costmanagement.id}"
  storage_connection_string = "${azurerm_storage_account.costmanagement.primary_connection_string}"
  site_config{
      websockets_enabled="true"
      http2_enabled="true"
  }
}