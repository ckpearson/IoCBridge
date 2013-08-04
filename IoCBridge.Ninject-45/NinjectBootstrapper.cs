using Ninject;
using Ninject.Parameters;
using System;
using System.Linq;

namespace IoCBridge.Ninject
{
    public sealed class NinjectBootstrapper
        : IoCBootstrapper
    {
        private readonly IKernel _kernel;

        public NinjectBootstrapper(IKernel kernel)
        {
            _kernel = kernel;
        }

        public NinjectBootstrapper()
        {
            _kernel = new StandardKernel();
        }

        protected override void OnInitialise()
        {
        }

        protected override void OnStart()
        {
        }

        protected override void DoBind(Type service, Type implementation, bool singleton)
        {
            if (singleton)
            {
                _kernel.Bind(service).To(implementation).InSingletonScope();
            }
            else
            {
                _kernel.Bind(service).To(implementation);
            }
        }

        protected override void DoBindConstant(Type service, object instance)
        {
            _kernel.Bind(service).ToConstant(instance);
        }

        protected override void DoBindFunc(Type service, Func<IIoCInjector, object> func)
        {
            _kernel.Bind(service).ToMethod((ctx) =>
                {
                    var injector = ((IIoC)this).Get<IIoCInjector>(new[]
                    {
                        new NamedArg("ioc", this),
                        new NamedArg("args", ctx.Parameters.Select(p =>
                            new NamedArg(
                                p.Name,
                                p.GetValue(ctx, ctx.Request.Target))).ToArray()),
                    });
                    return func(injector);
                });
        }

        protected override object DoGet(Type service, params NamedArg[] args)
        {
            if (args == null || args.Length == 0)
            {
                return _kernel.Get(service);
            }
            else
            {
                return _kernel.Get(service, args.Select(a =>
                    new ConstructorArgument(a.Name, a.Value)).ToArray());
            }
        }

        protected override bool DoIsTypeBound(Type serviceType)
        {
            var bindings = _kernel.GetBindings(serviceType);
            return bindings != null && bindings.Count() > 0;
        }
    }
}
