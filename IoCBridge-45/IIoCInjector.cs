using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCBridge
{
    public interface IIoCInjector
    {
        T Inject<T>(params NamedArg[] args);
        T Arg<T>(int index);
        T Arg<T>(string name);
    }
}
