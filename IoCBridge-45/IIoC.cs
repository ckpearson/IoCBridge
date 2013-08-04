using System;

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
