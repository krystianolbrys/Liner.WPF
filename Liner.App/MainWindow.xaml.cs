using System;
using System.Windows;
using System.Windows.Input;
using Liner.API.Contracts;
using Liner.API.Contracts.Requests;
using Liner.App.IoC;
using Liner.App.Loggers;
using Liner.App.Models;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Liner.App
{
    public partial class MainWindow : Window
    {
        private readonly ILinerApiService _linerService;
        private readonly ILogger _logger;
        private readonly PointsSelector _pointsSelector;

        public MainWindow()
        {
            InitializeComponent();
            var provider = new ServiceProviderFactory().Provide();

            _linerService = provider.GetRequiredService<ILinerApiService>()
                ?? throw new ArgumentNullException(nameof(_linerService));

            _pointsSelector = provider.GetRequiredService<PointsSelector>()
                 ?? throw new ArgumentNullException(nameof(_pointsSelector));

            _logger = new UILogger(logBox);

            _logger.Log("Liner.App - Started");
        }

        public void CanvaClickEventHandler(object sender, MouseButtonEventArgs eventArgument)
        {
            var point = eventArgument.GetPosition((IInputElement)eventArgument.OriginalSource);
            var mappedPoint = new Models.Point((int)point.X, (int)point.Y);

            _pointsSelector.Add(mappedPoint);

            _logger.Log(_pointsSelector.ToString());

            if (_pointsSelector.Status == Models.Enums.SelectedPointsStatusEnum.StartAndEndSelected)
            {
                var request = new TwoPointsRequest
                {
                    Start = new API.Contracts.Requests.Point { X = _pointsSelector.Start.X, Y = _pointsSelector.Start.Y },
                    End = new API.Contracts.Requests.Point { X = _pointsSelector.End.X, Y = _pointsSelector.End.Y }
                };

                var result = _linerService.GetPath(request);

                _logger.Log(JsonConvert.SerializeObject(result));
            }
        }
    }
}
