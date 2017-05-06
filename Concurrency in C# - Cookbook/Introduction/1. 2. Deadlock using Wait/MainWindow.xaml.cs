namespace DeadlockUsingWait
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        #region Solution with deadlock

        private void OnWindowsLoaded(object sender, RoutedEventArgs e)
        {
            this.Deadlock();
        }

        private void Deadlock()
        {
            // Start the delay
            var task = this.WaitAsync();

            // Synchronously block, waiting for the async method to complete.
            task.Wait();
        }

        private async Task WaitAsync()
        {
            // The await will capture the current context...
            await Task.Delay(TimeSpan.FromSeconds(1));
            // ... and will attempt to resume the method here in that context

            this.loadedLabel.Visibility = Visibility.Visible;
        }

        #endregion

        #region Solution without deadlock - using .ConfigureAwait(false)
        /*
                private void OnWindowsLoaded(object sender, RoutedEventArgs e)
                {
                    this.Deadlock();
                }

                private void Deadlock()
                {
                    // Start the delay
                    var task = this.WaitAsync();

                    // Synchronously block, waiting for the async method to complete.
                    task.Wait();

                    this.loadedLabel.Visibility = Visibility.Visible;
                }

                private async Task WaitAsync()
                {
                    // The await will capture the current context...
                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                    // ... and will attempt to resume the method here in the thread-pool thread context
                }
        */
        #endregion

        #region Solution without deadlock - using await and async method
        /*
                private async void OnWindowsLoaded(object sender, RoutedEventArgs e)
                {
                    await this.Deadlock();
                }

                private Task Deadlock()
                {
                    // Start the delay
                    var task = this.WaitAsync();

                    return task;
                }

                private async Task WaitAsync()
                {
                    // The await will capture the current context...
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    // ... and will attempt to resume the method here in that context

                    this.loadedLabel.Visibility = Visibility.Visible;
                }
        */
        #endregion
    }
}