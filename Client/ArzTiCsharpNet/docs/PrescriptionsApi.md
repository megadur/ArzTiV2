# ArzTiClient.Api.PrescriptionsApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApiPrescriptionsMarkAsReadPost**](PrescriptionsApi.md#apiprescriptionsmarkasreadpost) | **POST** /api/Prescriptions/mark-as-read |  |
| [**ApiPrescriptionsNewGet**](PrescriptionsApi.md#apiprescriptionsnewget) | **GET** /api/Prescriptions/new |  |
| [**ApiPrescriptionsSetAbgerechnetPost**](PrescriptionsApi.md#apiprescriptionssetabgerechnetpost) | **POST** /api/Prescriptions/set-abgerechnet |  |

<a id="apiprescriptionsmarkasreadpost"></a>
# **ApiPrescriptionsMarkAsReadPost**
> BulkOperationResponse ApiPrescriptionsMarkAsReadPost (BulkOperationRequest bulkOperationRequest = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using ArzTiClient.Api;
using ArzTiClient.Client;
using ArzTiClient.Model;

namespace Example
{
    public class ApiPrescriptionsMarkAsReadPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure HTTP basic authorization: basic
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PrescriptionsApi(httpClient, config, httpClientHandler);
            var bulkOperationRequest = new BulkOperationRequest(); // BulkOperationRequest |  (optional) 

            try
            {
                BulkOperationResponse result = apiInstance.ApiPrescriptionsMarkAsReadPost(bulkOperationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsMarkAsReadPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiPrescriptionsMarkAsReadPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<BulkOperationResponse> response = apiInstance.ApiPrescriptionsMarkAsReadPostWithHttpInfo(bulkOperationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsMarkAsReadPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **bulkOperationRequest** | [**BulkOperationRequest**](BulkOperationRequest.md) |  | [optional]  |

### Return type

[**BulkOperationResponse**](BulkOperationResponse.md)

### Authorization

[basic](../README.md#basic)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="apiprescriptionsnewget"></a>
# **ApiPrescriptionsNewGet**
> PrescriptionResponse ApiPrescriptionsNewGet (int? page = null, int? pageSize = null, string rezeptType = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using ArzTiClient.Api;
using ArzTiClient.Client;
using ArzTiClient.Model;

namespace Example
{
    public class ApiPrescriptionsNewGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure HTTP basic authorization: basic
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PrescriptionsApi(httpClient, config, httpClientHandler);
            var page = 1;  // int? |  (optional)  (default to 1)
            var pageSize = 100;  // int? |  (optional)  (default to 100)
            var rezeptType = "rezeptType_example";  // string |  (optional) 

            try
            {
                PrescriptionResponse result = apiInstance.ApiPrescriptionsNewGet(page, pageSize, rezeptType);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsNewGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiPrescriptionsNewGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<PrescriptionResponse> response = apiInstance.ApiPrescriptionsNewGetWithHttpInfo(page, pageSize, rezeptType);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsNewGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **page** | **int?** |  | [optional] [default to 1] |
| **pageSize** | **int?** |  | [optional] [default to 100] |
| **rezeptType** | **string** |  | [optional]  |

### Return type

[**PrescriptionResponse**](PrescriptionResponse.md)

### Authorization

[basic](../README.md#basic)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="apiprescriptionssetabgerechnetpost"></a>
# **ApiPrescriptionsSetAbgerechnetPost**
> BulkOperationResponse ApiPrescriptionsSetAbgerechnetPost (BulkOperationRequest bulkOperationRequest = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using ArzTiClient.Api;
using ArzTiClient.Client;
using ArzTiClient.Model;

namespace Example
{
    public class ApiPrescriptionsSetAbgerechnetPostExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure HTTP basic authorization: basic
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            // create instances of HttpClient, HttpClientHandler to be reused later with different Api classes
            HttpClient httpClient = new HttpClient();
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            var apiInstance = new PrescriptionsApi(httpClient, config, httpClientHandler);
            var bulkOperationRequest = new BulkOperationRequest(); // BulkOperationRequest |  (optional) 

            try
            {
                BulkOperationResponse result = apiInstance.ApiPrescriptionsSetAbgerechnetPost(bulkOperationRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsSetAbgerechnetPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiPrescriptionsSetAbgerechnetPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<BulkOperationResponse> response = apiInstance.ApiPrescriptionsSetAbgerechnetPostWithHttpInfo(bulkOperationRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PrescriptionsApi.ApiPrescriptionsSetAbgerechnetPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **bulkOperationRequest** | [**BulkOperationRequest**](BulkOperationRequest.md) |  | [optional]  |

### Return type

[**BulkOperationResponse**](BulkOperationResponse.md)

### Authorization

[basic](../README.md#basic)

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

