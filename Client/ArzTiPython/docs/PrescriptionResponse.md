# PrescriptionResponse


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**prescriptions** | [**List[PrescriptionDto]**](PrescriptionDto.md) |  | [optional] 
**total_count** | **int** |  | [optional] 
**page** | **int** |  | [optional] 
**page_size** | **int** |  | [optional] 
**has_next_page** | **bool** |  | [optional] 

## Example

```python
from arzti_client.models.prescription_response import PrescriptionResponse

# TODO update the JSON string below
json = "{}"
# create an instance of PrescriptionResponse from a JSON string
prescription_response_instance = PrescriptionResponse.from_json(json)
# print the JSON string representation of the object
print(PrescriptionResponse.to_json())

# convert the object into a dict
prescription_response_dict = prescription_response_instance.to_dict()
# create an instance of PrescriptionResponse from a dict
prescription_response_from_dict = PrescriptionResponse.from_dict(prescription_response_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


