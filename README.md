# MyDynamicXmlBuilder
Dynamic XML construction API for .NET

#Migartion
* Migartion started, we are ported MyDynamicXmlBuilder on .NET Framework, .NET Core and Xamarin.
* Like use the style for development and like new name 'DynamicXmlBuilder'
* Cross-Platform, like porting on .NET Core and Xamarin
* Support the Windows, Linux, Mac, Win 10, iOS, Android

## Install

```powershell
Install-Package MyDynamicXmlBuilder
```

```powershell
Install-Package DynamicXmlBuilder -Pre
```

* If prefer to use MyDynamicXmlBuilder, works on Win only

## Basic demo
```cs
using (var xml = XmlBuilder.Create())
{
	xml.Declaration();

    xml.user("Kiro Zlatniq", new 
	{
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

    xml.Users(XmlBuilder.Section(users => 
	{
        users.Comment("Users");
        users.User(new { Id = 1 }, XmlBuilder.Section(user => 
		{
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