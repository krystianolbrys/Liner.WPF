using System.Windows;
using Liner.API.Contracts;
using Liner.API.Contracts.Requests;
using Liner.API.Service.IoC;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Liner.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILinerApiService _api;

        public MainWindow()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            IServiceInstaller installer = new LinerApiServiceInstaller();
            installer.Install(services);
            var provider = services.BuildServiceProvider();

            _api = provider.GetRequiredService<ILinerApiService>();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var request = new TwoPointsRequest
            {
                Start = new API.Contracts.Requests.Point { X = 10, Y = 50 },
                End = new API.Contracts.Requests.Point { X = 100, Y = 140 }
            };

            var result = _api.GetPath(request);

            var logMsg = JsonConvert.SerializeObject(result);

            System.Console.WriteLine(logMsg);
        }
    }
}
