using System.Linq;


namespace IoCBridge
{
    /// <summary>
    /// Injector provided to provider functions for retrieving args and other types
    /// </summary>
    public sealed class IoCInjector : 
        IIoCInjector
    {
        private readonly CtorArg[] _args = new CtorArg[] { };
        private readonly IIoC _ioc;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="ioc">The IoC access instance</param>
        /// <param name="args">The ctor arguments for this request</param>
        public IoCInjector(IIoC ioc, params CtorArg[] args)
        {
            if (args == null) args = new CtorArg[] { };

            _ioc = ioc;
        }

        public T Inject<T>(params CtorArg[] args)
        {
            return _ioc.Get<T>(args);
        }

        public T Arg<T>(int index)
        {
            return (T)_args[index].Value;
        }

        public T Arg<T>(string name)
        {
            return (T)_args.Single(a => a.Name == name).Value;
        }
    }
}
