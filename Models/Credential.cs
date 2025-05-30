using DevExpress.Maui.DataForm;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using ChatAI.Resources;

namespace ChatAI.Models;

public class Credential : INotifyPropertyChanged
{
    private string endPoint;
    [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "SettingsStringId_FieldRequired")]
    [DataFormDisplayOptions(LabelIcon = "name")]
    public string EndPoint
    {
        get { return this.endPoint; }
        set
        {

            this.endPoint = value;
            NotifyPropertyChanged();

        }
    }

    private string apiKey;

    [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = "SettingsStringId_FieldRequired")]
    [DataFormPasswordEditor]
    [DataFormDisplayOptions(LabelIcon = "password")]
    public string ApiKey
    {
        get { return this.apiKey; }
        set
        {

            this.apiKey = value;
            NotifyPropertyChanged();

        }
    }

    private string chatClientDeploymentName;


    [DataFormDisplayOptions(LabelIcon = "name")]
    public string ChatClientDeploymentName
    {
        get { return this.chatClientDeploymentName; }
        set
        {

            this.chatClientDeploymentName = value;
            NotifyPropertyChanged();

        }
    }

    private string imageClientDeploymentName;

    [DataFormDisplayOptions(LabelIcon = "name")]
    public string ImageClientDeploymentName
    {
        get { return this.imageClientDeploymentName; }
        set
        {

            this.imageClientDeploymentName = value;
            NotifyPropertyChanged();

        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}