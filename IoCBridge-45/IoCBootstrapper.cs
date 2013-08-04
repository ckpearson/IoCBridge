using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCBridge
{
    public abstract class IoCBootstrapper :
        IIoCBoostrapper,
        IIoC
    {
        private readonly List<Binding> _bindings = new List<Binding>();

        public IoCBootstrapper()
        {

        }

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

        protected abstract void OnStart();
        protected abstract void OnInitialise();

        public void Start()
        {
            DoBindConstant(typeof(IIoC), this);
            DoBindConstant(typeof(IIoCBoostrapper), this);
            DoBind(typeof(IIoCInjector), typeof(IoCInjector), false);

            OnStart();
        }

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

        protected abstract void DoBindConstant(Type service, object instance);
        public void BindConstant(Type service, object instance)
        {
            lock (_bindings)
            {
                var bnd = new ConstantBinding(service, instance);
                DoBindConstant(bnd.ServiceType, instance);
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

        protected abstract object DoGet(Type service, params NamedArg[] args);
        public T Get<T>(params NamedArg[] args)
        {
            return (T)DoGet(typeof(T), args);
        }

        public object Get(Type serviceType, params NamedArg[] args)
        {
            return DoGet(serviceType, args);
        }

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
