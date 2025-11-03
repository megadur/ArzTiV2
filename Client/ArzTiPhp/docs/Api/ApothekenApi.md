# OpenAPI\Client\ApothekenApi

All URIs are relative to http://localhost, except if the operation defines another base path.

| Method | HTTP request | Description |
| ------------- | ------------- | ------------- |
| [**apiApothekenByIkIkNrGet()**](ApothekenApi.md#apiApothekenByIkIkNrGet) | **GET** /api/Apotheken/by-ik/{ikNr} |  |
| [**apiApothekenGet()**](ApothekenApi.md#apiApothekenGet) | **GET** /api/Apotheken |  |
| [**apiApothekenIdDelete()**](ApothekenApi.md#apiApothekenIdDelete) | **DELETE** /api/Apotheken/{id} |  |
| [**apiApothekenIdGet()**](ApothekenApi.md#apiApothekenIdGet) | **GET** /api/Apotheken/{id} |  |
| [**apiApothekenIdPut()**](ApothekenApi.md#apiApothekenIdPut) | **PUT** /api/Apotheken/{id} |  |
| [**apiApothekenPost()**](ApothekenApi.md#apiApothekenPost) | **POST** /api/Apotheken |  |


## `apiApothekenByIkIkNrGet()`

```php
apiApothekenByIkIkNrGet($ik_nr): \OpenAPI\Client\Model\ApothekeDto
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$ik_nr = 56; // int

try {
    $result = $apiInstance->apiApothekenByIkIkNrGet($ik_nr);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenByIkIkNrGet: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **ik_nr** | **int**|  | |

### Return type

[**\OpenAPI\Client\Model\ApothekeDto**](../Model/ApothekeDto.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiApothekenGet()`

```php
apiApothekenGet($page, $page_size, $search): \OpenAPI\Client\Model\ApothekeResponse
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$page = 1; // int
$page_size = 100; // int
$search = 'search_example'; // string

try {
    $result = $apiInstance->apiApothekenGet($page, $page_size, $search);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenGet: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **page** | **int**|  | [optional] [default to 1] |
| **page_size** | **int**|  | [optional] [default to 100] |
| **search** | **string**|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\ApothekeResponse**](../Model/ApothekeResponse.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiApothekenIdDelete()`

```php
apiApothekenIdDelete($id)
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$id = 56; // int

try {
    $apiInstance->apiApothekenIdDelete($id);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenIdDelete: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **id** | **int**|  | |

### Return type

void (empty response body)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiApothekenIdGet()`

```php
apiApothekenIdGet($id): \OpenAPI\Client\Model\ApothekeDto
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$id = 56; // int

try {
    $result = $apiInstance->apiApothekenIdGet($id);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenIdGet: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **id** | **int**|  | |

### Return type

[**\OpenAPI\Client\Model\ApothekeDto**](../Model/ApothekeDto.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: Not defined
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiApothekenIdPut()`

```php
apiApothekenIdPut($id, $update_apotheke_request): \OpenAPI\Client\Model\ApothekeDto
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$id = 56; // int
$update_apotheke_request = new \OpenAPI\Client\Model\UpdateApothekeRequest(); // \OpenAPI\Client\Model\UpdateApothekeRequest

try {
    $result = $apiInstance->apiApothekenIdPut($id, $update_apotheke_request);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenIdPut: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **id** | **int**|  | |
| **update_apotheke_request** | [**\OpenAPI\Client\Model\UpdateApothekeRequest**](../Model/UpdateApothekeRequest.md)|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\ApothekeDto**](../Model/ApothekeDto.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)

## `apiApothekenPost()`

```php
apiApothekenPost($create_apotheke_request): \OpenAPI\Client\Model\ApothekeDto
```



### Example

```php
<?php
require_once(__DIR__ . '/vendor/autoload.php');


// Configure HTTP basic authorization: basic
$config = OpenAPI\Client\Configuration::getDefaultConfiguration()
              ->setUsername('YOUR_USERNAME')
              ->setPassword('YOUR_PASSWORD');


$apiInstance = new OpenAPI\Client\Api\ApothekenApi(
    // If you want use custom http client, pass your client which implements `GuzzleHttp\ClientInterface`.
    // This is optional, `GuzzleHttp\Client` will be used as default.
    new GuzzleHttp\Client(),
    $config
);
$create_apotheke_request = new \OpenAPI\Client\Model\CreateApothekeRequest(); // \OpenAPI\Client\Model\CreateApothekeRequest

try {
    $result = $apiInstance->apiApothekenPost($create_apotheke_request);
    print_r($result);
} catch (Exception $e) {
    echo 'Exception when calling ApothekenApi->apiApothekenPost: ', $e->getMessage(), PHP_EOL;
}
```

### Parameters

| Name | Type | Description  | Notes |
| ------------- | ------------- | ------------- | ------------- |
| **create_apotheke_request** | [**\OpenAPI\Client\Model\CreateApothekeRequest**](../Model/CreateApothekeRequest.md)|  | [optional] |

### Return type

[**\OpenAPI\Client\Model\ApothekeDto**](../Model/ApothekeDto.md)

### Authorization

[basic](../../README.md#basic)

### HTTP request headers

- **Content-Type**: `application/json`, `text/json`, `application/*+json`
- **Accept**: `text/plain`, `application/json`, `text/json`

[[Back to top]](#) [[Back to API list]](../../README.md#endpoints)
[[Back to Model list]](../../README.md#models)
[[Back to README]](../../README.md)
