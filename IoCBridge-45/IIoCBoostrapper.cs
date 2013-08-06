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

        /// <summary>
        /// Binds the given service type to the given implementation type as a singleton
        /// </summary>
        /// <param name="service">The service type</param>
        /// <param name="implementation">The implementation type</param>
        void BindSingleton(Type service, Type implementation);

        /// <summary>
        /// Binds the given service type to the given implementation type as a singleton
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <typeparam name="TImplementation">The implementation type</typeparam>
        void BindSingleton<TService, TImplementation>();

        /// <summary>
        /// Binds the given service type to the given constant
        /// </summary>
        /// <param name="service">The service type</param>
        /// <param name="constant">The constant</param>
        void BindConstant(Type service, object constant);

        /// <summary>
        /// Binds the given service type to the given constant
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <param name="constant">The constant</param>
        void BindConstant<TService>(TService constant);

        /// <summary>
        /// Binds the given service type to the given provider function
        /// </summary>
        /// <param name="service">The service type</param>
        /// <param name="func">The provider function</param>
        void BindToFunction(Type service, Func<IIoCInjector, object> func);

        /// <summary>
        /// Binds the given service type to the given provider function
        /// </summary>
        /// <typeparam name="TService">The service type</typeparam>
        /// <param name="func">The provider function</param>
        void BindToFunction<TService>(Func<IIoCInjector, TService> func);
    }
}
