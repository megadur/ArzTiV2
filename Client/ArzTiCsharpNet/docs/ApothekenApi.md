# ArzTiClient.Api.ApothekenApi

All URIs are relative to *http://localhost*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApiApothekenByIkIkNrGet**](ApothekenApi.md#apiapothekenbyikiknrget) | **GET** /api/Apotheken/by-ik/{ikNr} |  |
| [**ApiApothekenGet**](ApothekenApi.md#apiapothekenget) | **GET** /api/Apotheken |  |
| [**ApiApothekenIdDelete**](ApothekenApi.md#apiapothekeniddelete) | **DELETE** /api/Apotheken/{id} |  |
| [**ApiApothekenIdGet**](ApothekenApi.md#apiapothekenidget) | **GET** /api/Apotheken/{id} |  |
| [**ApiApothekenIdPut**](ApothekenApi.md#apiapothekenidput) | **PUT** /api/Apotheken/{id} |  |
| [**ApiApothekenPost**](ApothekenApi.md#apiapothekenpost) | **POST** /api/Apotheken |  |

<a id="apiapothekenbyikiknrget"></a>
# **ApiApothekenByIkIkNrGet**
> ApothekeDto ApiApothekenByIkIkNrGet (long ikNr)



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
    public class ApiApothekenByIkIkNrGetExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var ikNr = 789L;  // long | 

            try
            {
                ApothekeDto result = apiInstance.ApiApothekenByIkIkNrGet(ikNr);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenByIkIkNrGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenByIkIkNrGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<ApothekeDto> response = apiInstance.ApiApothekenByIkIkNrGetWithHttpInfo(ikNr);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenByIkIkNrGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **ikNr** | **long** |  |  |

### Return type

[**ApothekeDto**](ApothekeDto.md)

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

<a id="apiapothekenget"></a>
# **ApiApothekenGet**
> ApothekeResponse ApiApothekenGet (int? page = null, int? pageSize = null, string search = null)



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
    public class ApiApothekenGetExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var page = 1;  // int? |  (optional)  (default to 1)
            var pageSize = 100;  // int? |  (optional)  (default to 100)
            var search = "search_example";  // string |  (optional) 

            try
            {
                ApothekeResponse result = apiInstance.ApiApothekenGet(page, pageSize, search);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<ApothekeResponse> response = apiInstance.ApiApothekenGetWithHttpInfo(page, pageSize, search);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **page** | **int?** |  | [optional] [default to 1] |
| **pageSize** | **int?** |  | [optional] [default to 100] |
| **search** | **string** |  | [optional]  |

### Return type

[**ApothekeResponse**](ApothekeResponse.md)

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

<a id="apiapothekeniddelete"></a>
# **ApiApothekenIdDelete**
> void ApiApothekenIdDelete (int id)



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
    public class ApiApothekenIdDeleteExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var id = 56;  // int | 

            try
            {
                apiInstance.ApiApothekenIdDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenIdDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.ApiApothekenIdDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **int** |  |  |

### Return type

void (empty response body)

### Authorization

[basic](../README.md#basic)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="apiapothekenidget"></a>
# **ApiApothekenIdGet**
> ApothekeDto ApiApothekenIdGet (int id)



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
    public class ApiApothekenIdGetExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var id = 56;  // int | 

            try
            {
                ApothekeDto result = apiInstance.ApiApothekenIdGet(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenIdGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<ApothekeDto> response = apiInstance.ApiApothekenIdGetWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **int** |  |  |

### Return type

[**ApothekeDto**](ApothekeDto.md)

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

<a id="apiapothekenidput"></a>
# **ApiApothekenIdPut**
> ApothekeDto ApiApothekenIdPut (int id, UpdateApothekeRequest updateApothekeRequest = null)



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
    public class ApiApothekenIdPutExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var id = 56;  // int | 
            var updateApothekeRequest = new UpdateApothekeRequest(); // UpdateApothekeRequest |  (optional) 

            try
            {
                ApothekeDto result = apiInstance.ApiApothekenIdPut(id, updateApothekeRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdPut: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenIdPutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<ApothekeDto> response = apiInstance.ApiApothekenIdPutWithHttpInfo(id, updateApothekeRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenIdPutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **int** |  |  |
| **updateApothekeRequest** | [**UpdateApothekeRequest**](UpdateApothekeRequest.md) |  | [optional]  |

### Return type

[**ApothekeDto**](ApothekeDto.md)

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

<a id="apiapothekenpost"></a>
# **ApiApothekenPost**
> ApothekeDto ApiApothekenPost (CreateApothekeRequest createApothekeRequest = null)



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
    public class ApiApothekenPostExample
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
            var apiInstance = new ApothekenApi(httpClient, config, httpClientHandler);
            var createApothekeRequest = new CreateApothekeRequest(); // CreateApothekeRequest |  (optional) 

            try
            {
                ApothekeDto result = apiInstance.ApiApothekenPost(createApothekeRequest);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApothekenApi.ApiApothekenPost: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApiApothekenPostWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    ApiResponse<ApothekeDto> response = apiInstance.ApiApothekenPostWithHttpInfo(createApothekeRequest);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApothekenApi.ApiApothekenPostWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **createApothekeRequest** | [**CreateApothekeRequest**](CreateApothekeRequest.md) |  | [optional]  |

### Return type

[**ApothekeDto**](ApothekeDto.md)

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

