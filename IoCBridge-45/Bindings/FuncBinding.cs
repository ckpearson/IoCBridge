using System;

namespace IoCBridge.Bindings
{
    /// <summary>
    /// Represents a binding to a provider function
    /// </summary>
    public class FuncBinding : Binding
    {
        private readonly Func<IIoCInjector, object> _func;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="service">The service type for the binding</param>
        /// <param name="func">The provider function to bind to</param>
        public FuncBinding(Type service, Func<IIoCInjector, object> func)
            : base(service)
        {
            _func = func;
        }

        /// <summary>
        /// Gets the provider function
        /// </summary>
        public Func<IIoCInjector, object> Func
        {
            get { return _func; }
        }
    }

    /// <summary>
    /// Represents a binding to a provider function
    /// </summary>
    /// <typeparam name="TService">The service type for the binding</typeparam>
    public class FuncBinding<TService> : FuncBinding
    {
        private readonly Func<IIoCInjector, TService> _func;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="func">The provider function to bind to</param>
        public FuncBinding(Func<IIoCInjector, TService> func)
            : base(typeof(TService), (injector) => func(injector))
        {
            _func = func;
        }

        /// <summary>
        /// Gets the strongly typed provider function
        /// </summary>
        public new Func<IIoCInjector, TService> Func
        {
            get { return _func; }
        }

        /// <summary>
        /// Gets the underlying non-generic provider function
        /// </summary>
        public Func<IIoCInjector, object> BaseFunc
        {
            get { return base.Func; }
        }
    }
}
