# MyDynamicXmlBuilder (RC)

TODO

## Install

```
Install-Package MyDynamicXmlBuilder
```

## Documentation
```
using MyDynamicXmlBuilder;
```

````
/*dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user("Kiro Zlatniq", new {
    username = "kiro",
    age = 50
});*/

 dynamic xml = new XmlBuilder();

            xml.Declaration();

            /* xml.user("Kiro Zlatniq", new
             {
                 id = 1,
                 username = "kiro",
                 age = 50,
             });*/


            xml.user(XmlBuilder.Fragment(u => {
                u.firstname("Kiro");
                u.lastname("Zlatnia");
                u.email("kiro@zlatnia.bg");
                u.phone(new { type = "gsm" }, "(985) 555-1234");
            }));

            

            Console.WriteLine(xml.ToString(true));
```

[Another dynamic xml build by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)
