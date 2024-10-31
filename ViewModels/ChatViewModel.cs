
using ChatAI.Controls.MarkdownView;
using ChatAI.Enums;
using ChatAI.Models;
using ChatAI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using static Android.Renderscripts.ScriptGroup;



namespace ChatAI.ViewModels;

public partial class ChatViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendMessageCommand),nameof(SendMessageImageCommand))]
    string editMessageText;
    public ChatUser Me { get; set; } = new ChatUser() { Name = "Me" };
    public ChatUser Сompanion { get; set; } = new ChatUser() { Name = "Artificial Intelligence" };
    public ObservableCollection<Message> Messages { get; set; } = [];
    

    [ObservableProperty]
    bool isOpenPopup;

    [ObservableProperty]
    string popupTitle;

    [ObservableProperty]
    bool isBusy;


    private readonly IOpenAIService openAIService;

    public Command CommandInit { get; }
    

    public ChatViewModel(IOpenAIService openAIService)
    {

        this.openAIService = openAIService;
        CommandInit = new Command(Init);

        
    }

    
    async void Init()
    {
        try
        {


            Credential credential = new();

            credential.EndPoint = await SecureStorage.GetAsync(nameof(credential.EndPoint));
            credential.ApiKey = await SecureStorage.GetAsync(nameof(credential.ApiKey));
            credential.ChatClientDeploymentName = await SecureStorage.GetAsync(nameof(credential.ChatClientDeploymentName));
            credential.ImageClientDeploymentName = await SecureStorage.GetAsync(nameof(credential.ImageClientDeploymentName));

            openAIService.SetCredential(credential);
        }
        catch
        {

        }

    }


    async public void SubmitMessage(object message, ServiceType serviceType = ServiceType.Chat)
    {
        if (message is string messageText)
        {
            Messages.Add(new Message() { Text = messageText, Sender = Me});
            EditMessageText = string.Empty;
            
            await Task.Run(async () => 
            {
                try
                {
                    if (serviceType == ServiceType.Chat)
                    {
                        var a = new Message() { Sender = Сompanion, IsLastMessage = true };
                        Messages.Add(a);
                        
                        await foreach (var chunk in openAIService.AskQuestion(messageText))
                        {
                            a.Text += chunk;
                        }

                        
                        
                    }
                    else if (serviceType == ServiceType.Image)
                    {
                        IsBusy = true;
                        var a = new Message() { Sender = Сompanion, IsLastMessage = true, ServiceType = serviceType };
                        Messages.Add(a);

                        a.Image = await openAIService.CreateImage(messageText);
                        IsBusy = false;
                    }

                    

                }
                catch (Exception ex) {
                    Messages.RemoveAt(Messages.Count - 1);  
                    PopupTitle = ex.Message;
                    IsOpenPopup = true;
                    IsBusy = false;
                }
            });

            /*
            if (serviceType == ServiceType.Chat)
            {
                //post process KaTeX
                var m = Messages[Messages.Count - 1];
                string patternInLine = @"\\\((.*?)\\\)"; //in line \( e \)
                string patternBlock =  @"\\\[(.*?)\\\]"; // block \[ e \]
                string k = m.Text;
                bool isChange = false;
                if (Regex.IsMatch(k, patternInLine))
                {
                    k = Regex.Replace(k, patternInLine, "$$$1$");  //in line \( e \) -> $ e $
                    isChange = true;
                }

                if (Regex.IsMatch(k, patternBlock))
                {
                    k = Regex.Replace(k, patternBlock, "$$$$$1$$$");    //block \[ e \] -> $$ e $$
                    isChange = true;
                }

                if (isChange)
                {
                    m.Text = k;
                }
            }
            */
            
        }
        else if (message is Message messageObj)
        {
            messageObj.SentAt = DateTime.Now;
            Messages.Add(messageObj);
        }
    }

    [RelayCommand(CanExecute = nameof(CanSendMessage))]
    void SendMessage(object parameter)
    {
        SubmitMessage(parameter);
    }

    [RelayCommand(CanExecute = nameof(CanSendMessageImage))]
    void SendMessageImage(object parameter)
    {
        SubmitMessage(parameter, ServiceType.Image);
    }

    [RelayCommand()]
    void DismissPopup()
    {
        IsOpenPopup = false;
    }

    bool CanSendMessage(object message)
    {
        if (message is string messageText)
            return !string.IsNullOrEmpty(messageText) && !string.IsNullOrWhiteSpace(messageText) && openAIService.HasChatClient;
        return message != null;
    }

    bool CanSendMessageImage(object message)
    {
        if (message is string messageText)
            return !string.IsNullOrEmpty(messageText) && !string.IsNullOrWhiteSpace(messageText) && openAIService.HasImageClient;
        return message != null;
    }
}

public class Message : BaseViewModel
{
    public Guid Id { get; } = Guid.NewGuid();
    public ChatUser Sender { get; set; }
    public DateTime? SentAt { get; set; } = DateTime.Now;


   

    private string text;

    public string Text
    {
        get => text;
        set => SetProperty(ref text, value);
    }

    public ServiceType ServiceType { get; set; } = ServiceType.Chat;

    public bool IsLastMessage { get; set; }

    private byte[] image;

    public byte[] Image
    {
        get => image;
        set => SetProperty(ref image, value);
    }

}

public class ChatUser
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Initials => string.Concat(Name.AsSpan(0, 1), Name.Split(null)[1].AsSpan(0, 1));
}



