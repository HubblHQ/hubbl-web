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
