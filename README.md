# MyDynamicXmlBuilder (Beta)

Dynamic XML construction API for .NET

## Install

* Stable version - 1.1.0
```
Install-Package MyDynamicXmlBuilder
```

* Beta version - 2.0.0
```
Install-Package MyDynamicXmlBuilder -Pre
```

## Documentation
```
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


* 23-12-2015 - TryInvokeMember is added
* 23-12-2015 - feature for make comment
* 24-12-2015 - TryInvokeMember - find and remove bug
* 24-12-2015 - Tag and Text members are private
* 24-12-2015 - new ToString method with out bool operations
* 24-12-2015 - ToString(bool indent) are private

* New features in 2.0.0
* 23-12-2015 add xml comment - xml.Comment("Someone comment")