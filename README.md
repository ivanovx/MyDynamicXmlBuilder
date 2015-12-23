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