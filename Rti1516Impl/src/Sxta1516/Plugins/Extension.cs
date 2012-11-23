namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections;

    /// <summary>
    /// Definies an extension point (path in the tree) with its nodes.
    /// </summary>
    public class Extension
    {
        string path;
        ArrayList nodesCollection = new ArrayList();

        /// <summary>
        /// returns the path in which the underlying nodes are inserted
        /// </summary>
        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        /// <summary>
        /// returns a ArrayList with all Modules defined in this extension.
        /// </summary>
        public ArrayList ModulesCollection
        {
            get { return nodesCollection; }
            set { nodesCollection = value; }
        }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Extension(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// Returns a string representation of a Extension.
        /// </summary>
        public override string ToString()
        {
            return "[Extension: Path = " + path + ", nodesCollection.Count = " + nodesCollection.Count + "]";
        }
    }
}
