# MyDynamicXmlBuilder (RC)

Dynamic XML construction API for .NET

## Install

* Latest Stable version - 1.1.0
```
Install-Package MyDynamicXmlBuilder
```

* Beta version - 2.0.0
```
Install-Package MyDynamicXmlBuilder -Pre
```

## Documentation
```cs
using MyDynamicXmlBuilder;
```

* Basic demo
```cs
dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user("Kiro Zlatniq", new
{
    id = 1,
    username = "kiro",
    age = 50,
});
```

* Another demo
````cs
dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user(XmlBuilder.Fragment(user => {
    user.firstname("Kiro");
	user.lastname("Zlatnia");
    user.username("zlatnia");
    user.age(50);
    user.email("kiro@zlatnia.bg");
    user.phone(new {
            type = "gsm",
            work = true
    }, "089855533");
})); 
	
Console.WriteLine(xml.ToString(true));
```

*[Another demo](https://github.com/csyntax/Talks/tree/master/DemoMyDynamicXmlBuider)

*[Another dynamic xml builder by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)

## TODO
- Declaration -> Declare
- Section -> Fragment
- true of false in ToString

## New features in 2.0.0 RC
* xml.Comment("Someone comment")
* ToString with auto indent (dont use bool opartions in ToString method)