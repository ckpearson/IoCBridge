using IoCBridge.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCBridge
{
    /// <summary>
    /// Abstract base class for providing bootstrappers for use by the bridge
    /// </summary>
    public abstract class IoCBootstrapper :
        IIoCBoostrapper,
        IIoC
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        public IoCBootstrapper()
        {

        }

        /// <summary>
        /// Adds a given binding for a service type or replaces an existing binding
        /// </summary>
        /// <param name="service">The service type</param>
        /// <param name="binding">The associated binding</param>
        private void addOrReplaceBinding(Type service, Binding binding)
        {
            if (service == null) throw new ArgumentNullException("Service type needed");
            if (binding == null) throw new ArgumentNullException("Binding needed");

            if (binding.ServiceType != service)
            {
                throw new ArgumentException("Binding must be of same service type");
            }

            try
            {
                lock (_bindings)
                {
                    Binding bnd = null;
                    bnd = _bindings.SingleOrDefault(b => b.ServiceType.Equals(service));
                    if (bnd == null)
                    {
                        _bindings.Add(binding);
                    }
                    else
                    {
                        _bindings[_bindings.IndexOf(bnd)] = binding;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add or replace binding", ex);
            }
        }

        /// <summary>
        /// When overridden in a derived class allows startup logic to be executed
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// When overridden in a derived class allows initialisation logic to be executed
        /// </summary>
        protected abstract void OnInitialise();

        public void Start()
        {
            OnInitialise();

            DoBindConstant(typeof(IIoC), this);
            DoBindConstant(typeof(IIoCBoostrapper), this);
            DoBind(typeof(IIoCInjector), typeof(IoCInjector), false);

            OnStart();
        }

        /// <summary>
        /// When overridden in a derived class allows handling of type binding requests
        /// </summary>
        /// <param name="service">The service type to be bound</param>
        /// <param name="implementation">The implementation type to be bound to</param>
        /// <param name="singleton">Whether the binding should be for an eventual singleton</param>
        protected abstract void DoBind(Type service, Type implementation, bool singleton);

        public void Bind(Type service, Type implementation)
        {
            lock (_bindings)
            {
                var bnd = new TypeBinding(service, implementation, false);
                DoBind(bnd.ServiceType, implementation, false);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        public void Bind<TService, TImplementation>()
        {
            lock (_bindings)
            {
                var bnd = new TypeBinding<TService, TImplementation>(false);
                DoBind(bnd.ServiceType, bnd.ImplementationType, false);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        public void BindSingleton(Type service, Type implementation)
        {
            lock (_bindings)
            {
                var bnd = new TypeBinding(service, implementation, true);
                DoBind(bnd.ServiceType, bnd.ImplementationType, true);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        public void BindSingleton<TService, TImplementation>()
        {
            lock (_bindings)
            {
                var bnd = new TypeBinding<TService, TImplementation>(true);
                DoBind(bnd.ServiceType, bnd.ImplementationType, true);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        /// <summary>
        /// When overridden in a derived class allows handling of constant binding requests
        /// </summary>
        /// <param name="service">The service type to be bound</param>
        /// <param name="constant">The constant to be bound to</param>
        protected abstract void DoBindConstant(Type service, object constant);
        public void BindConstant(Type service, object constant)
        {
            lock (_bindings)
            {
                var bnd = new ConstantBinding(service, constant);
                DoBindConstant(bnd.ServiceType, constant);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        public void BindConstant<TService>(TService implementation)
        {
            lock (_bindings)
            {
                var bnd = new ConstantBinding<TService>(implementation);
                DoBindConstant(bnd.ServiceType, bnd.Constant);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        /// <summary>
        /// When overridden in a derived class allows handling of provider function binding requests
        /// </summary>
        /// <param name="service">The service type to be bound</param>
        /// <param name="func">The provider function to be bound to</param>
        protected abstract void DoBindFunc(Type service, Func<IIoCInjector, object> func);

        public void BindToFunction(Type service, Func<IIoCInjector, object> func)
        {
            lock (_bindings)
            {
                var bnd = new FuncBinding(service, func);
                DoBindFunc(bnd.ServiceType, bnd.Func);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        public void BindToFunction<TService>(Func<IIoCInjector, TService> func)
        {
            lock (_bindings)
            {
                var bnd = new FuncBinding<TService>(func);
                DoBindFunc(bnd.ServiceType, bnd.BaseFunc);
                addOrReplaceBinding(bnd.ServiceType, bnd);
            }
        }

        /// <summary>
        /// When overridden in a dervived class allows handling of IoC get requests
        /// </summary>
        /// <param name="service">The service type to get an instance for</param>
        /// <param name="args">The constructor arguments</param>
        /// <returns>The requested instance</returns>
        protected abstract object DoGet(Type service, params CtorArg[] args);
        public T Get<T>(params CtorArg[] args)
        {
            return (T)DoGet(typeof(T), args);
        }

        public object Get(Type serviceType, params CtorArg[] args)
        {
            return DoGet(serviceType, args);
        }

        /// <summary>
        /// When overridden in a derived class allows handling of IoC type-bound check requests
        /// </summary>
        /// <param name="serviceType">The service type to check for the existence of a binding</param>
        /// <returns><c>True</c> if a binding exists, <c>False</c> otherwise</returns>
        protected abstract bool DoIsTypeBound(Type serviceType);
        public bool IsTypeBound<T>()
        {
            return DoIsTypeBound(typeof(T));
        }

        public bool IsTypeBound(Type serviceType)
        {
            return DoIsTypeBound(serviceType);
        }
    }
}
