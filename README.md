![Thumbnail](https://github.com/maxvone/planes-battle/assets/60828878/155f0e4e-e29a-4cf3-a0e4-82142c911bea)
## Used Packages
- DOTween
- UniTask
- Addressables

---

This project is based on Component and Service architecture.

The flow of the game starts in the ```GameBootstrap``` class that creates ```Game State Machine```. The Game State machine's goal is to control the flow of the game. The State machine pattern comes in handy when we need to sequentially do some things. In terms of this game basic states are:
- Bootstrap State (The state that registers services in Service Locator)
- LoadScene State (The state that instantiates and constructs an object that we need in the game)
- GameLoop
<img width="800" alt="image" src="https://github.com/maxvone/coin-runner/assets/60828878/46d1f31c-b9ed-4f77-9c37-66f52bb9be55">

Another important thing to discuss is the ```Service Locator``` pattern. It's the Dependency Injection implementation. I thought importing Zenject or some kind of DI framework would be bloated for such a small project, so I've decided to use the manual-written one.
The game uses the concept of ```Services```. Services are objects that handle one responsibility of a game. For example in the game you will find:
- Input Service
- Game Factory Service
- Asset Provider Service
- etc.

You can look at the work of service mostly in GameFactory, which is the Service that creates objects for the game and constructs them (passing them needed reference e.g.)
