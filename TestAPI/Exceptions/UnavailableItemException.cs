using System;
using System.IO;

namespace TestAPI.Exceptions
{
    /// <summary>
    /// An exception thrown when a file within MyStuff is not available on the file system.
    /// </summary>
    public class UnavailableItemException : FileNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnavailableItemException"/> class.
        /// </summary>
        public UnavailableItemException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnavailableItemException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public UnavailableItemException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnavailableItemException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="virtualPath">The virtual path of the file</param>
        /// <param name="ex">Any file not found exception</param>
        public UnavailableItemException(string message, string virtualPath, Exception ex)
            : base(message, virtualPath, ex)
        {

        }
    }
}