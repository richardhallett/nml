nml
===

What is nml?
------------

nml is linear algebra maths library built for .NET, mainly intended for projects that require a 3d maths library. 

Current Requirements
--------------------

.NET 4.5

Goals
-----

* Unit tested - All core maths functionality should be unit tested.
* Fast - As fast as I can make it.
* Interoperable - So it can be used with other existing frameworks.

Building
-----

## Windows

1. Load VS2015
2. Load the nml.sln - Build

Testing:

1. nuget restore
2. Xunit visual studio runner is included so tests should just work.

## Linux

1. Ensure you have the latest version of mono installed > 4
2. Build
	
	xbuild nml.sln

Testing:

1. Get a copy of nuget
	
	wget http://dist.nuget.org/win-x86-commandline/latest/nuget.exe -P ~/

3. Run restore for nuget
	
	mono ~/nuget.exe restore

4. Run the tests
	
	mono packages/xunit.runner.console.2.1.0/tools/xunit.console.exe nml.tests/bin/Debug/nml.tests.dll


The current state & plans
-------------------------

There is currently no stable release and there are still a few things I want to add:

* 64bit versions of Vectors/Matrices/Quaternions.
* SSE optimisations
* Better documentation
* More build targets

**If you decide to use this in your own projects, be aware it might change.**