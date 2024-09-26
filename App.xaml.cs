using ChatAI.Services;
using ChatAI.Views;
using Application = Microsoft.Maui.Controls.Application;

namespace ChatAI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            DependencyService.Register<NavigationService>();
            MainPage = new MainPage();
            
        }
    }
}
