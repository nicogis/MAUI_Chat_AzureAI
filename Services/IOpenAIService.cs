using ChatAI.Models;

namespace ChatAI.Services;

public interface IOpenAIService
{
    public void SetCredential(Credential credential);

    public bool HasImageClient { get; }

    public bool HasChatClient { get; }

    public IAsyncEnumerable<string> AskQuestion(string query);

    public Task<byte[]> CreateImage(string query);
}
