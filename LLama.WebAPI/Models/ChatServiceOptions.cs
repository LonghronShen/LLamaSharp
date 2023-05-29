namespace LLama.WebAPI.Models
{

    public class ChatServiceOptions
    {

        public LLamaParams ModelParams { get; set; }

        public string PromptFilePath { get; set; }

        public IEnumerable<string> Antiprompts { get; set; }

        public ChatServiceOptions()
        {
            this.ModelParams = new LLamaParams(
                model: @"ggml-model-q4_0.bin",
                n_ctx: 512,
                interactive: true,
                repeat_penalty: 1.0f,
                verbose_prompt: false
            );
            this.PromptFilePath = "Assets/chat-with-bob.txt";
            this.Antiprompts = new[] { "User:" };
        }

    }

}
