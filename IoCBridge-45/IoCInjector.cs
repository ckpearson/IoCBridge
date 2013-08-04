using System.Linq;


namespace IoCBridge
{
    public sealed class IoCInjector : 
        IIoCInjector
    {
        private readonly NamedArg[] _args = new NamedArg[] { };
        private readonly IIoC _ioc;

        public IoCInjector(IIoC ioc, params NamedArg[] args)
        {
            if (args == null) args = new NamedArg[] { };

            _ioc = ioc;
        }

        public T Inject<T>(params NamedArg[] args)
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
