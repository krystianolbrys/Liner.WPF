using System;
using System.Windows;
using Liner.API.Contracts;
using Liner.API.Contracts.Requests;
using Liner.App.IoC;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Liner.App
{
    public partial class MainWindow : Window
    {
        private readonly ILinerApiService _linerService;

        public MainWindow()
        {
            InitializeComponent();
            var provider = new IoCProviderFactory().Provide();

            _linerService = provider.GetRequiredService<ILinerApiService>() 
                ?? throw new ArgumentNullException(nameof(_linerService));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var request = new TwoPointsRequest
            {
                Start = new API.Contracts.Requests.Point { X = 10, Y = 50 },
                End = new API.Contracts.Requests.Point { X = 100, Y = 140 }
            };

            var result = _linerService.GetPath(request);

            var logMsg = JsonConvert.SerializeObject(result);

            System.Console.WriteLine(logMsg);
        }
    }
}
