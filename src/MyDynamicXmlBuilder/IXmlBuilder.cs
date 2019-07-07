using System;

namespace MyDynamicXmlBuilder
{
    public interface IXmlBuilder : IDisposable
    {
        string Build();
    }
}
