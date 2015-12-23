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
dynamic xml = new XmlBuilder();

xml.Declaration();

xml.user("Kiro Zlatniq", new {
    username = "kiro",
    age = 50
});
```

[Another dynamic xml build by Ivaylo Kenov](https://github.com/ivaylokenov/DynamicXMLBuilder)
