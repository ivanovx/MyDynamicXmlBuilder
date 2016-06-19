# MyDynamicXmlBuilder
Dynamic XML construction API for .NET

## Install

```powershell
Install-Package MyDynamicXmlBuilder
```

## Basic demo
```cs
using (var xml = XmlBuilder.Create())
{
	xml.Declaration();

    xml.user("Kiro Zlatniq", new {
        Id = 1,
        UserName = "kiro",
        Age = 50,
    });

    Console.WriteLine(xml);
}  
```

## Another demo

````cs
using (var xml = XmlBuilder.Create())
{
    xml.Declaration();

    xml.Comment("Someone comment");

    xml.Users(XmlBuilder.Section(users => {
        users.Comment("Users");
        users.User(new { Id = 1 }, XmlBuilder.Section(user => {
            user.Comment("User");
            user.FirstName("Kiro");
            user.LastName("Zlatnia");
            user.UserName("zlatnia");
            user.Age(50);
            user.Email("kiro@zlatnia.bg");
            user.Phone("089855533");
        }));
    }));

	Console.WriteLine(xml);
}
```

## For .NET Core
* [MyDynamicXmlBuilder for .NET Core](https://github.com/csyntax/MyDynamicXmlBuilder.DotNetCore)