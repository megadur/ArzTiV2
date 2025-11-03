# OpenAPI\Client\PrescriptionsApi

All URIs are relative to http://localhost, except if the operation defines another base path.

| Method | HTTP request | Description |
| ------------- | ------------- | ------------- |
| [**apiPrescriptionsMarkAsReadPost()**](PrescriptionsApi.md#apiPrescriptionsMarkAsReadPost) | **POST** /api/Prescriptions/mark-as-read |  |
| [**apiPrescriptionsNewGet()**](PrescriptionsApi.md#apiPrescriptionsNewGet) | **GET** /api/Prescriptions/new |  |
| [**apiPrescriptionsSetAbgerechnetPost()**](PrescriptionsApi.md#apiPrescriptionsSetAbgerechnetPost) | **POST** /api/Prescriptions/set-abgerechnet |  |


## `apiPrescriptionsMarkAsReadPost()`

```php
apiPrescriptionsMarkAsReadPost($bulk_operation_request): \OpenAPI\Client\Model\BulkOperationResponse
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\PrescriptionsApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$bulk_operation_request = new \OpenAPI\Client\Model\BulkOperationRequest(); // \OpenAPI\Client\Model\BulkOperationRequest

try {
    $result = $apiInstance->apiPrescriptionsMarkAsReadPost($bulk_operation_request);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling PrescriptionsApi->apiPrescriptionsMarkAsReadPost: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **bulk_operation_request** | [**\OpenAPI\Client\Model\BulkOperationRequest**](../Model/BulkOperationRequest.md)|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\BulkOperationResponse**](../Model/BulkOperationResponse.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiPrescriptionsNewGet()`

```php
apiPrescriptionsNewGet($page, $page_size, $rezept_type): \OpenAPI\Client\Model\PrescriptionResponse
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\PrescriptionsApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$page = 1; // int
$page_size = 100; // int
$rezept_type = 'rezept_type_example'; // string

try {
    $result = $apiInstance->apiPrescriptionsNewGet($page, $page_size, $rezept_type);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling PrescriptionsApi->apiPrescriptionsNewGet: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **page** | **int**|  | [optional] [default to 1] |
| **page_size** | **int**|  | [optional] [default to 100] |
| **rezept_type** | **string**|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\PrescriptionResponse**](../Model/PrescriptionResponse.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiPrescriptionsSetAbgerechnetPost()`

```php
apiPrescriptionsSetAbgerechnetPost($bulk_operation_request): \OpenAPI\Client\Model\BulkOperationResponse
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\PrescriptionsApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$bulk_operation_request = new \OpenAPI\Client\Model\BulkOperationRequest(); // \OpenAPI\Client\Model\BulkOperationRequest

try {
    $result = $apiInstance->apiPrescriptionsSetAbgerechnetPost($bulk_operation_request);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling PrescriptionsApi->apiPrescriptionsSetAbgerechnetPost: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **bulk_operation_request** | [**\OpenAPI\Client\Model\BulkOperationRequest**](../Model/BulkOperationRequest.md)|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\BulkOperationResponse**](../Model/BulkOperationResponse.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)
