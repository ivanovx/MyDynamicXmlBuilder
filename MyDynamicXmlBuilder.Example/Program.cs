using System;
using MyDynamicXmlBuilder;

namespace MyDynamicXmlBuilder.Example
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            dynamic xml = new XmlBuilder();

            xml.user("Kiro Zlatniq", new {
                username = "kiro",
                age = 50
            });

            Console.WriteLine(xml.ToString(true));
        }
    }
}
