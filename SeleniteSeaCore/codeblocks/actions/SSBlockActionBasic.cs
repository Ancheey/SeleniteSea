using SeleniteSeaCore.variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniteSeaCore.codeblocks.actions
{
    public abstract class SSBlockActionBasic : SSBlock
    {
        /// <summary>
        /// These values are used by the default editor
        /// Value name : Value object
        /// </summary>
        public Dictionary<string, SSValue> PublicValues { get; set; } = [];
    }
}
