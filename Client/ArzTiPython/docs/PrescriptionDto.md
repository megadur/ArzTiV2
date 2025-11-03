# PrescriptionDto


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**type** | **str** |  | [optional] 
**id** | **int** |  | [optional] 
**rezept_uuid** | **str** |  | [optional] 
**xml_request** | **str** |  | [optional] 
**transfer_arz** | **bool** |  | [optional] 
**rz_liefer_id** | **str** |  | [optional] 
**muster16_id** | **int** |  | [optional] 
**transaktions_nummer** | **int** |  | [optional] 
**erezept_id** | **str** |  | [optional] 
**daten_id** | **int** |  | [optional] 

## Example

```python
from arzti_client.models.prescription_dto import PrescriptionDto

# TODO update the JSON string below
json = "{}"
# create an instance of PrescriptionDto from a JSON string
prescription_dto_instance = PrescriptionDto.from_json(json)
# print the JSON string representation of the object
print(PrescriptionDto.to_json())

# convert the object into a dict
prescription_dto_dict = prescription_dto_instance.to_dict()
# create an instance of PrescriptionDto from a dict
prescription_dto_from_dict = PrescriptionDto.from_dict(prescription_dto_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


