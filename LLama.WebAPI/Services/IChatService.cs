using LLama.WebAPI.Models;

namespace LLama.WebAPI.Services
{

    public interface IChatService
    {

        string Send(SendMessageInput input);

    }

}
