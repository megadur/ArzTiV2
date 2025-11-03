# PrescriptionIdentifier


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**type** | **str** |  | [optional] 
**rezept_uuid** | **str** |  | [optional] 

## Example

```python
from arzti_client.models.prescription_identifier import PrescriptionIdentifier

# TODO update the JSON string below
json = "{}"
# create an instance of PrescriptionIdentifier from a JSON string
prescription_identifier_instance = PrescriptionIdentifier.from_json(json)
# print the JSON string representation of the object
print(PrescriptionIdentifier.to_json())

# convert the object into a dict
prescription_identifier_dict = prescription_identifier_instance.to_dict()
# create an instance of PrescriptionIdentifier from a dict
prescription_identifier_from_dict = PrescriptionIdentifier.from_dict(prescription_identifier_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


