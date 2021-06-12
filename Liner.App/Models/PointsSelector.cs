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
                }
            };
    }
}
