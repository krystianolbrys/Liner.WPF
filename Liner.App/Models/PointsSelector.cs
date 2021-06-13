using System;
using System.Collections.Generic;
using Liner.App.Models.Enums;

namespace Liner.App.Models
{
    public class PointsSelector
    {
        private readonly IDictionary<SelectedPointsStatusEnum, Action<Point>> _strategies;

        public Point Start { get; private set; }
        public Point End { get; private set; }
        public SelectedPointsStatusEnum Status { get; private set; }

        public PointsSelector()
        {
            _strategies = CreateStrategies;

            Status = SelectedPointsStatusEnum.NoneSelected;
        }

        public void Add(Point point)
        {
            if (point == null)
            {
                throw new ArgumentNullException(nameof(point));
            }

            _strategies[Status](point);
        }

        public bool PointSelectionCompleted => Status == SelectedPointsStatusEnum.StartAndEndSelected;

        public override string ToString()
        {
            if (Status == SelectedPointsStatusEnum.NoneSelected)
            {
                return "None Selected";
            }

            if (Status == SelectedPointsStatusEnum.StartSelected)
            {
                return $"Start: {Start} - End: []";
            }

            if (Status == SelectedPointsStatusEnum.StartAndEndSelected)
            {
                return $"Start: {Start} - End: {End}";
            }

            throw new ArgumentOutOfRangeException();
        }

        private IDictionary<SelectedPointsStatusEnum, Action<Point>> CreateStrategies =>
            new Dictionary<SelectedPointsStatusEnum, Action<Point>>
            {
                { SelectedPointsStatusEnum.NoneSelected, (point) =>
                    {
                        Start = point;
                        Status = SelectedPointsStatusEnum.StartSelected;
                    }
                },
                { SelectedPointsStatusEnum.StartSelected, (point) =>
                    {
                        End = point;
                        Status = SelectedPointsStatusEnum.StartAndEndSelected;
                    }
                },
                { SelectedPointsStatusEnum.StartAndEndSelected, (point) =>
                    {
                        Start = point;
                        End = null;
                        Status = SelectedPointsStatusEnum.StartSelected;
                    }
                }
            };
    }
}
