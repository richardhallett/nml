nml
===

What is nml?
------------

nml is linear algebra maths library built for .NET and working specifically with 3D space.
I originally created it as a learning exercise for myself and to use in projects that require a 3d maths library. 

There are a few C# libraries that exist however they mostly reside within existing projects, usually with good reason, namely cleaner interop with other code. I however wanted to create one that was standalone.

Current Requirements
--------------------

.NET 4.5

Goals
-----

* Unit tested - All core maths functionality should be unit tested.
* Fast - As fast as I can make it.
* Interoperable - So it can be used with other existing frameworks.

The current state & plans
-------------------------

This is at the moment does not have a stable release, there are still many things I want to add to it namely:

* 64bit versions of Vectors/Matrices/Quaternions.
* SSE optimisations - I have some thoughts about this, however until I'm reasonable happy with the API I'm not going to push too far in this direction
* Better documentation - I'm not generating any documentation at the moment.
* More build targets - At the moment this is compiled against .NET 4.5 which is what I use, however I want to be provide options to build it to various .NET targets and ensure it works also across Mono versions.

**So if you decide to use this in your own projects, be aware it might change.**