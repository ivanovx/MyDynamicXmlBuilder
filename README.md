# MyDynamicXmlBuilder

[![NuGet version](https://badge.fury.io/nu/MyDynamicXmlBuilder.svg)](https://badge.fury.io/nu/MyDynamicXmlBuilder)

[![Build status](https://ci.appveyor.com/api/projects/status/ilklj1gfluf4l0dp?svg=true)](https://ci.appveyor.com/project/IvanIvanov/mydynamicxmlbuilder)

> Dynamic XML construction API for .NET

## Requirements
* .NET Framework 4.5.2

## Installation
- Install from Nuget `Install-Package MyDynamicXmlBuilder`

## Examples!

* Nodes

```c#
var xml = XmlBuilder.Create();

xml.foo("bar");

Console.WriteLine(xml.Build());
```

* Attributes

```c#
var xml = XmlBuilder.Create();

xml.user("Kiro", new { username = "zlatnia", age = 50 });

Console.WriteLine(xml.Build());
```

* Nesting via delegates

```c#
var xml = XmlBuilder.Create();

xml.user(XmlBuilder.Section(u => {
    u.firstname("Kiro");
    u.lastname("Kirilov");
    u.username("jdoe@example.org");
}));

Console.WriteLine(xml.Build());
```
