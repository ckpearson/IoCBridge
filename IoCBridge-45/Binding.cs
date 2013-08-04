using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoCBridge
{
    public abstract class Binding
    {
        private readonly Type _serviceType;

        public Binding(Type serviceType)
        {
            _serviceType = serviceType;
        }

        public Type ServiceType
        {
            get { return _serviceType; }
        }
    }

    public abstract class Binding<TService> : Binding
    {
        public Binding() : base(typeof(TService))
        {

        }
    }

    public class TypeBinding : Binding
    {
        private readonly Type _implementationType;
        private readonly bool _isSingleton = false;

        public TypeBinding(Type serviceType, Type implementationType, bool singleton = false)
            : base(serviceType)
        {
            _implementationType = implementationType;
            _isSingleton = singleton;
        }

        public Type ImplementationType
        {
            get { return _implementationType; }
        }

        public bool IsSingleton
        {
            get { return _isSingleton; }
        }
    }

    public class TypeBinding<TService, TImplementation> : TypeBinding
    {
        public TypeBinding(bool singleton = false) : 
            base(typeof(TService), typeof(TImplementation), singleton)
        {

        }
    }

    public class ConstantBinding : Binding
    {
        private readonly object _constant;

        public ConstantBinding(Type serviceType, object constant)
            : base(serviceType)
        {
            _constant = constant;
        }

        public object Constant
        {
            get { return _constant; }
        }
    }

    public class ConstantBinding<TService> : ConstantBinding
    {
        public ConstantBinding(TService constant)
            : base(typeof(TService), constant)
        {

        }

        public new TService Constant
        {
            get { return (TService)base.Constant; }
        }
    }

    public class FuncBinding : Binding
    {
        private readonly Func<IIoCInjector, object> _func;

        public FuncBinding(Type service, Func<IIoCInjector, object> func)
            : base(service)
        {
            _func = func;
        }

        public Func<IIoCInjector, object> Func
        {
            get { return _func; }
        }
    }

    public class FuncBinding<TService> : FuncBinding
    {
        private readonly Func<IIoCInjector, TService> _func;

        public FuncBinding(Func<IIoCInjector, TService> func)
            : base(typeof(TService), (injector) => func(injector))
        {
            _func = func;
        }

        public new Func<IIoCInjector, TService> Func
        {
            get { return _func; }
        }

        public Func<IIoCInjector, object> BaseFunc
        {
            get { return base.Func; }
        }
    }
}
