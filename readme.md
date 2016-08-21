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

There is currently no stable release and there are still a few things I want to add:

* 64bit versions of Vectors/Matrices/Quaternions.
* SSE optimisations
* Better documentation

**If you decide to use this in your own projects, be aware it might change.**