using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Object;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Common.Logging
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var methodInfo = getNext();
            var canlog = (input.MethodBase.GetCustomAttribute(typeof(IgnoreLoggerAttribute), false) == null) ? true : false;
            if (canlog) LoggerHelper.Log(input.MethodBase.ReflectedType, LoggerLevel.Debug, string.Format("{0}=>{1}", input.MethodBase.Name, input.MethodBase.GetParameters().ToString(input)));
            var result = methodInfo.Invoke(input, getNext);
            if (!canlog) return result;
            try
            {
                object[] ts = (input.MethodBase as MethodInfo).ReturnTypeCustomAttributes.GetCustomAttributes(typeof(IgnoreLoggerAttribute), false);
                if (ts == null || ts.Length == 0) LoggerHelper.Log(input.MethodBase.ReflectedType, LoggerLevel.Debug, string.Format("{0}<={1}", input.MethodBase.Name, (result.ReturnValue == null) ? "null" : result.ReturnValue.ToString()));
                else LoggerHelper.Log(input.MethodBase.ReflectedType, LoggerLevel.Debug, string.Format("{0}<=***", input.MethodBase.Name));
            }
            catch
            {
                LoggerHelper.Log(input.MethodBase.ReflectedType, LoggerLevel.Debug, string.Format("{0}<=void", input.MethodBase.Name));
            }
            return result;
        }
        public bool WillExecute
        {
            get { return true; }
        }

    }
}
