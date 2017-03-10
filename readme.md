nml
===

What is nml?
------------

nml is linear algebra maths library built for .NET, mainly intended for projects that require a 3d maths library. 

Goals
-----

* Unit tested - All core maths functionality should be unit tested.
* Fast - As fast as I can make it.
* Interoperable - So it can be used with other existing frameworks.

Building
-----

## Visual Studio 2015

1. Load VS2015
2. Load the nml.sln - Build

Testing:

XUnit should be acquired via nuget and in built test runner should work.

## dotnet CLI

1. cd nml
2. dotnet restore
3. dotnet build

The current state & plans
-------------------------

The current release 1.0.0 is reasonably stable as it suits my own use cases, at some point I want to look at adding:

* 64bit versions of Vectors/Matrices/Quaternions.
* SSE optimisations
* Better documentation
