namespace Liner.App.Mappers
{
    public static class UserIterfaceToModelsMappers
    {
        public static Models.Point MapToPoint(this System.Windows.Point point) =>
            new Models.Point((int)point.X, (int)point.Y);
    }
}
