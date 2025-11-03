# ApothekeResponse


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**apotheken** | [**List[ApothekeDto]**](ApothekeDto.md) |  | 
**total_count** | **int** |  | [optional] 
**page** | **int** |  | [optional] 
**page_size** | **int** |  | [optional] 
**has_next_page** | **bool** |  | [optional] 

## Example

```python
from arzti_client.models.apotheke_response import ApothekeResponse

# TODO update the JSON string below
json = "{}"
# create an instance of ApothekeResponse from a JSON string
apotheke_response_instance = ApothekeResponse.from_json(json)
# print the JSON string representation of the object
print(ApothekeResponse.to_json())

# convert the object into a dict
apotheke_response_dict = apotheke_response_instance.to_dict()
# create an instance of ApothekeResponse from a dict
apotheke_response_from_dict = ApothekeResponse.from_dict(apotheke_response_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


