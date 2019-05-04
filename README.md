# Unity Asset Events

This plugin lets you define persistent events and register listeners to their invocation. Listeners can be registered either at runtime (static), or offline (persistent). Offline listeners, i.e, when no scene is running, are useful for propagating the event call and arguments send to it to other persistent assets like audio mixers or post processing profiles (for example, setting pitch/volume). Online listeners are meant to be used more dynamically, and are thus used in combination with a Listener script. 


| Asset |
| ------------- |
| ![](/Screenshots/02.png?raw=true "") |


## Execution Order

The order in which listeners are invoked - persistent vs static - can be managed. This can be leveraged to guarantee that scene listeners receive the call before persistent ones or vice versa (for example, initializing a scene state before persistent listeners process scene related information.)


| Asset Inspector |
| ------------- |
| ![](/Screenshots/01.png?raw=true "")|


| Scene Listener |
| ------------- |
| ![](/Screenshots/03.png?raw=true "")|
