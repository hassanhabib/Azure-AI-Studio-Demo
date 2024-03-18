using Azure.AI.OpenAI;
using Azure;
using System.Runtime.CompilerServices;

namespace Demo_YT_AI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string myHumanMessage = "Summarize Brokers for me.";

            string apiBase = "ADD_YOUR_API_BASE_HERE";
            string apiKey = "ADD_YOUR_API_KEY_HERE";
            string deploymentId = "gpt-35-turbo";

            var client = new OpenAIClient(
                new Uri(apiBase),
                new AzureKeyCredential(apiKey));

            var chatCompletionOptions = new ChatCompletionsOptions
            {
                Messages =
                {
                    new ChatMessage(ChatRole.Assistant, "You are an assistant that understand The Standard."),
                    new ChatMessage(ChatRole.User, myHumanMessage)
                },

                AzureExtensionsOptions = new AzureChatExtensionsOptions
                {
                    Extensions =
                    {
                        new AzureCognitiveSearchChatExtensionConfiguration
                        {
                            SearchEndpoint = new Uri("ADD_YOUR_AZURE_AI_SEARCH_URL_HERE"),
                            IndexName = "ADD_YOUR_INDEX_HERE",
                            SearchKey = new AzureKeyCredential("ADD_YOUR_SEARCH_KEY_HERE")
                        }
                    }
                }
            };

            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(
                deploymentId,
                chatCompletionOptions);

            Console.Write(response.Value.Choices[0].Message.Content);
        }
    }
}
