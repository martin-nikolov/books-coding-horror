namespace AsyncCompletion.Abstract
{
    using System.Threading.Tasks;

    /// <summary>
    /// Marks a type as requiring asynchronous completion and provides
    /// the result of that completion.
    /// </summary>
    public interface IAsyncCompletion
    {
        /// <summary>
        /// Gets the result of the completion of this instance.
        /// </summary>
        Task Completion { get; }

        /// <summary>
        /// Starts the completion of this instance. This is conceptually similar
        /// to <see cref="System.IDisposable.Dispose" />.
        /// After you call this method, you should not invoke any other members of
        /// this instance except <see cref="Completion" />.
        /// </summary>
        void Complete();
    }
}