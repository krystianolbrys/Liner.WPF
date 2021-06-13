using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Liner.App.IoC;
using Liner.App.Loggers;
using Liner.App.Mappers;
using Liner.App.Models;
using Liner.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Contracts = Liner.API.Contracts;

namespace Liner.App
{
    public partial class MainWindow : Window
    {
        private readonly Contracts.ILinerApiService _linerService;
        private readonly ILogger _logger;
        private readonly PointsSelector _pointsSelector;
        private readonly ICollection<Contracts.Common.Line> _linesCollection;

        public MainWindow()
        {
            InitializeComponent();
            var provider = new ServiceProviderFactory().Provide();

            _linerService = provider.GetRequiredService<Contracts.ILinerApiService>()
                ?? throw new ArgumentNullException(nameof(_linerService));

            _pointsSelector = provider.GetRequiredService<PointsSelector>()
                 ?? throw new ArgumentNullException(nameof(_pointsSelector));

            _linesCollection = new List<Contracts.Common.Line>();

            _logger = new UILogger(logBox);

            _logger.Log("Liner.App - Started");
        }

        public async void CanvaClickEventHandler(object sender, MouseButtonEventArgs eventArguments)
        {
            var point = GetPointRelativeToClickedElement(eventArguments)
                .MapToPoint();

            _pointsSelector.Add(point);

            _logger.Log(_pointsSelector);

            if (!_pointsSelector.PointSelectionCompleted)
            {
                return;
            }

            var request = new Contracts.Requests.GetPathRequest
            {
                Start = new Contracts.Common.Point { X = _pointsSelector.Start.X, Y = _pointsSelector.Start.Y },
                End = new Contracts.Common.Point { X = _pointsSelector.End.X, Y = _pointsSelector.End.Y }
            };

            var result = await _linerService.GetPath(request);

            foreach (var line in result.Lines)
            {
                _linesCollection.Add(line);
                DrawLineOnCanva(line, mainCanva);
            }

            _logger.Log(_linesCollection);
        }

        private void DrawLineOnCanva(Contracts.Common.Line line, System.Windows.Controls.Canvas canva)
        {
            Line lineOnCanva = new Line
            {
                Stroke = Brushes.Black,
                X1 = line.Start.X,
                Y1 = line.Start.Y,
                X2 = line.End.X,
                Y2 = line.End.Y,
                StrokeThickness = 1
            };

            canva.Children.Add(lineOnCanva);
        }

        private System.Windows.Point GetPointRelativeToClickedElement(MouseButtonEventArgs eventArgument)
        {
            if (!(eventArgument.Source is IInputElement source))
            {
                throw new ArgumentNullException($"{nameof(source)} is not IInputElement type - cannot get clicked position");
            }

            return eventArgument.GetPosition(source);
        }
    }
}
