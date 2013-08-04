using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCBridge
{
    public interface IIoC
    {
        T Get<T>(params NamedArg[] args);
        object Get(Type serviceType, params NamedArg[] args);

        bool IsTypeBound<T>();
        bool IsTypeBound(Type serviceType);
    }
}
