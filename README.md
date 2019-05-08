[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/Smidgens/Unity-Asset-Events/master/LICENSE)


# Unity Asset Events

This plugin lets you define persistent events and register listeners to their invocation. Listeners can be registered either at runtime (static), or offline (persistent). Offline listeners, i.e, when no scene is running, are useful for propagating the event call and arguments sent to it to other persistent assets like audio mixers or post processing profiles (for example, setting pitch/volume). Runtime listeners are meant to be used more dynamically, and are thus used in combination with a Listener script. 


| Event |
| ------------- |
| ![](/Screenshots/02.png?raw=true "") |
| ![](/Screenshots/01.png?raw=true "")|


## Execution Order

The order in which listeners are invoked - persistent vs static - can be managed. This can be leveraged to guarantee that scene listeners receive the call before persistent ones or vice versa (for example, initializing a scene state before persistent listeners process scene related information.)





| Scene Listener |
| ------------- |
| ![](/Screenshots/03.png?raw=true "")|
