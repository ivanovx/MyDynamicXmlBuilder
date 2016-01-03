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

            xml.Comment("Someone comment");

            xml.user(new { id = 1 }, XmlBuilder.Fragment(user => {
                user.firstname("Kiro");
                user.lastname("Zlatnia");
                user.username("zlatnia");
                user.age(50);
                user.email("kiro@zlatnia.bg");
                user.phone("089855533");
            }));            

            Console.WriteLine(xml);
        }
    }
}
