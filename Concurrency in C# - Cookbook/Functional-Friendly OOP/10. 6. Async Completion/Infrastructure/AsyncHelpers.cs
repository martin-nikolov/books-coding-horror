namespace AsyncCompletion.Infrastructure
{
    using System;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks;
    using AsyncCompletion.Abstract;

    public static class AsyncHelpers
    {
        public static async Task Using<TResource>(Func<TResource> construct, Func<TResource, Task> process) where TResource : IAsyncCompletion
        {
            // Create the resource we're using.
            var resource = construct();

            // Use the resource, catching any exceptions.
            Exception exception = null;
            try
            {
                await process(resource);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Complete (logically dispose) the resource.
            resource.Complete();
            await resource.Completion;

            // Re-throw the process delegate exception if necessary.
            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }
        }

        public static async Task<TResult> Using<TResource, TResult>(Func<TResource> construct, Func<TResource, Task<TResult>> process)
            where TResource : IAsyncCompletion
        {
            // Create the resource we're using.
            var resource = construct();

            // Use the resource, catching any exceptions.
            Exception exception = null;
            var result = default(TResult);
            try
            {
                result = await process(resource);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Complete (logically dispose) the resource.
            resource.Complete();
            try
            {
                await resource.Completion;
            }
            catch
            {
                // Only allow exceptions from Completion if the process
                // delegate did not throw an exception.
                if (exception == null)
                {
                    throw;
                }
            }

            // Re-throw the process delegate exception if necessary.
            if (exception != null)
            {
                ExceptionDispatchInfo.Capture(exception).Throw();
            }

            return result;
        }
    }
}