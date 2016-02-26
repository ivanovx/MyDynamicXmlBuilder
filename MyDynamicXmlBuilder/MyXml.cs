using System;

namespace MyDynamicXmlBuilder
{
    public static class MyXml
    {
        public static dynamic Create()
        {
            return new XmlBuilder();
        }
    } 
}
