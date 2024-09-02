using CommunityToolkit.Maui.Alerts;
using ChatAI.Models;
using ChatAI.Resources;
using ChatAI.Services;

namespace ChatAI.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public const string ViewName = "SettingsPage";

    private readonly IOpenAIService openAIService;

    public SettingsViewModel(IOpenAIService openAIService)
    {
        Title = AppResources.MainStringId_TabSettings;

        CredentialDataObject = new Credential();

        CommandInit = new Command(Init);

        this.openAIService = openAIService;
    }





    public Credential CredentialDataObject
    {
        get;
    }

    public Command CommandInit { get; }

    async void Init()
    {
        string endPoint;
        string apiKey;
        string chatClientDeploymentName;
        string imageClientDeploymentName;

        try
        {
            endPoint = await SecureStorage.GetAsync(nameof(this.CredentialDataObject.EndPoint));
            apiKey = await SecureStorage.GetAsync(nameof(this.CredentialDataObject.ApiKey));
            chatClientDeploymentName = await SecureStorage.GetAsync(nameof(this.CredentialDataObject.ChatClientDeploymentName));
            imageClientDeploymentName = await SecureStorage.GetAsync(nameof(this.CredentialDataObject.ImageClientDeploymentName));
        }
        catch
        {
            await Shell.Current.DisplayAlert(AppResources.SystemStringId_Alert_TitleError, AppResources.SettingsStringId_ErrorRetrievingCredential, AppResources.SystemStringId_Alert_ButtonCancel);
            return;
        }


        this.CredentialDataObject.EndPoint = endPoint;
        this.CredentialDataObject.ApiKey = apiKey;
        this.CredentialDataObject.ChatClientDeploymentName = chatClientDeploymentName;
        this.CredentialDataObject.ImageClientDeploymentName = imageClientDeploymentName;
    }


    public async Task OnSaveSettings()
    {
        try
        {

            try
            {
                await SecureStorage.SetAsync(nameof(this.CredentialDataObject.EndPoint), this.CredentialDataObject.EndPoint);
            }
            catch
            {
                await Shell.Current.DisplayAlert(AppResources.SystemStringId_Alert_TitleWarning, string.Format(AppResources.SettingsStringId_ErrorStoreSetting, nameof(this.CredentialDataObject.EndPoint)), AppResources.SystemStringId_Alert_ButtonCancel);
                return;
            }

            try
            {
                await SecureStorage.SetAsync(nameof(this.CredentialDataObject.ApiKey), this.CredentialDataObject.ApiKey);
            }
            catch
            {
                await Shell.Current.DisplayAlert(AppResources.SystemStringId_Alert_TitleWarning, string.Format(AppResources.SettingsStringId_ErrorStoreSetting, nameof(this.CredentialDataObject.ApiKey)), AppResources.SystemStringId_Alert_ButtonCancel);
                return;
            }

            try
            {
                await SecureStorage.SetAsync(nameof(this.CredentialDataObject.ChatClientDeploymentName), this.CredentialDataObject.ChatClientDeploymentName ?? string.Empty);
            }
            catch
            {
                await Shell.Current.DisplayAlert(AppResources.SystemStringId_Alert_TitleWarning, string.Format(AppResources.SettingsStringId_ErrorStoreSetting, nameof(this.CredentialDataObject.ChatClientDeploymentName)), AppResources.SystemStringId_Alert_ButtonCancel);
                return;
            }

            try
            {
                await SecureStorage.SetAsync(nameof(this.CredentialDataObject.ImageClientDeploymentName), this.CredentialDataObject.ImageClientDeploymentName ?? string.Empty);
            }
            catch
            {
                await Shell.Current.DisplayAlert(AppResources.SystemStringId_Alert_TitleWarning, string.Format(AppResources.SettingsStringId_ErrorStoreSetting, nameof(this.CredentialDataObject.ImageClientDeploymentName)), AppResources.SystemStringId_Alert_ButtonCancel);
                return;
            }

            this.openAIService.SetCredential(this.CredentialDataObject);

            var toast = Toast.Make(AppResources.SettingsStringId_SaveCredentialSuccess);

            await toast.Show();
        }
        finally
        {

        }
    }
}