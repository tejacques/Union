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
    public class MatchNotExhaustiveException : Exception
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public MatchNotExhaustiveException()
            : base("Match is not exhaustive")
        { }
    }
}
