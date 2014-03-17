using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Union
{
    /// <summary>
    /// An exception denoting an error when pattern matching in Union
    /// </summary>
    public class UnionMatchFailureException : Exception
    {
        /// <summary>
        /// The constructor which takes an error message
        /// </summary>
        /// <param name="message">The error message</param>
        public UnionMatchFailureException(string message) : base(message) { }
    }
}
