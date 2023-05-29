using LLama.WebAPI.Models;
using Microsoft.Extensions.Options;

namespace LLama.WebAPI.Services
{

    public class ChatService
        : IChatService
    {

        private readonly ChatSession<LLamaModel> _session;
        private readonly ChatServiceOptions _options;
        private readonly LLamaModel _model;

        public ChatService(IOptions<ChatServiceOptions> options)
        {
            this._options = options.Value;
            this._model = new LLamaModel(this._options.ModelParams);

            this._session = new ChatSession<LLamaModel>(this._model)
                .WithPromptFile(this._options.PromptFilePath)
                .WithAntiprompt(this._options.Antiprompts);
        }

        public string Send(SendMessageInput input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input.Text);

            Console.ForegroundColor = ConsoleColor.White;
            var outputs = _session.Chat(input.Text);
            var result = "";
            foreach (var output in outputs)
            {
                Console.Write(output);
                result += output;
            }

            return result;
        }

    }

}