using LLama.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LLama
{

    public class ChatSession<T>
        where T : IChatModel
    {

        protected readonly IChatModel _model;

        public List<ChatMessageRecord> History { get; } = new List<ChatMessageRecord>();

        public ChatSession(T model)
        {
            this._model = model;
        }

        public virtual IEnumerable<string> Chat(string text, string? prompt = null, string encoding = "UTF-8")
        {
            History.Add(new ChatMessageRecord(new ChatCompletionMessage(ChatRole.Human, text), DateTime.Now));
            string totalResponse = "";
            foreach (var response in _model.Chat(text, prompt, encoding))
            {
                totalResponse += response;
                yield return response;
            }
            History.Add(new ChatMessageRecord(new ChatCompletionMessage(ChatRole.Assistant, totalResponse), DateTime.Now));
        }

        public ChatSession<T> WithPrompt(string prompt, string encoding = "UTF-8")
        {
            _model.InitChatPrompt(prompt, encoding);
            return this;
        }

        public ChatSession<T> WithPromptFile(string promptFilename, string encoding = "UTF-8")
        {
            if (!string.IsNullOrEmpty(promptFilename))
            {
                return WithPrompt(File.ReadAllText(promptFilename), encoding);
            }

            return this;
        }

        /// <summary>
        /// Set the keyword to split the return value of chat AI.
        /// </summary>
        /// <param name="humanName"></param>
        /// <returns></returns>
        public ChatSession<T> WithAntiprompt(IEnumerable<string> antiprompt)
        {
            _model.InitChatAntiprompt(antiprompt.ToArray());
            return this;
        }

    }

}
