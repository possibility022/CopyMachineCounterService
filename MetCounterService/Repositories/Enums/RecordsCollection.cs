using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Enums
{
    public enum RecordsCollection
    {
        /// <summary>
        /// Returns default collection.
        /// </summary>
        Normal,
        /// <summary>
        /// Returns the others collections
        /// </summary>
        Others,

        /// <summary>
        /// Normal and Others in one list.
        /// </summary>
        Both
    }
}
