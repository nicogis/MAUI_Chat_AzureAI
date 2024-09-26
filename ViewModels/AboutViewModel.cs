using ChatAI.Resources;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;


namespace ChatAI.ViewModels;

public class AboutViewModel : BaseViewModel
{
    public const string ViewName = "AboutPage";

    public AboutViewModel()
    {

        CommandWebSite = new AsyncRelayCommand(OpenSite);
        CommandInit = new Command(Init);
        TitleApp = AppInfo.Current.Name;
        VersionApp = string.Format(AppResources.AboutStringId_CurrentVersion, AppInfo.Current.VersionString);

    }


    public static Task<Boolean> OpenSite()
    {
        return Browser.OpenAsync(AppResources.AboutStringId_SiteWebCompany, BrowserLaunchMode.External);
    }


    public ICommand CommandInit { get; }

    public IAsyncRelayCommand CommandWebSite { get; }

    string titleApp;
    public string TitleApp
    {
        get => this.titleApp;
        private set => SetProperty(ref this.titleApp, value);
    }

    string versionApp;
    public string VersionApp
    {
        get => this.versionApp;
        private set => SetProperty(ref this.versionApp, value);
    }

    string versionInfoOnline;
    public string VersionInfoOnline
    {
        get => this.versionInfoOnline;
        private set => SetProperty(ref this.versionInfoOnline, value);
    }


    public void Init()
    {
        try
        {
            string about = null;


            string valueYes = System.Net.WebUtility.HtmlEncode(AppResources.SystemStringId_Yes);
            string valueNo = System.Net.WebUtility.HtmlEncode(AppResources.SystemStringId_No);

            about += $"{AppResources.VersionTrackingStringId_IsFirstLaunchEver}: {(VersionTracking.Default.IsFirstLaunchEver ? valueYes : valueNo)}<br>";
            about += $"{AppResources.VersionTrackingStringId_IsFirstLaunchForCurrentVersion}: {(VersionTracking.Default.IsFirstLaunchForCurrentVersion ? valueYes : valueNo)}<br>";
            about += $"{AppResources.VersionTrackingStringId_IsFirstLaunchForCurrentBuild}: {(VersionTracking.Default.IsFirstLaunchForCurrentBuild ? valueYes : valueNo)}<br>";
            about += $"{AppResources.VersionTrackingStringId_CurrentVersion}: {VersionTracking.Default.CurrentVersion}<br>";
            about += $"{AppResources.VersionTrackingStringId_CurrentBuild}: {VersionTracking.Default.CurrentBuild}<br>";
            about += $"{AppResources.VersionTrackingStringId_FirstInstalledVersion}: {VersionTracking.Default.FirstInstalledVersion}<br>";
            about += $"{AppResources.VersionTrackingStringId_FirstInstalledBuild}: {VersionTracking.Default.FirstInstalledBuild}<br>";
            about += $"{AppResources.VersionTrackingStringId_VersionHistory}: {string.Join(',', VersionTracking.Default.VersionHistory)}<br>";
            about += $"{AppResources.VersionTrackingStringId_BuildHistory}: {string.Join(',', VersionTracking.Default.BuildHistory)}<br>";

            about += $"{AppResources.VersionTrackingStringId_PreviousVersion}: {(VersionTracking.Default.PreviousVersion?.ToString() ?? "-")}<br>";
            about += $"{AppResources.VersionTrackingStringId_PreviousBuild}: {(VersionTracking.Default.PreviousBuild?.ToString() ?? "-")}";

            VersionInfoOnline = about;
        }
        catch
        {

        }
    }
}