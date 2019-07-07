namespace MyDynamicXmlBuilder
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Linq;
    using System.Text;
    using System.Dynamic;
    using System.Xml.Linq;

    /// <summary>
    ///     Dynamic XML construction API for .NET
    /// </summary>
    /// 
    /// <copyright>
    ///     (c) Ivan Ivanov, 2015 - 2019 - http://xml.csyntax.net
    /// </copyright>
    public class XmlBuilder : DynamicObject, IXmlBuilder
    {
        private readonly MemoryStream memoryStream;
        private XmlWriter xmlWriter;

        private readonly XDocument parent;
        private XContainer children;

        public XmlBuilder()
		{
            this.parent = new XDocument();
            this.children = parent;
		}

        public XmlBuilder(MemoryStream memoryStream, XmlWriter xmlWriter)
        {
            this.memoryStream = memoryStream;
            this.xmlWriter = xmlWriter;
        }

        public static Action Section(Action fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException(nameof(fragmentBuilder));
			}

			return fragmentBuilder;
		}

		public static Action<dynamic> Section(Action<dynamic> fragmentBuilder)
		{
			if (fragmentBuilder == null)
			{
				throw new ArgumentNullException(nameof(fragmentBuilder));
			}

			return fragmentBuilder;
		}

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;

			string tagName = binder.Name;

			this.Tag(tagName, args);

			return true;
		}      

        private void Tag(string tagName, params object[] args)
		{
			if (string.IsNullOrEmpty(tagName))
			{
				throw new ArgumentNullException(nameof(tagName));
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
                .ForEach(arg => 
                {
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

			this.children.Add(element);

			if (fragment != null)
			{
				this.children = element;
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

				this.children = element.Parent;
			}
		}

        public void Comment(string comment)
		{
            if (string.IsNullOrEmpty(comment))
			{
				throw new ArgumentNullException(nameof(comment));
			}

			this.children.Add(new XComment(comment));
		}

        public void Declaration() => this.parent.Declaration = new XDeclaration("1.0", "utf-8", "yes");

        public string Build()
        {
            /*Encoding encoding = new UTF8Encoding(false);

            if (this.parent.Declaration != null && 
                !string.IsNullOrEmpty(this.parent.Declaration.Encoding) &&
                this.parent.Declaration.Encoding.ToLowerInvariant() == "utf-16")
            {
                encoding = new UnicodeEncoding(false, false);
            }*/

            var encoding = new UnicodeEncoding();

            XmlWriterSettings writerSettings = new XmlWriterSettings
            {
                Encoding = encoding,
                Indent = true,
                CloseOutput = true,
                OmitXmlDeclaration = false,
                WriteEndDocumentOnClose = true,
                Async = false,
                CheckCharacters = true,
                ConformanceLevel = ConformanceLevel.Document,
                NamespaceHandling = NamespaceHandling.Default,
                DoNotEscapeUriAttributes = false
            };

            using (this.memoryStream)
            {
                using (this.xmlWriter = XmlWriter.Create(this.memoryStream, writerSettings))
                {
                    this.parent.Save(this.xmlWriter);
                }
            }

            /*if (encoding is UnicodeEncoding)
            {
                return Encoding.Unicode.GetString(this.MemoryStream.ToArray());
            }
            else
            {
                return Encoding.UTF8.GetString(this.MemoryStream.ToArray());
            }*/

            return Encoding.Unicode.GetString(this.memoryStream.ToArray());
        }
		
		public override string ToString() => this.Build();

        public static implicit operator string(XmlBuilder xml) => xml.Build();

        public static dynamic Create()
            => new XmlBuilder();

        public static dynamic Create(MemoryStream memoryStream, XmlWriter xmlWriter) 
            => new XmlBuilder(memoryStream, xmlWriter);

        public void Dispose()
        {
            this.memoryStream.Dispose();
            this.xmlWriter.Dispose();
        }
    }
}