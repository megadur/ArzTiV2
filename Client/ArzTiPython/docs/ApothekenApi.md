# arzti_client.ApothekenApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**api_apotheken_by_ik_ik_nr_get**](ApothekenApi.md#api_apotheken_by_ik_ik_nr_get) | **GET** /api/Apotheken/by-ik/{ikNr} | 
[**api_apotheken_get**](ApothekenApi.md#api_apotheken_get) | **GET** /api/Apotheken | 
[**api_apotheken_id_delete**](ApothekenApi.md#api_apotheken_id_delete) | **DELETE** /api/Apotheken/{id} | 
[**api_apotheken_id_get**](ApothekenApi.md#api_apotheken_id_get) | **GET** /api/Apotheken/{id} | 
[**api_apotheken_id_put**](ApothekenApi.md#api_apotheken_id_put) | **PUT** /api/Apotheken/{id} | 
[**api_apotheken_post**](ApothekenApi.md#api_apotheken_post) | **POST** /api/Apotheken | 


# **api_apotheken_by_ik_ik_nr_get**
> ApothekeDto api_apotheken_by_ik_ik_nr_get(ik_nr)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.apotheke_dto import ApothekeDto
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    ik_nr = 56 # int | 

    try:
        api_response = api_instance.api_apotheken_by_ik_ik_nr_get(ik_nr)
        print("The response of ApothekenApi->api_apotheken_by_ik_ik_nr_get:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_by_ik_ik_nr_get: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **ik_nr** | **int**|  | 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_apotheken_get**
> ApothekeResponse api_apotheken_get(page=page, page_size=page_size, search=search)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.apotheke_response import ApothekeResponse
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    page = 1 # int |  (optional) (default to 1)
    page_size = 100 # int |  (optional) (default to 100)
    search = 'search_example' # str |  (optional)

    try:
        api_response = api_instance.api_apotheken_get(page=page, page_size=page_size, search=search)
        print("The response of ApothekenApi->api_apotheken_get:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_get: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 1]
 **page_size** | **int**|  | [optional] [default to 100]
 **search** | **str**|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_apotheken_id_delete**
> api_apotheken_id_delete(id)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    id = 56 # int | 

    try:
        api_instance.api_apotheken_id_delete(id)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_id_delete: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int**|  | 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_apotheken_id_get**
> ApothekeDto api_apotheken_id_get(id)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.apotheke_dto import ApothekeDto
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    id = 56 # int | 

    try:
        api_response = api_instance.api_apotheken_id_get(id)
        print("The response of ApothekenApi->api_apotheken_id_get:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_id_get: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int**|  | 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_apotheken_id_put**
> ApothekeDto api_apotheken_id_put(id, update_apotheke_request=update_apotheke_request)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.apotheke_dto import ApothekeDto
from arzti_client.models.update_apotheke_request import UpdateApothekeRequest
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    id = 56 # int | 
    update_apotheke_request = arzti_client.UpdateApothekeRequest() # UpdateApothekeRequest |  (optional)

    try:
        api_response = api_instance.api_apotheken_id_put(id, update_apotheke_request=update_apotheke_request)
        print("The response of ApothekenApi->api_apotheken_id_put:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_id_put: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int**|  | 
 **update_apotheke_request** | [**UpdateApothekeRequest**](UpdateApothekeRequest.md)|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_apotheken_post**
> ApothekeDto api_apotheken_post(create_apotheke_request=create_apotheke_request)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.apotheke_dto import ApothekeDto
from arzti_client.models.create_apotheke_request import CreateApothekeRequest
from arzti_client.rest import ApiException
from pprint import pprint

# Defining the host is optional and defaults to http://localhost
# See configuration.py for a list of all supported configuration parameters.
configuration = arzti_client.Configuration(
    host = "http://localhost"
)

# The client must configure the authentication and authorization parameters
# in accordance with the API server security policy.
# Examples for each auth method are provided below, use the example that
# satisfies your auth use case.

# Configure HTTP basic authorization: basic
configuration = arzti_client.Configuration(
    username = os.environ["USERNAME"],
    password = os.environ["PASSWORD"]
)

# Enter a context with an instance of the API client
with arzti_client.ApiClient(configuration) as api_client:
    # Create an instance of the API class
    api_instance = arzti_client.ApothekenApi(api_client)
    create_apotheke_request = arzti_client.CreateApothekeRequest() # CreateApothekeRequest |  (optional)

    try:
        api_response = api_instance.api_apotheken_post(create_apotheke_request=create_apotheke_request)
        print("The response of ApothekenApi->api_apotheken_post:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling ApothekenApi->api_apotheken_post: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **create_apotheke_request** | [**CreateApothekeRequest**](CreateApothekeRequest.md)|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

