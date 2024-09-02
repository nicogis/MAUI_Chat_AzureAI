using ChatAI.ViewModels;

namespace ChatAI.Views
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
            CurrentItem = ChatTab;
        }
    }
}