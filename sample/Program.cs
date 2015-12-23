﻿using System;
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


            //xml.Comment("Someone comment");

            xml.user(XmlBuilder.Fragment(user => {
                user.firstname("Kiro");
                user.lastname("Zlatnia");
                user.username("zlatnia");
                user.age(50);
                user.email("kiro@zlatnia.bg");
                user.phone(new {
                    type = "gsm",
                    work = true
                }, "089855533");
            }));            

            Console.WriteLine(xml.ToString(true));
        }
    }
}
