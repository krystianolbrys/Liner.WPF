using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Contracts.ILinerApiService _linerService;
        private readonly ILogger _logger;
        private readonly PointsSelector _pointsSelector;
        private readonly ICollection<Contracts.Common.Line> _linesCollection;

        private int _brushCounter;

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

            _brushCounter = 0;

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
                End = new Contracts.Common.Point { X = _pointsSelector.End.X, Y = _pointsSelector.End.Y },
                ExistingLines = _linesCollection.Select(line => new Contracts.Common.Line
                {
                    Start = new Contracts.Common.Point { X = line.Start.X, Y = line.Start.Y },
                    End = new Contracts.Common.Point { X = line.End.X, Y = line.End.Y }
                }).ToList().AsReadOnly(),
                Boundaries = new Contracts.Requests.Boundaries
                {
                    MaxWidth = (int)mainCanva.Width,
                    MaxHeight = (int)mainCanva.Height
                }
            };
            Stopwatch sw = Stopwatch.StartNew();
            var result = await _linerService.GetPath(request);

            var brushColor = GenerateBrushColor();

            

            foreach (var line in result.Lines)
            {
                _linesCollection.Add(line);
                DrawLineOnCanva(line, mainCanva, brushColor);
            }

            sw.Stop();
            _logger.Log(sw.ElapsedMilliseconds);
        }

        private void DrawLineOnCanva(Contracts.Common.Line line, System.Windows.Controls.Canvas canva, SolidColorBrush color)
        {
            Line lineOnCanva = new Line
            {
                Stroke = color,
                X1 = line.Start.X,
                Y1 = line.Start.Y,
                X2 = line.End.X,
                Y2 = line.End.Y,
                StrokeThickness = 1
            };

            canva.Children.Add(lineOnCanva);
        }

        private SolidColorBrush GenerateBrushColor()
        {
            var brushes = new List<SolidColorBrush>
            {
                Brushes.Green,
                Brushes.Goldenrod,
                Brushes.Black,
                Brushes.Blue,
                Brushes.Red,
                Brushes.Brown,
                Brushes.DarkViolet
            };

            return brushes[_brushCounter++ % brushes.Count];
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
