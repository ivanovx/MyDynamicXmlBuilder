# MyDynamicXmlBuilder (Beta)

Dynamic XML construction API for .NET

## Install

```
Install-Package MyDynamicXmlBuilder
```

```
Install-Package MyDynamicXmlBuilder -Pre
```

## Documentation
```
using MyDynamicXmlBuilder;
```

````
dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user(XmlBuilder.Fragment(u => {
    u.firstname("Kiro");
    u.lastname("Zlatnia");
    u.email("kiro@zlatnia.bg");
    u.phone(new { type = "gsm" }, "(985) 555-1234");
}));
	
Console.WriteLine(xml.ToString(true));
```

[Another dynamic xml build by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)

## TODO
* Declaration -> Declare
* Section -> Fragment
* true of false in ToString
* add license
* add summary
* add tutorial

*removed methods or make private
* 23-12-2015 - TryInvokeMember is removed
* todo Tag and Text members are partital removed

* New features in 2.0.0
* 23-12-2015 add xml comment - xml.Comment("Someone comment")