using System;

namespace Common.Logging
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Method | AttributeTargets.Property)]
    public class IgnoreLoggerAttribute : Attribute
    {
    }
}
