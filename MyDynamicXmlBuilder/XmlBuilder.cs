using System;
using System.Linq;
using System.Xml.Linq;
using System.Dynamic;
using System.IO;
using System.Xml;
using System.Text;

namespace MyDynamicXmlBuilder
{
	/// <summary>
	/// Dynamic XML construction API for .NET
	/// </summary>
	///
	/// <copyright>
	/// (c) Ivan Ivanov, 2015 - 2016 - http://csyntax.net
	/// </copyright>
	public sealed class XmlBuilder : DynamicObject
	{
		private static XDocument root = new XDocument();
		private static XContainer current;
        
		public XmlBuilder()
		{
			current = root;
		}

		public static Action Section(Action fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException("fragmentBuilder");
			}

			return fragmentBuilder;
		}

		public static Action<dynamic> Section(Action<dynamic> fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException("fragmentBuilder");
			}

			return fragmentBuilder;
		}

		/*public static XmlBuilder Build(Action<dynamic> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			XmlBuilder xmlBuilder = new XmlBuilder();

			builder(xmlBuilder);

			return xmlBuilder;
		}*/

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;

			string tagName = binder.Name;

			Tag(tagName, args);

			return true;
		}

        private void Tag(string tagName, params object[] args)
		{
			if (string.IsNullOrEmpty(tagName))
			{
				throw new ArgumentNullException("tagName");
			}

			if (tagName.IndexOf('_') == 0)
			{
				tagName = tagName.Substring(1);
			}

			string content = null;
			object attributes = null;

			Action fragment = null;

			args
                .ToList()
                .ForEach(arg => {
				    if (arg is Action)
				    {
					    fragment = arg as Action;

				    }
				    else if (arg is Action<dynamic>)
				    {
					    fragment = () => (arg as Action<dynamic>)(this);
				    }
				    else if (arg is string)
				    {
					    content = arg as string;
				    }
				    else if (arg.GetType().IsValueType)
				    {
					    content = arg.ToString();
				    }
				    else
				    {
					    attributes = arg;
				    }
			    });

			XElement element = new XElement(tagName);

			current.Add(element);

			if (fragment != null)
			{
				current = element;
			}

			if (!string.IsNullOrEmpty(content))
			{
				element.Add(content);
			}

			if (attributes != null)
			{
				attributes
                    .GetType()
                    .GetProperties()
                    .ToList()
                    .ForEach(prop => {
					    if (prop.Name == "xmlns")
					    {
						    XNamespace ns = prop.GetValue(attributes, null) as string;

						    element.Name = ns + element.Name.ToString();
					    }
					    else
					    {
						    element.Add(new XAttribute(prop.Name, prop.GetValue(attributes, null)));
					    }
				    });
			}

			if (fragment != null)
			{
				fragment();
				current = element.Parent;
			}
		}

        public void Comment(string comment)
		{
            if (string.IsNullOrEmpty(comment))
			{
				throw new ArgumentNullException("comment");
			}

			current.Add(new XComment(comment));
		}

        public void Declaration()
        {
            root.Declaration = new XDeclaration("1.0", "utf-8", "yes");
        }

		public static implicit operator string(XmlBuilder xml)
		{
			return xml.ToString();
		}
		
		public override string ToString()
		{
			Encoding encoding = new UTF8Encoding(false);

			if (root.Declaration != null && !string.IsNullOrEmpty(root.Declaration.Encoding) &&
				root.Declaration.Encoding.ToLowerInvariant() == "utf-16")
			{
				encoding = new UnicodeEncoding(false, false);
			}

			MemoryStream memoryStream = new MemoryStream();

			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
			{
				Encoding = encoding,
				Indent = true,
				CloseOutput = true,
				OmitXmlDeclaration = root.Declaration == null
			});

			root.Save(xmlWriter);

			xmlWriter.Flush();
			xmlWriter.Close();

			if (encoding is UnicodeEncoding)
			{
				return Encoding.Unicode.GetString(memoryStream.ToArray());
			}
			else
			{
				return Encoding.UTF8.GetString(memoryStream.ToArray());
			}
		}

        public static dynamic Create()
        {
            return new XmlBuilder();
        }
    }
}