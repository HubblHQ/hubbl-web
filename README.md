# HubblWeb

This is web site and API for Hubbl project

## Specification
Every ok response:
```
{ status, result: { ... } }
```

Every error response:
```
{ status, code, msg }
```

Available for all:
```
/signin : { login, password } -> { id, name, sessionToken }
/signup : { name, login, password } -> {}
```

Available for authenticated:
```
/hubs : {} -> { hubs: [ { id, name, author }, ... ] }
/hubs/search : { query } -> { hubs: [ { id, name, author }, ... ] }
/hub : { id } -> { name, author }
/hub/connect : { id } -> {}
/hub/new : { name } -> { id }
```

Available for connected:
```
/hub/delete : {} -> {}
/hub/users : { id } -> { users: [ { id, name }, ... ] }

/user : { id } -> { name }

/playlist : {} -> { playlist: [ ... ] }
/playlist/full : {} -> { playlist: [ ... ] }
/track : { id } -> { ... }
/track/like : { id } -> {}
/track/dislike : { id } -> {}
/player : {} -> { playPos, state, volume }
/player/update : { playPos, state, volume } -> {}
/player/next : {} -> {}
```

## How to build

You will need Monodevelop to build this project. Possibly it can be builded with Visual Studio, not tested. If so, please be respectful and do not push VS-related code and configuration files and PLEASE do no use libraries or classes which is not compatible with Mono.

First, you may have wanted to setup your workaround.
For Ubuntu and Debian, please do the folowing to install Mono, Monodevelop and xsp4:
```
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
sudo apt-get update
sudo apt-get install mono-complete monodevelop=5.10.0.871-0xamarin2 mono-xsp4
```
For more info please visit http://www.mono-project.com/docs/getting-started/install/linux/.

You may need Nancy and Nancy.Hosting.Self libraries, you can find them in /misc in this repository.
