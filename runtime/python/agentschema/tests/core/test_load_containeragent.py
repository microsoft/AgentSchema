
import json
import yaml

from agentschema.core import ContainerAgent


def test_load_json_containeragent():
    json_data = '''
    {
      "kind": "hosted",
      "protocols": [
        {
          "protocol": "responses",
          "version": "v0.1.1"
        }
      ],
      "environmentVariables": [
        {
          "name": "MY_ENV_VAR",
          "value": "my-value"
        }
      ]
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ContainerAgent.load(data)
    assert instance is not None
    assert instance.kind == "hosted"
    

def test_load_yaml_containeragent():
    yaml_data = '''
    kind: hosted
    protocols:
      - protocol: responses
        version: v0.1.1
    environmentVariables:
      - name: MY_ENV_VAR
        value: my-value
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ContainerAgent.load(data)
    assert instance is not None
    assert instance.kind == "hosted"


