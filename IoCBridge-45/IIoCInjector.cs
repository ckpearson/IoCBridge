
namespace IoCBridge
{
    /// <summary>
    /// Injector interface used for provider functions
    /// </summary>
    public interface IIoCInjector
    {
        /// <summary>
        /// Gets an instance of the given service type using the provided arguments
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <param name="args">The constructor arguments</param>
        /// <returns>The instance</returns>
        T Inject<T>(params CtorArg[] args);

        /// <summary>
        /// Gets the constructor argument provided to the function at the given index
        /// </summary>
        /// <typeparam name="T">The type of the argument</typeparam>
        /// <param name="index">The index of the argument</param>
        /// <returns>The argument at the given index</returns>
        T Arg<T>(int index);

        /// <summary>
        /// Gets the constructor argument provided to the function with the given name
        /// </summary>
        /// <typeparam name="T">The type of the argument</typeparam>
        /// <param name="name">The name of the argument</param>
        /// <returns>The argument with the given name</returns>
        T Arg<T>(string name);
    }
}
