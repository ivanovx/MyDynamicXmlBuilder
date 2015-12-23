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
                u.phone(new { type = "cell" }, "(985) 555-1234");
            }));



            Console.WriteLine(xml.ToString(true));
        }
    }
}
