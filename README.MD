# Simple dotnet and REDIS client.

The idea behid this is to achive the following:

* Create a Producer
* Create a Consumer

Producer will create a protobuf message and push it into REDIS (RPUSH). Consumer will dequeue from REDIS (RPOP) deserialize and we are all golden.

There is a docker-compose file containing redis and redis-commander.
