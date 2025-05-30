namespace ChatAI.Models;

public class ChatUser
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Initials => string.Concat(Name.AsSpan(0, 1), Name.Split(null)[1].AsSpan(0, 1));
}
