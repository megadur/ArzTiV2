# arzti_client.PrescriptionsApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**api_prescriptions_mark_as_read_post**](PrescriptionsApi.md#api_prescriptions_mark_as_read_post) | **POST** /api/Prescriptions/mark-as-read | 
[**api_prescriptions_new_get**](PrescriptionsApi.md#api_prescriptions_new_get) | **GET** /api/Prescriptions/new | 
[**api_prescriptions_set_abgerechnet_post**](PrescriptionsApi.md#api_prescriptions_set_abgerechnet_post) | **POST** /api/Prescriptions/set-abgerechnet | 


# **api_prescriptions_mark_as_read_post**
> BulkOperationResponse api_prescriptions_mark_as_read_post(bulk_operation_request=bulk_operation_request)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.bulk_operation_request import BulkOperationRequest
from arzti_client.models.bulk_operation_response import BulkOperationResponse
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
    api_instance = arzti_client.PrescriptionsApi(api_client)
    bulk_operation_request = arzti_client.BulkOperationRequest() # BulkOperationRequest |  (optional)

    try:
        api_response = api_instance.api_prescriptions_mark_as_read_post(bulk_operation_request=bulk_operation_request)
        print("The response of PrescriptionsApi->api_prescriptions_mark_as_read_post:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling PrescriptionsApi->api_prescriptions_mark_as_read_post: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **bulk_operation_request** | [**BulkOperationRequest**](BulkOperationRequest.md)|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_prescriptions_new_get**
> PrescriptionResponse api_prescriptions_new_get(page=page, page_size=page_size, rezept_type=rezept_type)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.prescription_response import PrescriptionResponse
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
    api_instance = arzti_client.PrescriptionsApi(api_client)
    page = 1 # int |  (optional) (default to 1)
    page_size = 100 # int |  (optional) (default to 100)
    rezept_type = 'rezept_type_example' # str |  (optional)

    try:
        api_response = api_instance.api_prescriptions_new_get(page=page, page_size=page_size, rezept_type=rezept_type)
        print("The response of PrescriptionsApi->api_prescriptions_new_get:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling PrescriptionsApi->api_prescriptions_new_get: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **page** | **int**|  | [optional] [default to 1]
 **page_size** | **int**|  | [optional] [default to 100]
 **rezept_type** | **str**|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **api_prescriptions_set_abgerechnet_post**
> BulkOperationResponse api_prescriptions_set_abgerechnet_post(bulk_operation_request=bulk_operation_request)

### Example

* Basic Authentication (basic):

```python
import arzti_client
from arzti_client.models.bulk_operation_request import BulkOperationRequest
from arzti_client.models.bulk_operation_response import BulkOperationResponse
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
    api_instance = arzti_client.PrescriptionsApi(api_client)
    bulk_operation_request = arzti_client.BulkOperationRequest() # BulkOperationRequest |  (optional)

    try:
        api_response = api_instance.api_prescriptions_set_abgerechnet_post(bulk_operation_request=bulk_operation_request)
        print("The response of PrescriptionsApi->api_prescriptions_set_abgerechnet_post:\n")
        pprint(api_response)
    except Exception as e:
        print("Exception when calling PrescriptionsApi->api_prescriptions_set_abgerechnet_post: %s\n" % e)
```



### Parameters


Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **bulk_operation_request** | [**BulkOperationRequest**](BulkOperationRequest.md)|  | [optional] 

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
**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

