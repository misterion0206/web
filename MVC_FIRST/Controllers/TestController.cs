using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;

public class TestController : Controller
{
    private readonly OpenAIClient _client;

    public TestController()
    {
        string endpoint = "https://yochen-test1.openai.azure.com/";
        string key = "f36a01a2964e42778eccd84203c9acdf";
        _client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<string> Index([FromBody] PromptRequest request)
    {
        var options = new ChatCompletionsOptions
        {
            Messages =
            {
                new ChatMessage(ChatRole.System, @"你現在是一個有20年經驗的軟體系統分析師，請遵守以下規則。
                                                  1.僅回答軟體系統有關的問題。
                                                  2.使用繁體中文回答，若對方使用其他語言使用他問的語言回答。"),
                new ChatMessage(ChatRole.User, request.Prompt),
            },
            Temperature = (float)0.7,
            MaxTokens = 4000,
            NucleusSamplingFactor = (float)0.95,
            FrequencyPenalty = 0,
            PresencePenalty = 0,
        };

        var response = await _client.GetChatCompletionsAsync("yochen-gpt-35-test", options);
        var responseMsg = response.Value.Choices.First().Message.Content;

        return responseMsg;
    }

    public class PromptRequest
    {
        public string Prompt { get; set; }
    }
}
