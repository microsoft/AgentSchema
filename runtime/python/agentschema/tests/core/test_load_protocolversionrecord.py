
import json
import yaml

from agentschema.core import ProtocolVersionRecord


def test_load_json_protocolversionrecord():
    json_data = '''
    {
      "protocol": "responses",
      "version": "v0.1.1"
    }
    '''
    data = json.loads(json_data, strict=False)
    instance = ProtocolVersionRecord.load(data)
    assert instance is not None
    assert instance.protocol == "responses"
    assert instance.version == "v0.1.1"
    

def test_load_yaml_protocolversionrecord():
    yaml_data = '''
    protocol: responses
    version: v0.1.1
    
    '''
    data = yaml.load(yaml_data, Loader=yaml.FullLoader)
    instance = ProtocolVersionRecord.load(data)
    assert instance is not None
    assert instance.protocol == "responses"
    assert instance.version == "v0.1.1"


