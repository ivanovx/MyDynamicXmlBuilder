using System;
using MyDynamicXmlBuilder;

namespace MyDynamicXmlBuilder.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic xml = new XmlBuilder();

            xml.hello("word");

            Console.WriteLine(xml.ToString(true));
        }
    }
}
