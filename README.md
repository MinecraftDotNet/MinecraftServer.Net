# MinecraftServer.Net
A complete rewrite of the Java edition's Minecraft server on C#, notably with support for multithreading

## Why tho?
Its a trivial question to answer: writting mods / plugins is a pain, because minecraft's codebase is really bad, and combined with the terrible language that is Java, you are into for an awful experience with modding the game. This server has an aim of fixing that, as well as a major issue that's been talked about: performance. Turns out the server uses just a single core of the ones availabe to it. This server has the aim to allocate better the available processing power its been provided, by splitting some of the workload to other threads

## Java compatibility
One of the aims of this project is to be a black box that acts exactly as the Java severs do, but to do its job objectively better. In that case, you could without a trouble connect to this server with your java client, and even connect with a modded client that  supports the packages being sent by the server

## Contribute
This project aint gonna write itself, so any kind of contributions are welcome. Even reccomending new features or reporting bugs is a great way to help me, but even better so - to take your first issue / feature request and make a pull request.
