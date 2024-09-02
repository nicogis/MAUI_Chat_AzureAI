using Azure.AI.OpenAI;
using ChatAI.Models;
using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;


namespace ChatAI.Services;

public class OpenAIService : IOpenAIService
{

    private AzureOpenAIClient client;
    private ChatClient chatClient;
    private ImageClient imageClient;


    public void SetCredential(Credential credential)
    {
        bool noSetAI = string.IsNullOrWhiteSpace(credential.ApiKey) || string.IsNullOrWhiteSpace(credential.EndPoint);
        chatClient = null;
        imageClient = null;

        if (noSetAI)
        {
            return;
        }

        ApiKeyCredential apiKeyCredential = new(credential.ApiKey);
        client = new AzureOpenAIClient(new Uri(credential.EndPoint), apiKeyCredential);


        if (!string.IsNullOrWhiteSpace(credential.ChatClientDeploymentName))
        {
            chatClient = client.GetChatClient(credential.ChatClientDeploymentName);
        }

        if (!string.IsNullOrWhiteSpace(credential.ImageClientDeploymentName))
        {
            imageClient = client.GetImageClient(credential.ImageClientDeploymentName);
        }
    }

    public bool HasImageClient
    { get { return imageClient != null; } }

    public bool HasChatClient
    { get { return chatClient != null; } }

    public async IAsyncEnumerable<string> AskQuestion(string query)
    {

        AsyncCollectionResult<StreamingChatCompletionUpdate> updates = chatClient.CompleteChatStreamingAsync(query);


        await foreach (StreamingChatCompletionUpdate update in updates)
        {
            foreach (ChatMessageContentPart updatePart in update.ContentUpdate)
            {
                yield return updatePart.Text;
            }
        }
    }

    public async Task<byte[]> CreateImage(string query)
    {
        ImageGenerationOptions options = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W1024xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Bytes
        };

        GeneratedImage image = await imageClient.GenerateImageAsync(query, options);
        return image.ImageBytes.ToArray();

    }
}
