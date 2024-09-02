namespace ChatAI.ViewModels.Templates;
public class MessageTemplateSelector : DataTemplateSelector, IMarkupExtension
{
    public DataTemplate SenderTemplate { get; set; }
    public DataTemplate RecipientTemplate { get; set; }
    public object ProvideValue(IServiceProvider serviceProvider) => this;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item is Message message ? message.Sender.Name == "Me" ? SenderTemplate : RecipientTemplate : null;
    }
}

