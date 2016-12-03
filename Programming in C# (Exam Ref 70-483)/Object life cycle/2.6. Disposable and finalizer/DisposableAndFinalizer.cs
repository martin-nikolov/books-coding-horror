namespace ProgrammingInCSharp.DisposableAndFinalizer
{
    using System;
    using System.IO;

    public class DisposableAndFinalizer
    {
        private static void Main()
        {
        }
    }

    public class UnmanagedWrapper : IDisposable
    {
        public FileStream Stream { get; private set; }

        public UnmanagedWrapper()
        {
            //this.Stream = ...
        }

        ~UnmanagedWrapper()
        {
            this.Dispose(false);
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Stream?.Close();
            }
        }
    }
}