using LLama.WebAPI.Models;
using LLama.WebAPI.Services;

namespace Microsoft.Extensions.DependencyInjection
{

    public static class LLamaExtensions
    {

        public static void AddLLamaServices(this IServiceCollection services, Action<ChatServiceOptions> setupAction = null)
        {
            services.AddMvc()
                .AddApplicationPart(typeof(LLamaExtensions).Assembly)
                .AddControllersAsServices();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddSingleton<ChatService>();
        }

    }

}