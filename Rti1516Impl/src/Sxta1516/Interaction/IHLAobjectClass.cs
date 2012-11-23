namespace Sxta.Rti1516.Proxies
{

    using System;

    using Hla.Rti1516;

    ///<summary>
    ///Autogenerated object instance interface. 
    ///</summary>
    /// <author> Sxta.Rti1516.Compiler.ProxyCompiler</author>
    public interface IHLAobjectClass : IHLAreflection
    {
        ///<summary>Adds a listener for the attributes associated with the IHLAobjectClass class.</summary>
        ///<param name="l"> the listener to Add</param>
        void AddIHLAobjectClassListener(IHLAobjectClassListener l);

        ///<summary>Removes a listener for the attributes associated with the IHLAobjectClass class.</summary>
        ///<param name="l"> the listener to Remove</param>
        void RemoveIHLAobjectClassListener(IHLAobjectClassListener l);

        /// <summary>Sets the value of the parents attribute.</summary>
        /// <param name="p"> Parents the new attribute value</param>
        /// <param name="userSuppliedTag"> a user-supplied tag to associate with the action</param>
        /// <exception cref="ObjectInstanceNotKnown"> if the object instance is unknown</exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined</exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned</exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution</exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress</exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress</exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure</exception>
        void SetParents(String pParents, byte[] userSuppliedTag);

        ///<summary>Returns the value of the parents attribute.</summary>
        ///<returns> the current attribute value</returns>
     String GetParents();

        /// <summary>Sets the value of the sharing attribute.</summary>
        /// <param name="p"> Sharing the new attribute value</param>
        /// <param name="userSuppliedTag"> a user-supplied tag to associate with the action</param>
        /// <exception cref="ObjectInstanceNotKnown"> if the object instance is unknown</exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined</exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned</exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution</exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress</exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress</exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure</exception>
        void SetSharing(Sxta.Rti1516.Reflection.HLAsharingType pSharing, byte[] userSuppliedTag);

        ///<summary>Returns the value of the sharing attribute.</summary>
        ///<returns> the current attribute value</returns>
        Sxta.Rti1516.Reflection.HLAsharingType GetSharing();

        /// <summary>Sets the value of the attributes attribute.</summary>
        /// <param name="p"> Attributes the new attribute value</param>
        /// <param name="userSuppliedTag"> a user-supplied tag to associate with the action</param>
        /// <exception cref="ObjectInstanceNotKnown"> if the object instance is unknown</exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined</exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned</exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution</exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress</exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress</exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure</exception>
        void SetAttributes(long[] pAttributes, byte[] userSuppliedTag);

        ///<summary>Returns the value of the attributes attribute.</summary>
        ///<returns> the current attribute value</returns>
     long[] GetAttributes();

        /// <summary>Sets the value of the semantics attribute.</summary>
        /// <param name="p"> Semantics the new attribute value</param>
        /// <param name="userSuppliedTag"> a user-supplied tag to associate with the action</param>
        /// <exception cref="ObjectInstanceNotKnown"> if the object instance is unknown</exception>
        /// <exception cref="AttributeNotDefined"> if one of the attributes is undefined</exception>
        /// <exception cref="AttributeNotOwned"> if one of the attributes is not owned</exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of an execution</exception>
        /// <exception cref="SaveInProgress"> if a save operation is in progress</exception>
        /// <exception cref="RestoreInProgress"> if a restore operation is in progress</exception>
        /// <exception cref="RTIinternalError"> if an internal error occurred in the
        /// run-time infrastructure</exception>
        void SetSemantics(String pSemantics, byte[] userSuppliedTag);

        ///<summary>Returns the value of the semantics attribute.</summary>
        ///<returns> the current attribute value</returns>
     String GetSemantics();
    }
}
