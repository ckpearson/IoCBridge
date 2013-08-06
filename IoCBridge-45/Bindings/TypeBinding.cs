using System;

namespace IoCBridge.Bindings
{
    /// <summary>
    /// Represents a binding to an implementation type
    /// </summary>
    public class TypeBinding : Binding
    {
        private readonly Type _implementationType;
        private readonly bool _isSingleton = false;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="serviceType">The service type for the binding</param>
        /// <param name="implementationType">The implementation type for the binding</param>
        /// <param name="singleton">Whether the binding should be to an eventual singleton</param>
        public TypeBinding(Type serviceType, Type implementationType, bool singleton = false)
            : base(serviceType)
        {
            _implementationType = implementationType;
            _isSingleton = singleton;
        }

        /// <summary>
        /// Gets the implementation type
        /// </summary>
        public Type ImplementationType
        {
            get { return _implementationType; }
        }

        /// <summary>
        /// Gets whether the binding is for an eventual singleton
        /// </summary>
        public bool IsSingleton
        {
            get { return _isSingleton; }
        }
    }

    /// <summary>
    /// Represents a binding to an implementation type
    /// </summary>
    /// <typeparam name="TService">The service type for the binding</typeparam>
    /// <typeparam name="TImplementation">The implementation type for the binding</typeparam>
    public class TypeBinding<TService, TImplementation> : TypeBinding
    {
        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="singleton">Whether the binding is for an eventual singleton</param>
        public TypeBinding(bool singleton = false) :
            base(typeof(TService), typeof(TImplementation), singleton)
        {

        }
    }
}
