using System.Collections.Specialized;
using ChatAI.Models;
using ChatAI.Resources;
using ChatAI.ViewModels;

namespace ChatAI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChatPage : ContentPage
{
    private readonly ChatViewModel vm;
    public ChatPage()
    {
        InitializeComponent();
        vm = ((ChatViewModel)BindingContext);
        //BindingContext = vm;
        vm.Messages.CollectionChanged += OnMessagesCollectionChanged;
    }
    void OnMessagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        chatSurface.ScrollTo(chatSurface.GetItemHandle(vm.Messages.Count - 1));
    }
    private void MessagesCollectionSizeChanged(object sender, EventArgs e)
    {
        chatSurface.ScrollTo(chatSurface.GetItemHandleByVisibleIndex(chatSurface.VisibleItemCount - 1));
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        
        Message o = e.Parameter as Message;

        byte[] image = o?.Image;

        if (image != null) 
        {
            string file = Path.Combine(FileSystem.Current.CacheDirectory, $"{Guid.NewGuid()}.png");
            File.WriteAllBytes(file, image);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = AppResources.ChatStringId_ShareImage,
                File = new ShareFile(file)
            });
        }
    }
}

