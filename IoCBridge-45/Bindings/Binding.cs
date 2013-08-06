using System;

namespace IoCBridge.Bindings
{
    /// <summary>
    /// Representation of an IoC binding registered via bridge
    /// </summary>
    public abstract class Binding
    {
        private readonly Type _serviceType;

        /// <summary>
        /// Instantiaes a new instance
        /// </summary>
        /// <param name="serviceType">The service type for the binding</param>
        public Binding(Type serviceType)
        {
            _serviceType = serviceType;
        }

        /// <summary>
        /// Gets the service type of the binding
        /// </summary>
        public Type ServiceType
        {
            get { return _serviceType; }
        }
    }

    /// <summary>
    /// Representation of an IoC binding registered via bridge
    /// </summary>
    /// <typeparam name="TService">The service type for the binding</typeparam>
    public abstract class Binding<TService> : Binding
    {
        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        public Binding() : base(typeof(TService))
        {

        }
    }
}