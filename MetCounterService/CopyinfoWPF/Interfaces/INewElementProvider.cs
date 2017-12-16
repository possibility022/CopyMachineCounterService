using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyinfoWPF.Interfaces
{
    interface INewElementProvider<out T>
    {

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns></returns>
        bool GetResult();

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <returns></returns>
        T GetElement();

        /// <summary>
        /// Accepts this instance.
        /// </summary>
        /// <returns></returns>
        bool Accept();
    }
}
