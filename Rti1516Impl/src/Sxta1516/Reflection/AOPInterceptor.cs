using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Sxta.Rti1516.Management;
using System.Diagnostics;
using Sxta.Rti1516.Lrc;

using Castle.DynamicProxy;

namespace Sxta.Rti1516.Reflection
{
    public class HLAProxySink2 : StandardInterceptor
    {
        public static void ProcessConstruction(HLAobjectRoot obj, Type type)
        {
            object[] classAttributes = type.GetCustomAttributes(typeof(HLAObjectClassAttribute), false);
            if (classAttributes.Length == 0)
            {
                throw new Exception(type.FullName + " is class derived from HLAobjectRoot, but it hasn't the attribute HLAObjectClass");
            }

            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            
            bool defaultConstructorExists = false;
            foreach (ConstructorInfo constrInfo in constructors)
            {
                if (constrInfo.IsPublic)
                {
                    // TODO: según la guía de estilo debería lanzarse MyException en vez de Exception
                    throw new Exception(type.FullName + " has the attribute HLAObjectClass but its constructor: " + constrInfo + " is public. Must change it to protected and call NewInstance(typeof(" + type.FullName + ")) instead");
                }

                if (constrInfo.GetParameters().Length == 0)
                    defaultConstructorExists = true;

            }

            // TODO: según la guía de estilo debería lanzarse MyException en vez de Exception
            if (!defaultConstructorExists)
                throw new Exception(type.FullName + " must have a constructor by default without parameters");

            /* This code has moved to PreProcessConstruction in HLAobjectRoot
            PropertyInfo[] infos = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propInfo in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                object[] arrayOfCustomAttributes = propInfo.GetCustomAttributes(true);
                foreach (Attribute attr in arrayOfCustomAttributes)
                {
                    if (attr.GetType() == typeof(HLAAttributeAttribute))
                    {
                        MethodInfo mi = propInfo.GetSetMethod();
                        //Every HLAAttribute must be on a property declared as virtual and have a set method
                        if (mi != null && mi.IsVirtual)
                        {
                            Console.WriteLine(">>> Method info : " + mi + ", " + propInfo.DeclaringType.FullName);
                            ((HLAAttributeAttribute)attr).propInfo = propInfo;
                            obj.tableMethodInfo2Attr.Add(mi.Name, (HLAAttributeAttribute)attr);
                            obj.AttrTable.Add(((HLAAttributeAttribute)attr).Name, (HLAAttributeAttribute)attr);
                            ((HLAAttributeAttribute)attr).realobject = obj;
                        }
                        else
                            throw new Exception(propInfo.DeclaringType + "." + propInfo.Name + " property has the attribute HLAAttribute but it must be virtual and have a set method");
                    }
                }
            }
            */
        }

        protected override void PreProceed(IInvocation invocation, params object[] args)
        {
            //Console.WriteLine("In PreProceed " + invocation.Method);
        }

        protected override void PostProceed(IInvocation invocation, ref object returnValue, params object[] arguments)
        {
            //Console.WriteLine("In PostProceed " + method);
            if (isSetMethodForHLAobjectRoot(invocation))
            {
                HLAobjectRoot obj = invocation.InvocationTarget as HLAobjectRoot;
                HLAAttributeAttribute hlaAttr;
                System.Reflection.MethodInfo methodInfo = invocation.Method;
                if (obj.tableMethodInfo2Attr.TryGetValue(methodInfo.Name, out hlaAttr))
                {
                    hlaAttr.FlushAttributeValues(hlaAttr.AttributeInfo.Name, arguments);
                }
            }
        }

        public override object Intercept(IInvocation invocation, params object[] args)
        {
            object retValue = null;

            if (isSetMethodForHLAobjectRoot(invocation) && !isCallbackInvocation())
            {
                PreProceed(invocation, args);
                HLAobjectRoot obj = invocation.InvocationTarget as HLAobjectRoot;

                Sxtafederate federate = (Sxtafederate)obj.OwnFederate;
                if (federate == null || !(federate.HLAisJoined && federate.HLAtimeRegulating))
                {
                    retValue = invocation.Proceed(args);
                }

                PostProceed(invocation, ref retValue, args);
            }
            else
            {
                retValue = invocation.Proceed(args);
            }

            return retValue;
        }
 
        protected Boolean isSetMethodForHLAobjectRoot(IInvocation invocation)
        {
            System.Reflection.MethodInfo methodInfo = invocation.Method;
            return methodInfo.IsSpecialName && methodInfo.Name.StartsWith("set_") && invocation.InvocationTarget is HLAobjectRoot;
        }

        protected Boolean isCallbackInvocation()
        {
            Boolean found = false;
            StackTrace trace = new StackTrace(StackTrace.METHODS_TO_SKIP + 5);
            StackFrame frame;
            MethodBase caller;
            Type dt;
            for (int i = 0; i < trace.FrameCount && !found; i++)
            {
                frame = trace.GetFrame(i);
                caller = frame.GetMethod();
                dt = caller.DeclaringType;

                if (dt.IsSubclassOf(typeof(Callback)))  // == typeof(Callback)
                {
                    found = true;
                }
            }

            return found;
            
        }
    }
}
