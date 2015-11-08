using System;
using System.Dynamic;
using MyDynamicXmlBuilder;

namespace MyDynamicXmlBuilder.Example
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            dynamic xml = new XmlBuilder();

            xml.Declaration();

            xml.user("Kiro Zlatniq", new {
                username = "kiro",
                age = 50
            });

            Console.WriteLine(xml.ToString(true));
        }
    }
}
