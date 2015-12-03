# MyDynamicXmlBuilder

This project is inspired by DynamicXmlBuilder from Ivaylo Kenov

## Install

```
Install-Package MyDynamicXmlBuilder
```

## Documentation
```
using MyDynamicXmlBuilder;
```

````
dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user("Kiro Zlatniq", new {
    username = "kiro",
    age = 50
});
```
