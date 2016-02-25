# MyDynamicXmlBuilder

Dynamic XML construction API for .NET

## Install
```powershell
Install-Package MyDynamicXmlBuilder
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

* [Another demo](https://github.com/csyntax/Talks/tree/master/DemoMyDynamicXmlBuider)

* [Another dynamic xml builder by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)

## TODO
- Declaration -> Declare - version 2.1.1
- Section -> Fragment - version 2.1.1
- true of false in ToString - version 2.1.1

## New features in 2.0.0 RC
* xml.Comment("Someone comment")
* ToString with auto indent (dont use bool opartions in ToString method)

## Comming version 2.1.1

````cs
using(dynamic xml = new XmlBuilder())
{
	xml.Declaration();
}
```