# MinecraftServer.Net
A complete rewrite of the Java edition's Minecraft server on C#, notably with support for multithreading

## Why tho?
Its a trivial question to answer: writting mods / plugins is a pain, because minecraft's codebase is really bad, and combined with the mediocre and outdated language that is Java, you are into for an awful experience with modding the game. This project (as well as its siblings, MinecraftClient.Net (was Minecraft.Net) and Minecraft.Net (different from old Minecraft.Net)) has an aim of fixing that, as well as a major issue that's been talked about: performance. Turns out the server uses just a single core of the ones availabe to it (except for chat, but that doesn't really help the situation, does it?). This server has the aim to allocate better the available processing power its been provided, by splitting some of the workload to other threads. Effords to make such a thing in Java is being talked about, but for now it's a glitchy and incomplete mess at best (no offence to its creators, they're doing god's work and the project is in its early stage)

## Java compatibility
One of the aims of this project is to act exactly as the Java severs do (but to do its job objectively better and to be able to set it up so it utilises better and more optimised formats and packets). In that case, you could without a trouble connect to this server with your java client, and even connect with a modded client that supports the packages being sent by the server (having a port of your mod for the Minecraft.Net ecosystem that works with the Java ecosystem, ain't that amazing?)

## Future plans
I have an ambitious plan for when this takes its final form: Add Java plugins and mods support. Now, this is going to be quite a bite to chew, but still it's doable, if somehow I figure out how to translate Java Bytecode to CIL (common intermediate language), the .Net bytecode equivalent. That shouldn't be too hard compared to what's next. Plugin support won't be too hard, I'd just need to write a wrapper for my API, but for the other stuff.. AWW boy, modding, even today, is a mess (even with Fabric, which provides only half of the fix). Still, noone is providing full modding API to minecraft and sometimes you need to use injects, which is going tobe a pain to deal with and I might as well not do so (just implement support for the provided API and miserably give up when an injection is detected).

## Contribute
This project aint gonna write itself, so any kind of contributions are welcome. Even reccomending new features or reporting bugs is a great way to help me, but even better so - to take your first issue / feature request and make a pull request.
