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

You will need Monodevelop to build this project. Possibly it can be builded with Visual Studio, not tested. If so, please be respectful and do not push VS-related code and configuration files and PLEASE do no use libraries or classes which is not compatible with Mono. Also please notice it is very important to follow the case of filenames in this project even if your code is working good without it.

First, you may have wanted to setup your workaround.

For Ubuntu and Debian, please do the folowing to install Mono, Monodevelop and xsp4:
```
sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
sudo apt-get update
sudo apt-get install mono-complete monodevelop mono-xsp4
```
For more info please visit http://www.mono-project.com/docs/getting-started/install/linux/.

You may need Nancy and Nancy.Hosting.Self libraries, you can find them in /misc in this repository.

## How to run

There is some problems with running project with Monodevelop yet. But you can still run it by command line.

In the root of your project please type (in case you building the project in debug mode):
```
mono hubbl-web/bin/Debug/hubbl-web.exe
```

## How to deploy

Copy-pasted from https://github.com/NancyFx/Nancy/wiki/Hosting-Nancy-with-Nginx-on-Ubuntu.

To install project on Linux, grab anything from Bin/Release or Debug folder and copy it to any place you want on Linux machine, but make sure that assets folder ends up on  /var/hubbl/assets/ for making the nginx config below to work. For the following we assume that you copied all of it to /var/hubbl

### Install nginx

Nginx is the webserver we're using. We configure it to forward all requests to the Nancy self hosted application. The content folder with static files will be handled by nginx.

    $ sudo apt-get install nginx

Create the website configuration file in /etc/nginx/sites-available/hubbl with the following content. The server_name is the domain on which the request will be handled. Change this to your own value.

```
    server {
        listen       80;
        server_name  hubbl.com;
        root /var/hubbl;

        location /Content/ {
            alias /var/hubbl/assets/;
            location ~*  \.(jpg|jpeg|png|gif|ico|css|js|ttf)$ {
                expires 365d;
            }
        }

        location / {
                proxy_pass http://127.0.0.1:8080;
        }
    }
```

This lets your server forward any requests send to port 80 to your running nancy instance listening on port 8080 with the exception of static content that is requested from /assets/


To enable the website, create a symbolic link from the sites-available to the sites-enabled folder. This will make it easy to temporary disable sites in the future.

    $ sudo ln -s /etc/nginx/sites-available/hubbl /etc/nginx/sites-enabled/hubbl

The configuration is completed, reload Nginx to apply.

    $ sudo /etc/init.d/nginx reload
