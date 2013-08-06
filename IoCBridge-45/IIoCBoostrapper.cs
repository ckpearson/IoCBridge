using System;

namespace IoCBridge
{
    /// <summary>
    /// Bootstrapper interface
    /// </summary>
    public interface IIoCBoostrapper
    {
        /// <summary>
        /// Starts the bootstrapper
        /// </summary>
        void Start();

        /// <summary>
        /// Binds the given service type to the given implementation type
        /// </summary>
        /// <param name="service">The service type</param>
        /// <param name="implementation">The implementation type</param>
        void Bind(Type service, Type implementation);

        /// <summary>
        /// Binds the given service type to the given implementation type
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <typeparam name="TImplementation">The implementation type</typeparam>
        void Bind<TService, TImplementation>();
        void BindSingleton(Type service, Type implementation);
        void BindSingleton<TService, TImplementation>();
        void BindConstant(Type service, object instance);
        void BindConstant<TService>(TService implementation);

        void BindToFunction(Type service, Func<IIoCInjector, object> func);
        void BindToFunction<TService>(Func<IIoCInjector, TService> func);
    }
}
