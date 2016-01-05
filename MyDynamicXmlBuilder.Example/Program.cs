using System;
using MyDynamicXmlBuilder;

namespace MyDynamicXmlBuilder.Example
{
	class Program
	{
		public static void Main(string[] args)
		{
			dynamic xml = new XmlBuilder();
						
			xml.Declaration();

			xml.Comment("Someone comment");

			xml.Users(XmlBuilder.Section(users =>
			{
				xml.User(new { Id = 1 }, XmlBuilder.Section(user => {
					user.FirstName("Kiro");
					user.LastName("Zlatnia");
					user.UserName("zlatnia");
					user.Age(50);
					user.Email("kiro@zlatnia.bg");
					user.Phone("089855533");
				}));
			}));

			Console.WriteLine(xml);
		}
	}
}
