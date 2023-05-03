![An image of an orange triangle in a gnome window, behind it a console window is seen](https://imgur.com/gallery/eXygBIc)

# What is SimpleOpenTK
This project is a basic setup of using [OpenTK](https://opentk.net/) with .NET 6 in a way that a developer can easily build both Windows and Linux binaries, without any platform specific code or compiler directives.

# Why is it on GitHub
It's put on GitHub partly as a proof-of-concept, but mainly because it provides me with a decent template for console applications using .NET that have some niceties that come out-of-the-box with ASP.NET web applications, such as dependency injection and the use of IConfiguration or the Options pattern and such.

# What is the future of this project
This project will serve as my personal template for console based projects, as well as a proof of concept for how easy it is to start multiplatform 3D game development using .NET.

While that is said, I have no plans to extend my integration with OpenTK, or build anything more substantial in this repository. That's not to say I won't, I just have no plans to at this time.

# How to build and run it
Simply open the solution in Visual Studio 2022 and hit run to get a nice orange triangle, rendered using OpenGL through OpenTK.

## Linux builds
The publish profile for Linux in this repo assumes a WSL (Windows Subsystem for Linux) installation with a particular user, namely my own. If you want to build a Linux binary from windows using this profile, be sure to update the target path first.

Once setup, you can run and debug your code from a Windows system, and with a single button build a Linux binary that you can immediately run, using WSLg!

We are truly living in the future

# Closing comments
If this repository is helpful, or interesting, feel free to star, watch, or create appropriate issues. While I can not guarantee any level of interaction on my part, it's nice to know that ones work is helpful or appreciated by someone else. 
