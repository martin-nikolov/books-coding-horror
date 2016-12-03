namespace ProgrammingInCSharp.ConfigureAwait
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random rnd = new Random();

        public MainWindow()
        {
            this.InitializeComponent();

            var timer = new DispatcherTimer() { Interval = new TimeSpan(1000) };
            timer.Tick += this.OnTimerElapsed;

            timer.Start();
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            this.currentTimerLabel.Content = DateTime.Now.ToLongTimeString();
        }

        private async void OnWriteResponseAsyncButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ShowResponseContentLabels("Waiting...");
            var randomUrl = this.GetRandomUrl();

            using (var httpClient = new HttpClient())
            {
                var content = await httpClient.GetStringAsync(randomUrl);

                this.ShowResponseContentLabels("Writing to file...");

                await this.WriteToFile(content);

                this.ShowResponseContentLabels("Done...");
            }
        }

        private async Task WriteToFile(string content)
        {
            var outputFilePath = "../../output.txt";

            try
            {
                using (var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                {
                    var encodedText = Encoding.Unicode.GetBytes(content);

                    await fileStream.WriteAsync(encodedText, 0, encodedText.Length);
                }
            }
            finally
            {
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }
            }
        }

        private void OnWriteResponseSyncButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ShowResponseContentLabels("Waiting...");
            var randomUrl = this.GetRandomUrl();

            var httpWebRequest = WebRequest.Create(randomUrl);
            var response = httpWebRequest.GetResponse();

            this.ShowResponseContentLabels(response.ContentLength.ToString());
        }

        private string GetRandomUrl()
        {
            var urls = new string[] { "http://microsoft.com", "http://msdn.microsoft.com", "http://blogs.msdn.com" };
            var randomUrl = urls[this.rnd.Next(0, urls.Length)];

            return randomUrl;
        }

        private void ShowResponseContentLabels(string content)
        {
            this.responseContentLengthLabel.Content = content;
            this.responseContentLengthLabel.Visibility = Visibility.Visible;
            this.responseContentLabel.Visibility = Visibility.Visible;
        }
    }
}