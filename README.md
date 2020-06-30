# Samp Sharp with Docker project template

## Dependencies
You only need [docker](https://docs.docker.com/get-docker/).

## Quickstart
Build Samp Sharp docker container. 
This command will build gamemode and create container with samp server, SampSharp plugin and gamemode inside.
``` 
$ docker build Gamemode -t sampsharp-server:latest
```

Run server container.
```
$ docker run -p 7777:7777/udp sampsharp-server:latest
```

And you should get SA-MP server with SampSharp gamemode running on port 7777 :)
