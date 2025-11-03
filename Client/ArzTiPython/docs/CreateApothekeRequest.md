# CreateApothekeRequest


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**apotheke_name** | **str** |  | 
**apotheke_name_zusatz** | **str** |  | [optional] 
**apo_ik_nr** | **int** |  | [optional] 
**inhaber_vorname** | **str** |  | [optional] 
**inhaber_nachname** | **str** |  | [optional] 
**apo_int_nr** | **int** |  | [optional] 
**plz** | **int** |  | [optional] 
**ort** | **str** |  | [optional] 
**strasse** | **str** |  | [optional] 
**email** | **str** |  | [optional] 
**telefon** | **str** |  | [optional] 
**mobil** | **str** |  | [optional] 
**fax** | **str** |  | [optional] 
**bemerkung** | **str** |  | [optional] 
**bundesland** | **str** |  | [optional] 
**mandant_type** | **str** |  | [optional] 
**id_le_type** | **int** |  | [optional] 
**id_hauptapotheke** | **int** |  | [optional] 
**id_ht_anrede** | **int** |  | [optional] 
**filialapotheke** | **int** |  | [optional] 
**gesperrt** | **bool** |  | [optional] 
**sec_login** | **int** |  | [optional] 
**sec_login_werte** | **str** |  | [optional] 
**sec_login_nur_apo_user** | **bool** |  | [optional] 

## Example

```python
from arzti_client.models.create_apotheke_request import CreateApothekeRequest

# TODO update the JSON string below
json = "{}"
# create an instance of CreateApothekeRequest from a JSON string
create_apotheke_request_instance = CreateApothekeRequest.from_json(json)
# print the JSON string representation of the object
print(CreateApothekeRequest.to_json())

# convert the object into a dict
create_apotheke_request_dict = create_apotheke_request_instance.to_dict()
# create an instance of CreateApothekeRequest from a dict
create_apotheke_request_from_dict = CreateApothekeRequest.from_dict(create_apotheke_request_dict)
```
[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)


