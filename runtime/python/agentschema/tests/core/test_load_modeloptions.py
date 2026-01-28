
import json
import yaml

from agentschema.core import ModelOptions


def test_load_json_modeloptions():
    json_data = '''
    {
      "frequencyPenalty": 0.5,
      "maxOutputTokens": 2048,
      "presencePenalty": 0.3,
      "seed": 42,
      "temperature": 0.7,
      "topK": 40,
      "topP": 0.9,
      "stopSequences": [
        "\n",
        "###"
      ],
      "allowMultipleToolCalls": true,
      "additionalProperties": {
        "customProperty": "value",
        "anotherProperty": "anotherValue"
      }
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ModelOptions.load(data)
    assert instance is not None
    assert instance.frequencyPenalty == 0.5
    assert instance.maxOutputTokens == 2048
    assert instance.presencePenalty == 0.3
    assert instance.seed == 42
    assert instance.temperature == 0.7
    assert instance.topK == 40
    assert instance.topP == 0.9
    
    assert instance.allowMultipleToolCalls
    

def test_load_yaml_modeloptions():
    yaml_data = '''
    frequencyPenalty: 0.5
    maxOutputTokens: 2048
    presencePenalty: 0.3
    seed: 42
    temperature: 0.7
    topK: 40
    topP: 0.9
    stopSequences:
      - |+
        
      - "###"
    allowMultipleToolCalls: true
    additionalProperties:
      customProperty: value
      anotherProperty: anotherValue
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ModelOptions.load(data)
    assert instance is not None
    assert instance.frequencyPenalty == 0.5
    assert instance.maxOutputTokens == 2048
    assert instance.presencePenalty == 0.3
    assert instance.seed == 42
    assert instance.temperature == 0.7
    assert instance.topK == 40
    assert instance.topP == 0.9
    assert instance.allowMultipleToolCalls


