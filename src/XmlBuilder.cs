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
	/// (c) Ivan Ivanov, 2015 - http://csyntax.github.io
	/// </copyright>

	public sealed class XmlBuilder : DynamicObject
	{
		private XDocument root = new XDocument();
		private XContainer current;

		public XmlBuilder()
		{
			current = root;
		}

		public static Action Fragment(Action fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException("fragmentBuilder");
			}

			return fragmentBuilder;
		}

		public static Action<dynamic> Fragment(Action<dynamic> fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException("fragmentBuilder");
			}

			return fragmentBuilder;
		}

		public static XmlBuilder Build(Action<dynamic> builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}

			XmlBuilder xmlBuilder = new XmlBuilder();

			builder(xmlBuilder);

			return xmlBuilder;
		}

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;

			string tagName = binder.Name;

			Tag(tagName, args);

			return true;
		} 

		/*
			!!!!!!!!!! PUBLIC TO PRIVATE
		*/
		private void Tag(string tagName, params object[] args)
		{
			if (String.IsNullOrEmpty(tagName))
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

			args.ToList().ForEach(arg => {
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

			if (!String.IsNullOrEmpty(content))
			{
				element.Add(content);
			}

			if (attributes != null)
			{
				attributes.GetType().GetProperties().ToList().ForEach(prop =>
				{
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
			if (String.IsNullOrEmpty(comment))
			{
				throw new ArgumentNullException("comment");
			}

			current.Add(new XComment(comment));
		}

		public void CData(string data)
		{
			if (String.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}

			current.Add(new XCData(data));
		}

		public void Text(string text)
		{
			if (String.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("text");
			}

			current.Add(new XText(text));
		}

		public void Declaration(string version = null, string encoding = null, string standalone = null)
		{
			root.Declaration = new XDeclaration(version, encoding, standalone);
		}

		public void DocumentType(string name, string publicId = null, string systemId = null, string internalSubset = null)
		{
			if (String.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}

			root.Add(new XDocumentType(name, publicId, systemId, internalSubset));
		}

		public static implicit operator string (XmlBuilder xml)
		{
			return xml.ToString(false);
		}

		/*
			ToString with bool option is a private
		*/
		private string ToString(bool indent)
		{
			Encoding encoding = new UTF8Encoding(false);

			if (root.Declaration != null && !String.IsNullOrEmpty(root.Declaration.Encoding) &&
				root.Declaration.Encoding.ToLowerInvariant() == "utf-16")
			{
				encoding = new UnicodeEncoding(false, false);
			}

			MemoryStream memoryStream = new MemoryStream();

			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
			{
				Encoding = encoding,
				Indent = indent,
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

		/*
			new ToString method
		*/
		public override string ToString()
		{
			//return base.ToString();

			/*Encoding encoding = new UTF8Encoding(false);

			if (root.Declaration != null && !String.IsNullOrEmpty(root.Declaration.Encoding) &&
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
			}*/

			return this.ToString(true);
		}

		
		/*
			This methods is been removed in 2.0.0 stable
		*/
		private XDocument ToXDocument()
		{
			return root;
		}

		private XElement ToXElement()
		{
			return root.Elements().FirstOrDefault();
		}

		private XmlDocument ToXmlDocument()
		{
			var xmlDoc = new XmlDocument();

			xmlDoc.Load(root.CreateReader());

			return xmlDoc;
		}

		private XmlNode ToXmlNode()
		{
			if (root.DocumentType != null && root.Nodes().Count() > 1)
			{
				return ToXmlDocument().ChildNodes[1] as XmlNode;
			}
			else if (root.DocumentType == null && root.Nodes().Count() >= 1)
			{
				return ToXmlDocument().FirstChild as XmlNode;
			}
			else
			{
				return null as XmlNode;
			}
		}

		private XmlElement ToXmlElement()
		{
			return ToXmlNode() as XmlElement;
		}
	}
}