using System;

namespace IoCBridge
{
    public interface IIoCBoostrapper
    {
        void Start();
        void Bind(Type service, Type implementation);
        void Bind<TService, TImplementation>();
        void BindSingleton(Type service, Type implementation);
        void BindSingleton<TService, TImplementation>();
        void BindConstant(Type service, object instance);
        void BindConstant<TService>(TService implementation);

        void BindToFunction(Type service, Func<IIoCInjector, object> func);
        void BindToFunction<TService>(Func<IIoCInjector, TService> func);
    }
}
