import requests
from brian_pb2 import BrianResponse, BrianRequest

req = BrianRequest(name = "Brian")

r = requests.post('http://localhost:31149/', data = req.SerializeToString())

br = BrianResponse()
br.ParseFromString(r.content)

print("name: %s" % br.name)
print("message: %s" % br.message)
