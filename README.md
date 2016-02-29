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

var xml = MyXml.Create();

xml.Declaration();

xml.Comment("Someone comment");

xml.Users(XmlBuilder.Section(users => {
    users.Comment("Users");
    users.User(new { Id = 1 }, XmlBuilder.Section(user => {
        user.FirstName("Kiro");
        user.LastName("Zlatnia");
        user.UserName("zlatnia");
        user.Age(50);
        user.Email("kiro@zlatnia.bg");
        user.Phone("089855533");
    }));
}));

Console.WriteLine(xml);
```

* with ````cs using``` keyword - comming in version 3.0.0

* [Another demo](https://github.com/csyntax/Talks/tree/master/DemoMyDynamicXmlBuider)

* [Another dynamic xml builder by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)