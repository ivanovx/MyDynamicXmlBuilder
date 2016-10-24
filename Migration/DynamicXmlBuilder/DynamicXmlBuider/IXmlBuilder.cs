using System;
using System.ComponentModel;
using System.Dynamic;

namespace DynamicXmlBuider
{
    internal interface IXmlBuilder : IDisposable, IDynamicMetaObjectProvider
    {
    }
}
