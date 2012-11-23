//
// In order to convert some functionality to Visual C#, the Java Language Conversion Assistant
// creates "support classes" that duplicate the original functionality.  
//
// Support classes replicate the functionality of the original code, but in some cases they are 
// substantially different architecturally. Although every effort is made to preserve the 
// original architecture of the application in the converted project, the user should be aware that 
// the primary goal of these support classes is to replicate functionality, and that at times 
// the architecture of the resulting solution may differ somewhat.
//

using System;

/// <summary>
/// Contains conversion support elements such as classes, interfaces and static methods.
/// </summary>
public class SupportClass
{
	/// <summary>
	/// Represents a collection ob objects that contains no duplicate elements.
	/// </summary>	
	public interface SetSupport : System.Collections.ICollection, System.Collections.IList
	{
		/// <summary>
		/// Adds a new element to the Collection if it is not already present.
		/// </summary>
		/// <param name="obj">The object to add to the collection.</param>
		/// <returns>Returns true if the object was added to the collection, otherwise false.</returns>
		new bool Add(System.Object obj);

		/// <summary>
		/// Adds all the elements of the specified collection to the Set.
		/// </summary>
		/// <param name="c">Collection of objects to add.</param>
		/// <returns>true</returns>
		bool AddAll(System.Collections.ICollection c);
	}


	/*******************************/
	/// <summary>
	/// The class performs token processing in strings
    /// This class replaces the original tokenizer class given by the Java to C Conversor.
	/// </summary>
    public class Tokenizer
    {
        private String[] tokens;
        private int currentToken;

        //The tokenizer uses the default delimiter set: the space character, the tab character, the newline character, and the carriage-return character and the form-feed character
        const string delimitersDefatult = " \t\n\r\f";
        private string delimiters;

        public Tokenizer(String source) : this(source, delimitersDefatult)
        {
        }

        public Tokenizer(String source, string dlmters)
        {
            delimiters = dlmters;
            this.tokens = source.Split(delimiters.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            this.currentToken = 0;
        }

        public String NextToken()
        {
            if (!HasMoreTokens()) throw new System.ArgumentOutOfRangeException();
            else return tokens[currentToken++];
        }

        public Boolean HasMoreTokens()
        {
            return this.currentToken < this.tokens.Length;
        }

    }


	/*******************************/
	/// <summary>
	/// SupportClass for the HashSet class.
	/// </summary>
	[Serializable]
	public class HashSetSupport : System.Collections.ArrayList, SetSupport
	{
		public HashSetSupport() : base()
		{	
		}

		public HashSetSupport(System.Collections.ICollection c) 
		{
			this.AddAll(c);
		}

		public HashSetSupport(int capacity) : base(capacity)
		{
		}

		/// <summary>
		/// Adds a new element to the ArrayList if it is not already present.
		/// </summary>		
		/// <param name="obj">Element to insert to the ArrayList.</param>
		/// <returns>Returns true if the new element was inserted, false otherwise.</returns>
		new public virtual bool Add(System.Object obj)
		{
			bool inserted;

			if ((inserted = this.Contains(obj)) == false)
			{
				base.Add(obj);
			}

			return !inserted;
		}

		/// <summary>
		/// Adds all the elements of the specified collection that are not present to the list.
		/// </summary>
		/// <param name="c">Collection where the new elements will be added</param>
		/// <returns>Returns true if at least one element was added, false otherwise.</returns>
		public bool AddAll(System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(c).GetEnumerator();
			bool added = false;

			while (e.MoveNext() == true)
			{
				if (this.Add(e.Current) == true)
					added = true;
			}

			return added;
		}
		
		/// <summary>
		/// Returns a copy of the HashSet instance.
		/// </summary>		
		/// <returns>Returns a shallow copy of the current HashSet.</returns>
		public override System.Object Clone()
		{
			return base.MemberwiseClone();
		}
	}


	/*******************************/
	/// <summary>
	/// This class provides functionality not found in .NET collection-related interfaces.
	/// </summary>
	public class ICollectionSupport
	{
		/// <summary>
		/// Adds a new element to the specified collection.
		/// </summary>
		/// <param name="c">Collection where the new element will be added.</param>
		/// <param name="obj">Object to add.</param>
		/// <returns>true</returns>
		public static bool Add(System.Collections.ICollection c, System.Object obj)
		{
			bool added = false;
			//Reflection. Invoke either the "add" or "Add" method.
			System.Reflection.MethodInfo method;
			try
			{
				//Get the "add" method for proprietary classes
				method = c.GetType().GetMethod("Add");
				if (method == null)
					method = c.GetType().GetMethod("add");
				int index = (int) method.Invoke(c, new System.Object[] {obj});
				if (index >=0)	
					added = true;
			}
			catch (System.Exception e)
			{
				throw e;
			}
			return added;
		}

		/// <summary>
		/// Adds all of the elements of the "c" collection to the "target" collection.
		/// </summary>
		/// <param name="target">Collection where the new elements will be added.</param>
		/// <param name="c">Collection whose elements will be added.</param>
		/// <returns>Returns true if at least one element was added, false otherwise.</returns>
		public static bool AddAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(c).GetEnumerator();
			bool added = false;

			//Reflection. Invoke "addAll" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("addAll");

				if (method != null)
					added = (bool) method.Invoke(target, new System.Object[] {c});
				else
				{
					method = target.GetType().GetMethod("Add");
					while (e.MoveNext() == true)
					{
						bool tempBAdded =  (int) method.Invoke(target, new System.Object[] {e.Current}) >= 0;
						added = added ? added : tempBAdded;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return added;
		}

		/// <summary>
		/// Removes all the elements from the collection.
		/// </summary>
		/// <param name="c">The collection to Remove elements.</param>
		public static void Clear(System.Collections.ICollection c)
		{
			//Reflection. Invoke "Clear" method or "clear" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Clear");

				if (method == null)
					method = c.GetType().GetMethod("clear");

				method.Invoke(c, new System.Object[] {});
			}
			catch (System.Exception e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Determines whether the collection contains the specified element.
		/// </summary>
		/// <param name="c">The collection to check.</param>
		/// <param name="obj">The object to locate in the collection.</param>
		/// <returns>true if the element is in the collection.</returns>
		public static bool Contains(System.Collections.ICollection c, System.Object obj)
		{
			bool contains = false;

			//Reflection. Invoke "contains" method for proprietary classes
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Contains");

				if (method == null)
					method = c.GetType().GetMethod("contains");

				contains = (bool)method.Invoke(c, new System.Object[] {obj});
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return contains;
		}

		/// <summary>
		/// Determines whether the collection contains all the elements in the specified collection.
		/// </summary>
		/// <param name="target">The collection to check.</param>
		/// <param name="c">Collection whose elements would be checked for containment.</param>
		/// <returns>true id the target collection contains all the elements of the specified collection.</returns>
		public static bool ContainsAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{						
			System.Collections.IEnumerator e =  c.GetEnumerator();

			bool contains = false;

			//Reflection. Invoke "containsAll" method for proprietary classes or "Contains" method for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("containsAll");

				if (method != null)
					contains = (bool)method.Invoke(target, new Object[] {c});
				else
				{					
					method = target.GetType().GetMethod("Contains");
					while (e.MoveNext() == true)
					{
						if ((contains = (bool)method.Invoke(target, new Object[] {e.Current})) == false)
							break;
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return contains;
		}

		/// <summary>
		/// Removes the specified element from the collection.
		/// </summary>
		/// <param name="c">The collection where the element will be removed.</param>
		/// <param name="obj">The element to Remove from the collection.</param>
		public static bool Remove(System.Collections.ICollection c, System.Object obj)
		{
			bool changed = false;

			//Reflection. Invoke "Remove" method for proprietary classes or "Remove" method
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("Remove");

				if (method != null)
					method.Invoke(c, new System.Object[] {obj});
				else
				{
					method = c.GetType().GetMethod("Contains");
					changed = (bool)method.Invoke(c, new System.Object[] {obj});
					method = c.GetType().GetMethod("Remove");
					method.Invoke(c, new System.Object[] {obj});
				}
			}
			catch (System.Exception e)
			{
				throw e;
			}

			return changed;
		}

		/// <summary>
		/// Removes all the elements from the specified collection that are contained in the target collection.
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to Remove from the target collection.</param>
		/// <returns>true</returns>
		public static bool RemoveAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.ArrayList al = ToArrayList(c);
			System.Collections.IEnumerator e = al.GetEnumerator();

			//Reflection. Invoke "RemoveAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = target.GetType().GetMethod("RemoveAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {al});
				else
				{
					method = target.GetType().GetMethod("Remove");
					System.Reflection.MethodInfo methodContains = target.GetType().GetMethod("Contains");

					while (e.MoveNext() == true)
					{
						while ((bool) methodContains.Invoke(target, new System.Object[] {e.Current}) == true)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return true;
		}

		/// <summary>
		/// Retains the elements in the target collection that are contained in the specified collection
		/// </summary>
		/// <param name="target">Collection where the elements will be removed.</param>
		/// <param name="c">Elements to be retained in the target collection.</param>
		/// <returns>true</returns>
		public static bool RetainAll(System.Collections.ICollection target, System.Collections.ICollection c)
		{
			System.Collections.IEnumerator e = new System.Collections.ArrayList(target).GetEnumerator();
			System.Collections.ArrayList al = new System.Collections.ArrayList(c);

			//Reflection. Invoke "RetainAll" method for proprietary classes or "Remove" for each element in the collection
			System.Reflection.MethodInfo method;
			try
			{
				method = c.GetType().GetMethod("RetainAll");

				if (method != null)
					method.Invoke(target, new System.Object[] {c});
				else
				{
					method = c.GetType().GetMethod("Remove");

					while (e.MoveNext() == true)
					{
						if (al.Contains(e.Current) == false)
							method.Invoke(target, new System.Object[] {e.Current});
					}
				}
			}
			catch (System.Exception ex)
			{
				throw ex;
			}

			return true;
		}

		/// <summary>
		/// Returns an array containing all the elements of the collection.
		/// </summary>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c)
		{	
			int index = 0;
			System.Object[] objects = new System.Object[c.Count];
			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objects[index++] = e.Current;

			return objects;
		}

		/// <summary>
		/// Obtains an array containing all the elements of the collection.
		/// </summary>
		/// <param name="objects">The array into which the elements of the collection will be stored.</param>
		/// <returns>The array containing all the elements of the collection.</returns>
		public static System.Object[] ToArray(System.Collections.ICollection c, System.Object[] objects)
		{	
			int index = 0;

			System.Type type = objects.GetType().GetElementType();
			System.Object[] objs = (System.Object[]) Array.CreateInstance(type, c.Count );

			System.Collections.IEnumerator e = c.GetEnumerator();

			while (e.MoveNext())
				objs[index++] = e.Current;

			//If objects is smaller than c then do not return the new array in the parameter
			if (objects.Length >= c.Count)
				objs.CopyTo(objects, 0);

			return objs;
		}

		/// <summary>
		/// Converts an ICollection instance to an ArrayList instance.
		/// </summary>
		/// <param name="c">The ICollection instance to be converted.</param>
		/// <returns>An ArrayList instance in which its elements are the elements of the ICollection instance.</returns>
		public static System.Collections.ArrayList ToArrayList(System.Collections.ICollection c)
		{
			System.Collections.ArrayList tempArrayList = new System.Collections.ArrayList();
			System.Collections.IEnumerator tempEnumerator = c.GetEnumerator();
			while (tempEnumerator.MoveNext())
				tempArrayList.Add(tempEnumerator.Current);
			return tempArrayList;
		}
	}


	/*******************************/
	/// <summary>
	/// Method used to obtain the underlying type of an object to make the correct method call.
	/// </summary>
	/// <param name="tempObject">Object instance received.</param>
	/// <param name="method">Method name to invoke.</param>
	/// <param name="parameters">Object array containing the method parameters.</param>
	/// <returns>The return value of the method called with the proper parameters.</returns>
	public static System.Object InvokeMethodAsVirtual(System.Object tempObject, System.String method, System.Object[] parameters)
	{
		System.Reflection.MethodInfo methodInfo;
		System.Type type = tempObject.GetType();
		if (parameters != null)
		{
			System.Type[] types = new System.Type[parameters.Length];
			for (int index = 0; index < parameters.Length; index++)
				types[index] = parameters[index].GetType();
			methodInfo = type.GetMethod(method, types);
		}
		else methodInfo = type.GetMethod(method);
		try
		{
			return methodInfo.Invoke(tempObject, parameters);
		}
		catch (System.Exception exception)
		{
			throw exception.InnerException;
		}
	}

	/*******************************/
	/// <summary>
	/// Writes the exception stack trace to the received stream
	/// </summary>
	/// <param name="throwable">Exception to obtain information from</param>
	/// <param name="stream">Output sream used to write to</param>
	public static void WriteStackTrace(System.Exception throwable, System.IO.TextWriter stream)
	{
		stream.Write(throwable.StackTrace);
		stream.Flush();
	}

	/*******************************/
	/// <summary>
	/// Converts an array of sbytes to an array of bytes
	/// </summary>
	/// <param name="sbyteArray">The array of sbytes to be converted</param>
	/// <returns>The new array of bytes</returns>
	public static byte[] ToByteArray(byte[] sbyteArray)
	{
		byte[] byteArray = null;

		if (sbyteArray != null)
		{
			byteArray = new byte[sbyteArray.Length];
			for(int index=0; index < sbyteArray.Length; index++)
				byteArray[index] = (byte) sbyteArray[index];
		}
		return byteArray;
	}

	/// <summary>
	/// Converts a string to an array of bytes
	/// </summary>
	/// <param name="sourceString">The string to be converted</param>
	/// <returns>The new array of bytes</returns>
	public static byte[] ToByteArray(System.String sourceString)
	{
		return System.Text.UTF8Encoding.UTF8.GetBytes(sourceString);
	}

	/// <summary>
	/// Converts a array of object-type instances to a byte-type array.
	/// </summary>
	/// <param name="tempObjectArray">Array to convert.</param>
	/// <returns>An array of byte type elements.</returns>
	public static byte[] ToByteArray(System.Object[] tempObjectArray)
	{
		byte[] byteArray = null;
		if (tempObjectArray != null)
		{
			byteArray = new byte[tempObjectArray.Length];
			for (int index = 0; index < tempObjectArray.Length; index++)
				byteArray[index] = (byte)tempObjectArray[index];
		}
		return byteArray;
	}


	/*******************************/
	/// <summary>
	/// Support class used to extend System.Net.Sockets.UdpClient class functionality
	/// </summary>
	public class UdpClientSupport : System.Net.Sockets.UdpClient
	{
	
		public int port = -1;
		
		public System.Net.IPEndPoint ipEndPoint = null;
		
		public String host = null;
	
	
		/// <summary>
		/// Initializes a new instance of the UdpClientSupport class, and binds it to the local port number provided.
		/// </summary>
		/// <param name="port">The local port number from which you intend to communicate.</param>
		public UdpClientSupport(int port) : base(port)
		{
			this.port = port;
		}

		/// <summary>
		/// Initializes a new instance of the UdpClientSupport class.
		/// </summary>
		public UdpClientSupport() : base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the UdpClientSupport class,
		/// and binds it to the specified local endpoint.
		/// </summary>
		/// <param name="IP">An IPEndPoint that respresents the local endpoint to which you bind the UDP connection.</param>
		public UdpClientSupport(System.Net.IPEndPoint IP) : base(IP)
		{
			this.ipEndPoint = IP;
			this.port = this.ipEndPoint.Port;
		}

		/// <summary>
		/// Initializes a new instance of the UdpClientSupport class,
		/// and and establishes a default remote host.
		/// </summary>
		/// <param name="host">The name of the remote DNS host to which you intend to connect.</param>
		/// <param name="port">The remote port number to which you intend to connect. </param>
		public UdpClientSupport(System.String host, int port) : base(host,port)
		{
			this.host = host;
			this.port = port;
		}

		/// <summary>
		/// Returns a UDP datagram that was sent by a remote host.
		/// </summary>
		/// <param name="tempClient">UDP client instance to use to receive the datagram</param>
		/// <param name="packet">Instance of the recieved datagram packet</param>
		public static void Receive(System.Net.Sockets.UdpClient tempClient, out PacketSupport packet)
		{
			System.Net.IPEndPoint remoteIpEndPoint = 
				new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

			PacketSupport tempPacket;
			try
			{
				byte[] data_in = tempClient.Receive(ref remoteIpEndPoint); 
				tempPacket = new PacketSupport(data_in, data_in.Length);
				tempPacket.IPEndPoint = remoteIpEndPoint;
			}
			catch ( System.Exception e )
			{
				throw new System.Exception(e.Message); 
			}
			packet = tempPacket;
		}

		/// <summary>
		/// Sends a UDP datagram to the host at the specified remote endpoint.
		/// </summary>
		/// <param name="tempClient">Client to use as source for sending the datagram</param>
		/// <param name="packet">Packet containing the datagram data to send</param>
		public static void Send(System.Net.Sockets.UdpClient tempClient, PacketSupport packet)
		{
			tempClient.Send(packet.Data,packet.Length, packet.IPEndPoint);     
		}
		
		
		/// <summary>
		/// Gets and sets the address of the IP
		/// </summary>			
		/// <returns>The IP address</returns>
		public System.Net.IPEndPoint IPEndPoint
		{
			get 
			{
				return this.ipEndPoint;
			}
			set 
			{
				this.ipEndPoint = value;
			}
		}
	
		/// <summary>
		/// Gets and sets the port
		/// </summary>			
		/// <returns>The int value of the port</returns>
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value < 0 || value > 0xFFFF)
					throw new System.ArgumentException("Port out of range:"+ value);

				this.port = value;
			}
		}
		
		
		/// <summary>
		/// Gets the address of the IP
		/// </summary>			
		/// <returns>The IP address</returns>
		public System.Net.IPAddress getIPEndPointAddress()
		{
			if(this.ipEndPoint == null)
				return null;
			else
				return (this.ipEndPoint.Address == null)? null : this.ipEndPoint.Address;
		}

	}

	/*******************************/
	/// <summary>
	/// Receives a byte array and returns it transformed in an sbyte array
	/// </summary>
	/// <param name="byteArray">Byte array to process</param>
	/// <returns>The transformed array</returns>
	public static byte[] ToSByteArray(byte[] byteArray)
	{
		byte[] sbyteArray = null;
		if (byteArray != null)
		{
			sbyteArray = new byte[byteArray.Length];
			for(int index=0; index < byteArray.Length; index++)
				sbyteArray[index] = (byte) byteArray[index];
		}
		return sbyteArray;
	}

	/*******************************/
	/// <summary>
	/// Class to manage packets
	/// </summary>
	public class PacketSupport
	{
		private byte[] data;
		private int length;
		private System.Net.IPEndPoint ipEndPoint;

		int port = -1;
		System.Net.IPAddress address = null;

		/// <summary>
		/// Constructor for the packet
		/// </summary>	
		/// <param name="data">The bufferStream to store the data</param>	
		/// <param name="data">The length of the data sent</param>	
		/// <returns>A new packet to receive data of the specified length</returns>	
		public PacketSupport(byte[] data, int length)
		{
			if (length > data.Length)
				throw new System.ArgumentException("illegal length"); 

			this.data = data;
			this.length = length;
			this.ipEndPoint = null;
		}

		/// <summary>
		/// Constructor for the packet
		/// </summary>	
		/// <param name="data">The data to be sent</param>	
		/// <param name="data">The length of the data to be sent</param>	
		/// <param name="data">The IP of the destination point</param>	
		/// <returns>A new packet with the data, length and ipEndPoint set</returns>
		public PacketSupport(byte[] data, int length, System.Net.IPEndPoint ipendpoint)
		{
			if (length > data.Length)
				throw new System.ArgumentException("illegal length"); 

			this.data = data;
			this.length = length;
			this.ipEndPoint = ipendpoint;
		}

		/// <summary>
		/// Gets and sets the address of the IP
		/// </summary>			
		/// <returns>The IP address</returns>
		public System.Net.IPEndPoint IPEndPoint
		{
			get 
			{
				return this.ipEndPoint;
			}
			set 
			{
				this.ipEndPoint = value;
			}
		}

		/// <summary>
		/// Gets and sets the address
		/// </summary>			
		/// <returns>The int value of the address</returns>
		public System.Net.IPAddress Address
		{
			get
			{
				return address;
			}
			set
			{
				address = value;
				if (this.ipEndPoint == null) 
				{
					if (Port >= 0 && Port <= 0xFFFF)
					  this.ipEndPoint = new System.Net.IPEndPoint(value, Port);
				}
				else
					this.ipEndPoint.Address = value;
			}
		}

		/// <summary>
		/// Gets and sets the port
		/// </summary>			
		/// <returns>The int value of the port</returns>
		public int Port
		{
			get
			{
				return port;
			}
			set
			{
				if (value < 0 || value > 0xFFFF)
					throw new System.ArgumentException("Port out of range:"+ value);

				port = value;
				if (this.ipEndPoint == null) 
				{
					if (Address != null)
					  this.ipEndPoint = new System.Net.IPEndPoint(Address, value);
				}
				else
					this.ipEndPoint.Port = value;
			}
		}

		/// <summary>
		/// Gets and sets the length of the data
		/// </summary>			
		/// <returns>The int value of the length</returns>
		public int Length
		{
			get 
			{
				return this.length;
			}
			set
			{
				if (value > data.Length)
					throw new System.ArgumentException("illegal length"); 

				this.length = value;
			}
		}

		/// <summary>
		/// Gets and sets the byte array that contains the data
		/// </summary>			
		/// <returns>The byte array that contains the data</returns>
		public byte[] Data
		{
			get 
			{
				return this.data;
			}

			set 
			{
				this.data = value;
			}
		}
	}
	/*******************************/
	//Provides access to a static System.Random class instance
	static public System.Random Random = new System.Random();

	/*******************************/
	/// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
	/// <param name="sourceStream">The source Stream to read from.</param>
	/// <param name="target">Contains the array of characteres read from the source Stream.</param>
	/// <param name="start">The starting index of the target array.</param>
	/// <param name="count">The maximum number of characters to read from the source Stream.</param>
	/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
	public static System.Int32 ReadInput(System.IO.Stream sourceStream, byte[] target, int start, int count)
	{
		// Returns 0 bytes if not enough space in target
		if (target.Length == 0)
			return 0;

		byte[] receiver = new byte[target.Length];
		int bytesRead   = sourceStream.Read(receiver, start, count);

		// Returns -1 if EOF
		if (bytesRead == 0)	
			return -1;
                
		for(int i = start; i < start + bytesRead; i++)
			target[i] = (byte)receiver[i];
                
		return bytesRead;
	}

	/// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
	/// <param name="sourceTextReader">The source TextReader to read from</param>
	/// <param name="target">Contains the array of characteres read from the source TextReader.</param>
	/// <param name="start">The starting index of the target array.</param>
	/// <param name="count">The maximum number of characters to read from the source TextReader.</param>
	/// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
	public static System.Int32 ReadInput(System.IO.TextReader sourceTextReader, byte[] target, int start, int count)
	{
		// Returns 0 bytes if not enough space in target
		if (target.Length == 0) return 0;

		char[] charArray = new char[target.Length];
		int bytesRead = sourceTextReader.Read(charArray, start, count);

		// Returns -1 if EOF
		if (bytesRead == 0) return -1;

		for(int index=start; index<start+bytesRead; index++)
			target[index] = (byte)charArray[index];

		return bytesRead;
	}

	/*******************************/
	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static long Identity(long literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static ulong Identity(ulong literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static float Identity(float literal)
	{
		return literal;
	}

	/// <summary>
	/// This method returns the literal value received
	/// </summary>
	/// <param name="literal">The literal to return</param>
	/// <returns>The received value</returns>
	public static double Identity(double literal)
	{
		return literal;
	}

	/*******************************/
	/// <summary>
	/// Provides functionality not found in .NET map-related interfaces.
	/// </summary>
	public class MapSupport
	{
		/// <summary>
		/// Determines whether the SortedList contains a specific value.
		/// </summary>
		/// <param name="d">The dictionary to check for the value.</param>
		/// <param name="obj">The object to locate in the SortedList.</param>
		/// <returns>Returns true if the value is contained in the SortedList, false otherwise.</returns>
		public static bool ContainsValue(System.Collections.IDictionary d, System.Object obj)
		{
			bool contained = false;
			System.Type type = d.GetType();

			//Classes that implement the SortedList class
			if (type == System.Type.GetType("System.Collections.SortedList"))
			{
				contained = (bool) ((System.Collections.SortedList) d).ContainsValue(obj);
			}
			//Classes that implement the Hashtable class
			else if (type == System.Type.GetType("System.Collections.Hashtable"))
			{
				contained = (bool) ((System.Collections.Hashtable) d).ContainsValue(obj);
			}
			else 
			{
				//Reflection. Invoke "containsValue" method for proprietary classes
				try
				{
					System.Reflection.MethodInfo method = type.GetMethod("containsValue");
					contained = (bool) method.Invoke(d, new Object[] {obj});
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					throw e;
				}
				catch (System.Exception e)
				{
					throw e;
				}
			}

			return contained;
		}
		
		
		/// <summary>
		/// Determines whether the NameValueCollection contains a specific value.
		/// </summary>
		/// <param name="d">The dictionary to check for the value.</param>
		/// <param name="obj">The object to locate in the SortedList.</param>
		/// <returns>Returns true if the value is contained in the NameValueCollection, false otherwise.</returns>
		public static bool ContainsValue(System.Collections.Specialized.NameValueCollection d, System.Object obj)
		{
			bool contained = false;
			System.Type type = d.GetType();

			for (int i = 0; i < d.Count && !contained ; i++)
			{
				System.String [] values = d.GetValues(i);
				if (values != null) 
				{
					foreach (System.String val in values)
					{
						if (val.Equals(obj))
						{
							contained = true;
							break;
						}
					}
				}
			}
			return contained;
		}		

		/// <summary>
		/// Copies all the elements of d to target.
		/// </summary>
		/// <param name="target">Collection where d elements will be copied.</param>
		/// <param name="d">Elements to copy to the target collection.</param>
		public static void PutAll(System.Collections.IDictionary target, System.Collections.IDictionary d)
		{
			if(d != null)
			{
					System.Collections.ArrayList keys = new System.Collections.ArrayList(d.Keys);
				System.Collections.ArrayList values = new System.Collections.ArrayList(d.Values);

				for (int i=0; i < keys.Count; i++)
					target[keys[i]] = values[i];
			}
		}
		
		/// <summary>
		/// Returns a portion of the list whose keys are less than the limit object parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The end element of the portion to extract.</param>
		/// <returns>The portion of the collection whose elements are less than the limit object parameter.</returns>
		public static System.Collections.SortedList HeadMap(System.Collections.SortedList l, System.Object limit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			for (int i=0; i < l.Count; i++)
			{
				if (comparer.Compare(l.GetKey(i), limit) >= 0)
					break;

				newList.Add(l.GetKey(i), l[l.GetKey(i)]);
			}

			return newList;
		}

		/// <summary>
		/// Returns a portion of the list whose keys are greater that the lowerLimit parameter less than the upperLimit parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The start element of the portion to extract.</param>
		/// <param name="limit">The end element of the portion to extract.</param>
		/// <returns>The portion of the collection.</returns>
		public static System.Collections.SortedList SubMap(System.Collections.SortedList list, System.Object lowerLimit, System.Object upperLimit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			if (list != null)
			{
				if ((list.Count > 0)&&(!(lowerLimit.Equals(upperLimit))))
				{
					int index = 0;
					while (comparer.Compare(list.GetKey(index), lowerLimit) < 0)
						index++;

					for (; index < list.Count; index++)
					{
						if (comparer.Compare(list.GetKey(index), upperLimit) >= 0)
							break;

						newList.Add(list.GetKey(index), list[list.GetKey(index)]);
					}
				}
			}

			return newList;
		}

		/// <summary>
		/// Returns a portion of the list whose keys are greater than the limit object parameter.
		/// </summary>
		/// <param name="l">The list where the portion will be extracted.</param>
		/// <param name="limit">The start element of the portion to extract.</param>
		/// <returns>The portion of the collection whose elements are greater than the limit object parameter.</returns>
		public static System.Collections.SortedList TailMap(System.Collections.SortedList list, System.Object limit)
		{
			System.Collections.Comparer comparer = System.Collections.Comparer.Default;
			System.Collections.SortedList newList = new System.Collections.SortedList();

			if (list != null)
			{
				if (list.Count > 0)
				{
					int index = 0;
					while (comparer.Compare(list.GetKey(index), limit) < 0)
						index++;

					for (; index < list.Count; index++)
						newList.Add(list.GetKey(index), list[list.GetKey(index)]);
				}
			}

			return newList;
		}
	}


}
