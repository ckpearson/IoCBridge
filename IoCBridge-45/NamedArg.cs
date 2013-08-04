using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCBridge
{
    public sealed class NamedArg
    {
        private readonly string _name;
        private readonly object _value;

        public NamedArg(string name, object value)
        {
            _name = name;
            _value = value;
        }

        public string Name { get { return _name; } }
        public object Value { get { return _value; } }
    }
}
