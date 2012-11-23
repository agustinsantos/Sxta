using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

// Import log4net classes.
using log4net;

using Castle.DynamicProxy;
using Hla.Rti1516;
using Sxta.Rti1516.Management;
using Sxta.Rti1516.ObjectManagement;
using Sxta.Rti1516.XrtiHandles;
using Sxta.Rti1516.Ambassadors;

namespace Sxta.Rti1516.Reflection
{
    [HLAObjectClassAttribute(Name = "HLAobjectRoot",
                             Sharing = HLAsharingType.Neither,
                             Semantics = "The root object.")]
    public class HLAobjectRoot : IHLAobjectRoot
    {

        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // PATCH ANGEL
        public static IHLAfederate DefaultFederate;
        private IHLAfederate OwnFederate_;

        public IHLAfederate OwnFederate
        {
            get { return OwnFederate_; }
            set { OwnFederate_ = value; }
        }

        public static IFederateAmbassador DefaultFederateAmbassador;
        private IFederateAmbassador OwnFederateAmbassador_;

        public IFederateAmbassador OwnFederateAmbassador
        {
            get { return OwnFederateAmbassador_; }
            set { OwnFederateAmbassador_ = value; }
        }

        public static long DefaultFederationExecutionHandle;

        // COMMENT ANGEL: Valor por defecto = -1. Se corresponde con la metafederación
        public static long METAFEDERATION_EXECUTION_HANDLE = -1;

        private long OwnFederationExecutionHandle_ = METAFEDERATION_EXECUTION_HANDLE;

        public long OwnFederationExecutionHandle
        {
            set { OwnFederationExecutionHandle_ = value; }
            get { return OwnFederationExecutionHandle_; }
        }
        // END PATCH

        static ProxyGenerator _generator = new ProxyGenerator();

        protected HLAobjectRoot()
        {
            PreProcessConstruction();
            AutoFlushDisabled = true;
        }

        private static object NewInstance(Type type, HLAProxyAttribute proxyInfo, params object[] args)
        {
            // type == typeof(HLAobjectRoot) || 
            if (type.IsSubclassOf(typeof(HLAobjectRoot)))
            {
                object obj;

                /* 1. Creates a new object */
                if (args != null)
                    obj = _generator.CreateClassProxy(type, new HLAProxySink2(), args);
                else
                    obj = _generator.CreateClassProxy(type, new HLAProxySink2());

                /* 2. Sets-up properties for the new object */
                HLAobjectRoot objRoot = obj as HLAobjectRoot;

                //if (objRoot.OwnFederateAmbassador == null)
                //    return obj;

                // It is a true proxy for a remote object
                if (proxyInfo != null)
                {
                    objRoot.HLAprivilegeToDeleteObject_ = false;

                    objRoot.instanceHandle = new XRTIObjectInstanceHandle(proxyInfo.ObjectInstanceHandle);
                    objRoot.objectName = proxyInfo.ObjectName;
                    objRoot.classHandle = new XRTIObjectClassHandle(proxyInfo.ObjectClassHandle);
                }
                // It is a local object
                else
                {
                    objRoot.HLAprivilegeToDeleteObject_ = true;

                    objRoot.OwnFederate = HLAobjectRoot.DefaultFederate;
                    objRoot.OwnFederateAmbassador = HLAobjectRoot.DefaultFederateAmbassador;
                    objRoot.OwnFederationExecutionHandle = HLAobjectRoot.DefaultFederationExecutionHandle;

                    objRoot.instanceHandle = new XRTIObjectInstanceHandle(((BaseAmbassador)objRoot.OwnFederateAmbassador).HandleCounter);
                    string className = obj.GetType().BaseType.Name;
                    objRoot.objectName = className + objRoot.InstanceHandle.ToString();
                }

                /* 3. Executes reflection process for aop engine */
                HLAProxySink2.ProcessConstruction(objRoot, type);

                /* 4. Warns to the creation listeners */
                foreach (IHLACreateObjectRootListener l in creationListeners)
                {
                    l.OnReceiveCreatedNewObject(obj);
                }

                // It is a local object
                if (proxyInfo == null)
                {
                    // COMMENT ANGEL: Permite enviar la modificación de propiedades que se produce en el constructor
                    objRoot.FlushAttributeValues(null);
                    objRoot.AutoFlushDisabled = false;
                }

                if (log.IsDebugEnabled)
                    log.Debug("Created an instance of " + obj.GetType().BaseType.Name + " [" + objRoot.InstanceHandle + "]");

                return obj;
            }
            else
                throw new Exception("Must be a subclass of HLAobjectRoot");
        }

        /// <summary>
        /// Create an instance of the specified object.
        /// This function install the AOP engine.
        /// </summary>
        /// <param name="type">the type of the object to create</param>
        /// <returns>the new object created</returns>
        public static object NewInstance(Type type)
        {
            return NewInstance(type, null, null);
        }

        public static object NewInstance(Type type, params object[] args)
        {
            return NewInstance(type, null, args);
        }

        internal static object NewRemoteInstance(Type type, HLAProxyAttribute proxyInfo)
        {
            return NewInstance(type, proxyInfo, null);
        }

        internal static object NewRemoteInstance(Type type, HLAProxyAttribute proxyInfo, params object[] args)
        {
            return NewInstance(type, proxyInfo, args);
        }

        protected void PreProcessConstruction()
        {
            HLAAttributeAttribute HLAattribute;
            PropertyInfo[] infos = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo propInfo in infos)
            {
                object[] arrayOfCustomAttributes = propInfo.GetCustomAttributes(true);
                foreach (Attribute attr in arrayOfCustomAttributes)
                {
                    if (attr.GetType() == typeof(HLAAttributeAttribute))
                    {
                        HLAattribute = (HLAAttributeAttribute)attr;
                        MethodInfo mi = propInfo.GetSetMethod();

                        // Every HLAAttribute must be on a property declared as virtual and have a set method
                        if (mi != null && mi.IsVirtual)
                        {
                            Console.WriteLine(">>> Method info : " + mi + ", " + propInfo.DeclaringType.FullName);

                            this.tableMethodInfo2Attr.Add(mi.Name, HLAattribute);
                            this.AttrTable.Add(HLAattribute.Name, HLAattribute);

                            HLAattribute.propInfo = propInfo;
                            HLAattribute.realobject = this;
                        }
                        else
                        {
                            throw new Exception(propInfo.DeclaringType + "." + propInfo.Name + " property has the attribute HLAAttribute but it must be virtual and have a set method");
                        }
                    }
                }
            }
        }


        protected internal Dictionary<string, HLAAttributeAttribute> tableMethodInfo2Attr = new Dictionary<string, HLAAttributeAttribute>();
        protected internal Dictionary<string, HLAAttributeAttribute> AttrTable = new Dictionary<string, HLAAttributeAttribute>();

        /// <summary> The object instance handle.</summary>
        protected Hla.Rti1516.IObjectInstanceHandle instanceHandle = null;

        /// <summary> The object class handle.</summary>
        protected Hla.Rti1516.IObjectClassHandle classHandle;

        /// <summary> The object name.</summary>
        protected System.String objectName;

        /// <summary> Whether or not automatic state flushing has been disabled.</summary>
        protected internal bool autoFlushDisabled = false;

        // PATCH ANGEL
        protected internal bool HLAprivilegeToDeleteObject_;

        public bool HLAprivilegeToDeleteObject
        {
            get { return HLAprivilegeToDeleteObject_; }
        }
        // END PATCH

        /// <summary> Listeners for attributes associated with the class.</summary>
        private static List<IHLACreateObjectRootListener> creationListeners = new List<IHLACreateObjectRootListener>();

        /// <summary> Listeners for attributes associated with the class.</summary>
        private List<IHLAobjectRootListener> listeners = new List<IHLAobjectRootListener>();

        public Hla.Rti1516.IObjectInstanceHandle InstanceHandle
        {
            get { return instanceHandle; }
            set { instanceHandle = value; }
        }

        public string ObjectName
        {
            get { return objectName; }
            set { objectName = value; }
        }

        public Hla.Rti1516.IObjectClassHandle ClassHandle
        {
            get { return classHandle; }
            set { classHandle = value; }
        }

        public static void AddIHLAobjectRootCreationListener(IHLACreateObjectRootListener l)
        {
            creationListeners.Add(l);
        }

        public static void RemoveIHLAobjectRootCreationListener(IHLACreateObjectRootListener l)
        {
            creationListeners.Remove(l);
        }

        public void AddIHLAobjectRootListener(IHLAobjectRootListener l)
        {
            listeners.Add(l);
        }

        public void RemoveIHLAobjectRootListener(IHLAobjectRootListener l)
        {
            listeners.Remove(l);
        }

        /// <summary> 
        /// Gets/Sets whether auto-flush behavior is disabled for this proxy.
        /// if <code>true</code> then auto-flush is disabled, <code>false</code>
        /// otherwise
        /// </summary>
        public bool AutoFlushDisabled
        {
            get { return autoFlushDisabled; }
            set { autoFlushDisabled = value; }
        }

        public void UpdateAttributeValues(string property, object newValue)
        {
            Boolean joined = (OwnFederate != null) && ((Sxtafederate)OwnFederate).HLAisJoined;
            if (!(joined && OwnFederate.HLAtimeRegulating))
            {

                foreach (IHLAobjectRootListener l in listeners)
                {
                    //TODO Include user Tag UpdateAttributeValues(instanceHandle, ahvm, userSuppliedTag);
                    l.OnReceiveUpdateAttributeValues(instanceHandle, property, newValue);
                }

            }
            else
            {
                ILogicalTime time = OwnFederate.HLAlogicalTime.Add(OwnFederate.HLAlookahead);

                foreach (IHLAobjectRootListener l in listeners)
                {
                    //TODO Include user Tag UpdateAttributeValues(instanceHandle, ahvm, userSuppliedTag);
                    l.OnReceiveUpdateAttributeValues(instanceHandle, property, newValue, time);
                }
            }
        }

        protected void UpdateAttributeValues(IDictionary<string, object> methodNameValueMap)
        {
            Boolean joined = (OwnFederate != null) && ((Sxtafederate)OwnFederate).HLAisJoined;
            if (!(joined && OwnFederate.HLAtimeRegulating))
            {

                foreach (IHLAobjectRootListener l in listeners)
                {
                    //TODO Include user Tag UpdateAttributeValues(instanceHandle, ahvm, userSuppliedTag);
                    l.OnReceiveUpdateAttributeValues(instanceHandle, methodNameValueMap);
                }

            }
            else
            {
                ILogicalTime time = OwnFederate.HLAlogicalTime;//.Add(OwnFederate.HLAlookahead);

                foreach (IHLAobjectRootListener l in listeners)
                {
                    //TODO Include user Tag UpdateAttributeValues(instanceHandle, ahvm, userSuppliedTag);
                    l.OnReceiveUpdateAttributeValues(instanceHandle, methodNameValueMap, time);
                }
            }
        }
        
        /// <summary> 
        /// Flushes all modified attribute values.
        /// </summary>
        /// <param name="userSuppliedTag">the user-supplied tag to associate with the update
        /// </param>
        /// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
        /// </exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
        /// </exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress
        /// </exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        public void FlushAttributeValues(byte[] userSuppliedTag)
        {
            FlushAttributeValues(userSuppliedTag, false);
        }

        /// <summary> 
        /// Flushes all, or all modified attribute values.
        /// </summary>
        /// <param name="userSuppliedTag">the user-supplied tag to associate with the update
        /// </param>
        /// <param name="superFlush">if <code>true</code> provide updates for all attributes;
        /// if <code>false</code>, only provide updates for the modified ones
        /// </param>
        /// <exception cref="ObjectInstanceNotKnown">  if the object instance is unknown
        /// </exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined
        /// </exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution
        /// </exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress
        /// </exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress
        /// </exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure
        /// </exception>
        protected void FlushAttributeValues(byte[] userSuppliedTag, bool superFlush)
        {
            IDictionary<string, object> ahvm = new Dictionary<string, object>();

            GetAttributeValuesToFlush(ahvm, superFlush);

            if (ahvm.Count != 0)
            {
                UpdateAttributeValues(ahvm);
            }
        }

        /// <summary>Places the attribute values to flush into the specified map.</summary>
        /// <param name="ahvm"> the attribute handle value map to populate</param>
        /// <param name="superFlush"> if <code>true</code> provide updates for all attributes;
        /// if <code>false</code>, only provide updates for the modified ones</param>
        /// <exception name="RTIinternalError"> if an internal error occurs in the run-time
        /// infrastructure</exception>
        protected void GetAttributeValuesToFlush(IDictionary<string, object> ahvm, bool superFlush)
        {
            foreach (KeyValuePair<string, HLAAttributeAttribute> entry in tableMethodInfo2Attr)
            {
                HLAAttributeAttribute hlaAttr = entry.Value;
                if (hlaAttr.IsValid && (superFlush || hlaAttr.IsDirty))
                {
                    string name = entry.Key.Substring(4, entry.Key.Length - 4);
                    ahvm[name] = hlaAttr.propInfo.GetGetMethod().Invoke(this, null);
                    hlaAttr.IsDirty = false;
                }
            }
        }

        public override string ToString()
        {
            if (instanceHandle != null)
                return instanceHandle.ToString();
            else
                return this.GetType().BaseType.ToString();
        }
    }
    
}
