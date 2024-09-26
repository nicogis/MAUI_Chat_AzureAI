using ChatAI.ViewModels;



namespace ChatAI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AboutPage : ContentPage
{

    public AboutPage(AboutViewModel aboutViewModel)
    {


        InitializeComponent();

        BindingContext = aboutViewModel;




    }
}