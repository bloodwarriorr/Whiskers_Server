using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Type
    {
        public int TypeCode { get; set; }
       public string TypeDesc { get; set; }
        public Type()
        {

        }

        public Type(int typeCode, string typeDesc)
        {
            TypeCode = typeCode;
            TypeDesc = typeDesc;
        }
    }
}
