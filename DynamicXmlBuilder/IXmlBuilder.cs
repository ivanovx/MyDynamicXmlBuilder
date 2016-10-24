using System;
using System.Dynamic;
using System.Xml.Serialization;

namespace DynamicXmlBuilder
{
    internal interface IXmlBuilder : IDisposable, IDynamicMetaObjectProvider
    {

    }
}
