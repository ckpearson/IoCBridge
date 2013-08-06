using System;

namespace IoCBridge.Bindings
{
    /// <summary>
    /// Represents a binding to a constant
    /// </summary>
    public class ConstantBinding : Binding
    {
        private readonly object _constant;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="serviceType">The service type for the binding</param>
        /// <param name="constant">The constant to bind to</param>
        public ConstantBinding(Type serviceType, object constant)
            : base(serviceType)
        {
            _constant = constant;
        }

        /// <summary>
        /// Gets the constant of the binding
        /// </summary>
        public object Constant
        {
            get { return _constant; }
        }
    }

    /// <summary>
    /// Represents a binding to the constant
    /// </summary>
    /// <typeparam name="TService">The service type for the binding</typeparam>
    public class ConstantBinding<TService> : ConstantBinding
    {
        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="constant">The constant to bind to</param>
        public ConstantBinding(TService constant)
            : base(typeof(TService), constant)
        {

        }

        /// <summary>
        /// Gets the constant of the binding
        /// </summary>
        public new TService Constant
        {
            get { return (TService)base.Constant; }
        }
    }
}
