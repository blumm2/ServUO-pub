# [ServUO]

[![GitHub issues](https://img.shields.io/github/issues/servuo/servuo.svg)](https://github.com/ServUO/ServUO/issues)
[![GitHub release](https://img.shields.io/github/release/servuo/servuo.svg)](https://github.com/ServUO/ServUO/releases)
[![GitHub repo size](https://img.shields.io/github/repo-size/servuo/servuo.svg)](https://github.com/ServUO/ServUO/)
[![Discord](https://img.shields.io/discord/110970849628000256.svg)](https://discord.gg/0cQjvnFUN26nRt7y)
[![GitHub contributors](https://img.shields.io/github/contributors/servuo/servuo.svg)](https://github.com/ServUO/ServUO/graphs/contributors)
[![GitHub](https://img.shields.io/github/license/servuo/servuo.svg?color=a)](https://github.com/ServUO/ServUO/blob/master/LICENSE)


ServUO is a community driven Ultima Online Server Emulator written in C#.


### Website

[ServUO]


#### Windows

Run `_windebug.bat` for development, attaching a debugger and/or extended output.

Run `_winrelease.bat` for production environment.


#### Other Platforms

Run `make debug` for development, attaching a debugger and/or extended output.

Run `make` or `make release` for production environment. Writing release is optinal by default


### Requirements

The [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) on every platform. The SDK, not just the runtime, is needed to run the server too: scripts are compiled at startup (`Config/Compiler.cfg`). Builds target x64, or ARM64 with `-p:Platform=ARM64`; `make` picks the platform of the machine.

### Linux Dependencies

#### Ubuntu / Debian
```
sudo add-apt-repository ppa:dotnet/backports
sudo apt-get update
sudo apt-get -y install make dotnet-sdk-10.0
```

#### Arch-based
```
sudo pacman -S make dotnet-sdk
```

### Summary

1. Starting with the `/Config` directory, make sure to read the readme first, then find and edit `Server.cfg` to set up the essentials.
2. Go through the remaining `*.cfg` files to ensure they suit your needs.
3. For Windows, run `_winrelease.bat` to produce `ServUO.exe`, OSX/Linux users may run `make`.
4. Run `ServUO`
5. ???
6. Profit!


    [ServUO]: <https://www.servuo.dev>

