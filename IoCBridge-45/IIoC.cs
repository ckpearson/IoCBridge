using System;

namespace IoCBridge
{
    /// <summary>
    /// IoC access interface
    /// </summary>
    public interface IIoC
    {
        /// <summary>
        /// Gets an instance of the given type using the provided parameters
        /// </summary>
        /// <typeparam name="T">The service type to get an instance for</typeparam>
        /// <param name="args">The constructor arguments</param>
        /// <returns>The requested instance</returns>
        T Get<T>(params NamedArg[] args);

        /// <summary>
        /// Gets an instance of the given type using the provided parameters
        /// </summary>
        /// <param name="serviceType">The service type to get an instance of</param>
        /// <param name="args">The constructor arguments</param>
        /// <returns>The requested instance</returns>
        object Get(Type serviceType, params NamedArg[] args);

        /// <summary>
        /// Gets whether a given service type has a binding
        /// </summary>
        /// <typeparam name="T">The service type to check</typeparam>
        /// <returns><c>True</c> if the service type has a binding, <c>False</c> otherwise</returns>
        bool IsTypeBound<T>();

        /// <summary>
        /// Gets whether a given service type has a binding
        /// </summary>
        /// <param name="serviceType">The service type to check</param>
        /// <returns><c>True</c> if the service type has a binding, <c>False</c> otherwise</returns>
        bool IsTypeBound(Type serviceType);
    }
}
