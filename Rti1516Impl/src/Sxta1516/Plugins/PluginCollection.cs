using System;
using System.Collections;

namespace Sxta.Core.Plugins
{
	/// <summary>
	///     <para>
	///       A collection that stores <see cref="IPlugin"/> objects.
	///    </para>
	/// </summary>
	/// <seealso cref="PluginCollection"/>
	[Serializable()]
	public class PluginCollection : CollectionBase 
	{
		/// <summary>
		///     <para>
		///       Initializes a new instance of <see cref="PluginCollection"/>.
		///    </para>
		/// </summary>
		public PluginCollection() 
		{
		}
		
		/// <summary>
		/// <para>Represents the entry at the specified index of the <see cref="IPlugin"/>.</para>
		/// </summary>
		/// <param name="index"><para>The zero-based index of the entry to locate in the collection.</para></param>
		/// <value>
		///    <para> The entry at the specified index of the collection.</para>
		/// </value>
		/// <exception cref="System.ArgumentOutOfRangeException"><paramref name="index"/> is outside the valid range of indexes for the collection.</exception>
		public IPlugin this[int index] 
		{
			get {
				return ((IPlugin)(List[index]));
			}
			set {
				List[index] = value;
			}
		}
		
		/// <summary>
		///    <para>Adds a <see cref="IPlugin"/> with the specified value to the
		///    <see cref="PluginCollection"/> .</para>
		/// </summary>
		/// <param name="value">The <see cref="IPlugin"/> to add.</param>
		/// <returns>
		///    <para>The index at which the new element was inserted.</para>
		/// </returns>
		public int Add(IPlugin value) 
		{
			return List.Add(value);
		}
		
		/// <summary>
		/// <para>Gets a value indicating whether the
		///    <see cref="PluginCollection"/> contains the specified <see cref="IPlugin"/>.</para>
		/// </summary>
		/// <param name="value">The <see cref="IPlugin"/> to locate.</param>
		/// <returns>
		/// <para><see langword="true"/> if the <see cref="IPlugin"/> is contained in the collection;
		///   otherwise, <see langword="false"/>.</para>
		/// </returns>
		/// <seealso cref="PluginCollection.IndexOf"/>
		public bool Contains(IPlugin value) 
		{
			return List.Contains(value);
		}
		
		/// <summary>
		///    <para>Returns the index of a <see cref="IPlugin"/> in
		///       the <see cref="PluginCollection"/> .</para>
		/// </summary>
		/// <param name="value">The <see cref="IPlugin"/> to locate.</param>
		/// <returns>
		/// <para>The index of the <see cref="IPlugin"/> of <paramref name="value"/> in the
		/// <see cref="PluginCollection"/>, if found; otherwise, -1.</para>
		/// </returns>
		/// <seealso cref="PluginCollection.Contains"/>
		public int IndexOf(IPlugin value) 
		{
			return List.IndexOf(value);
		}
		
		/// <summary>
		///    <para>Returns an enumerator that can iterate through
		///       the <see cref="PluginCollection"/> .</para>
		/// </summary>
		/// <returns><para>None.</para></returns>
		/// <seealso cref="System.Collections.IEnumerator"/>
		public new PluginEnumerator GetEnumerator() 
		{
			return new PluginEnumerator(this);
		}
		
		/// <summary>
		///    <para> Removes a specific <see cref="IPlugin"/> from the
		///    <see cref="PluginCollection"/> .</para>
		/// </summary>
		/// <param name="value">The <see cref="IPlugin"/> to remove from the <see cref="PluginCollection"/> .</param>
		/// <returns><para>None.</para></returns>
		/// <exception cref="System.ArgumentException"><paramref name="value"/> is not found in the Collection. </exception>
		public void Remove(IPlugin value) 
		{
			List.Remove(value);
		}
		
		/// <summary>
		/// Default enumerator.
		/// </summary>
		public class PluginEnumerator : object, IEnumerator 
		{
			IEnumerator baseEnumerator;
			IEnumerable temp;
			
			/// <summary>
			/// Creates a new instance.
			/// </summary>
			public PluginEnumerator(PluginCollection mappings) 
			{
				this.temp = ((IEnumerable)(mappings));
				this.baseEnumerator = temp.GetEnumerator();
			}
			
			/// <summary>
			/// Returns the current object.
			/// </summary>
			public IPlugin Current {
				get {
					return ((IPlugin)(baseEnumerator.Current));
				}
			}
			
			object IEnumerator.Current {
				get {
					return baseEnumerator.Current;
				}
			}
			
			/// <summary>
			/// Moves to the next object.
			/// </summary>
			public bool MoveNext() 
			{
				return baseEnumerator.MoveNext();
			}
			
			bool IEnumerator.MoveNext() 
			{
				return baseEnumerator.MoveNext();
			}
			
			/// <summary>
			/// Resets this enumerator.
			/// </summary>
			public void Reset() 
			{
				baseEnumerator.Reset();
			}
			
			void IEnumerator.Reset() 
			{
				baseEnumerator.Reset();
			}
		}
	}
}
