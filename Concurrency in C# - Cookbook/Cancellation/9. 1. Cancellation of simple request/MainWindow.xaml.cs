namespace CancellationOfSimpleRequest
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            this.startButton.IsEnabled = false;
            this.cancelButton.IsEnabled = true;

            try
            {
                this.cancellationTokenSource = new CancellationTokenSource();

                var token = this.cancellationTokenSource.Token;

                await Task.Delay(TimeSpan.FromSeconds(5), token);

                MessageBox.Show("Delay completed successfully.");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Delay was canceled.");
            }
            catch (Exception)
            {
                MessageBox.Show("Delay completed with error.");
            }
            finally
            {
                this.startButton.IsEnabled = true;
                this.cancelButton.IsEnabled = false;
            }
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            this.cancellationTokenSource?.Cancel();
        }
    }
}