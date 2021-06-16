using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

/// <summary>
/// For that project some fetures should be implemented but i didn't have so much time:
/// 0.1 - Liner.API for simplicity are class libraries, in production env should be external API and publicated contracts
/// 0.2 - Proper UnitTest should be written for whole Lines.Core domain business logic models
/// 0.3 - UnitTests with testing framework like NUnit, libraries - AuotFixture, FluentAssertions, Moq
/// 1 - ILogger implementation for storing logs in some persist storage
/// 2 - Some kind of AoP approach for logging business Exceptions from API
/// 3 - Maybe AutoMapper library for mapping Contracts <-> CommandRequests <-> BuildCoreDomainModels (I preffer explicit creation of DomainModels)
/// 4.1 - BFS would be significantly faster with dedicated nodes not Node<TValue>, explicit memory allocations(unsafe)
/// 4.2 - In this case data volume is very small - only 206k nodes with max 4 childrens
/// 4.2 - For bigger volume more complex algorithm should be used including paraller execution
/// </summary>

namespace Liner.App
{
    public partial class MainWindow : Window
    {
        private readonly Contracts.ILinerApiService _linerService;
        private readonly ILogger _logger;
        private readonly PointsSelector _pointsSelector;
        private readonly ICollection<Contracts.Common.TwoPointLine> _linesCollection;

        private int _brushCounter;

        public MainWindow()
        {
            InitializeComponent();
            var provider = new ServiceProviderFactory().Provide();

            _linerService = provider.GetRequiredService<Contracts.ILinerApiService>()
                ?? throw new ArgumentNullException(nameof(_linerService));

            _pointsSelector = provider.GetRequiredService<PointsSelector>()
                 ?? throw new ArgumentNullException(nameof(_pointsSelector));

            _linesCollection = new List<Contracts.Common.TwoPointLine>();

            _logger = new UILogger(logBox);

            _brushCounter = 0;

            _logger.Log("Liner.App - Started");
            _logger.Log(_pointsSelector);
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

            // should be provided from userInput, appSetting, DB or something else
            var lineMarginInPixels = 3; // 

            var request = BuildRequest(mainCanva, _pointsSelector, _linesCollection, lineMarginInPixels);

            var result = await _linerService.GetPath(request);

            if (result.IsPathFound)
            {
                _logger.Log("Path from start to end FOUND");
                var brushColorForPath = GenerateBrushColor();

                foreach (var line in result.TwoPointLines)
                {
                    _linesCollection.Add(line);
                    DrawLineOnCanva(line, mainCanva, brushColorForPath);
                }
            }
            else
            {
                _logger.Log("Path from start to end NOT FOUND - Try other points or shrink line margin");
            }

            _pointsSelector.Reset();
            _logger.Log(_pointsSelector);
        }

        private Contracts.Requests.GetPathRequest BuildRequest(
            Canvas canva, 
            PointsSelector pointsSelector, 
            ICollection<Contracts.Common.TwoPointLine> lines,
            int lineMarginInPixels)
        {
            return new Contracts.Requests.GetPathRequest
            {
                Start = new Contracts.Common.Point { X = pointsSelector.Start.X, Y = pointsSelector.Start.Y },
                End = new Contracts.Common.Point { X = pointsSelector.End.X, Y = pointsSelector.End.Y },
                ExistingLines = lines.Select(line => new Contracts.Common.TwoPointLine
                {
                    Start = new Contracts.Common.Point { X = line.Start.X, Y = line.Start.Y },
                    End = new Contracts.Common.Point { X = line.End.X, Y = line.End.Y }
                }).ToList().AsReadOnly(),
                Configuration = new Contracts.Requests.Configuration
                {
                    Width = (int)canva.Width,
                    Height = (int)canva.Height,
                    LineMarginInPixels = lineMarginInPixels
                }
            };
        }

        private void DrawLineOnCanva(Contracts.Common.TwoPointLine line, System.Windows.Controls.Canvas canva, SolidColorBrush color)
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
