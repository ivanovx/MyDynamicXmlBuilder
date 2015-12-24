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

*[Another demo](https://github.com/csyntax/Talks/tree/master/DemoMyDynamicXmlBuider)
*[Another dynamic xml builder by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)

## TODO
- Declaration -> Declare
- Section -> Fragment
- true of false in ToString

*removed methods or make private
	* 23-12-2015 - TryInvokeMember is added
	* 24-12-2015 - TryInvokeMember - find and remove bug
	* 24-12-2015 - Tag and Text members are private

* New features in 2.0.0
* 23-12-2015 add xml comment - xml.Comment("Someone comment")