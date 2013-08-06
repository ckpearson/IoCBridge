
namespace IoCBridge
{
    /// <summary>
    /// Represents a constructor arg
    /// </summary>
    public sealed class CtorArg
    {
        private readonly string _name;
        private readonly object _value;

        /// <summary>
        /// Instantiates a new instance
        /// </summary>
        /// <param name="name">The name of the argument</param>
        /// <param name="value">The value of the argument</param>
        public CtorArg(string name, object value)
        {
            _name = name;
            _value = value;
        }

        /// <summary>
        /// Gets the name of the argument
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Gets the value of the argument
        /// </summary>
        public object Value { get { return _value; } }
    }
}
