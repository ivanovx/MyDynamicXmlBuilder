using System;
using System.Dynamic;

namespace DynamicXmlBuider
{
    internal interface IXmlBuilder : IDisposable, IDynamicMetaObjectProvider
    {
    }
}
