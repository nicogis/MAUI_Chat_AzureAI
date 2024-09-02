using System.Collections.Specialized;
using ChatAI.ViewModels;

namespace ChatAI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ChatPage : ContentPage
{
    private readonly ChatViewModel vm;
    public ChatPage(ChatViewModel chatViewModel)
    {
        InitializeComponent();
        vm = chatViewModel;
        BindingContext = vm;
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
}

