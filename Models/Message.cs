using ChatAI.Enums;
using DevExpress.Maui.Mvvm;

namespace ChatAI.Models;

public class Message : DXObservableObject
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
