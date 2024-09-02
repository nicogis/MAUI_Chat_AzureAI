using DevExpress.Maui.DataForm;
using ChatAI.Resources;
using ChatAI.ViewModels;



namespace ChatAI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SettingsPage : ContentPage
{
    
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
        
        InitializeComponent();
        BindingContext = settingsViewModel;
    }

    private void DFVCredential_ValidateProperty(object sender, DataFormPropertyValidationEventArgs e)
    {
        if ((e.PropertyName == "EndPoint") || (e.PropertyName == "ApiKey"))
        {
            if (string.IsNullOrWhiteSpace(e.NewValue.ToString()))
            {
                e.HasError = true;
                e.ErrorText = AppResources.SettingsStringId_NoValidValueCredential;
            }
        }

    }

    private async void DXButton_Clicked(object sender, EventArgs e)
    {
        if (!DFVCredential.Validate())
        {
            return;
        }
        
        DFVCredential.Commit();
        
        await ((SettingsViewModel)BindingContext).OnSaveSettings();
    }
}