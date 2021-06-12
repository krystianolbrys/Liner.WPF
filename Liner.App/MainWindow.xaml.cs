using System.Windows;
using Liner.Infrastructure;
using Liner.Service.IoC;
using Liner.Service.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Liner.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMediator _mediator;

        public MainWindow()
        {
            InitializeComponent();

            var services = new ServiceCollection();

            IServiceInstaller installer = new LinerServiceInstaller();

            installer.Install(services);

            var provider = services.BuildServiceProvider();

            IMediator mediator = provider.GetRequiredService<IMediator>();

            _mediator = mediator;

            //var result = await mediator.Send(new SampleRequest(23));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = await _mediator.Send(new SampleRequest(23));
            System.Console.WriteLine(result);
        }
    }
}
