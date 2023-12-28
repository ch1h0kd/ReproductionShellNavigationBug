using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using test.Pages;
using test.ViewModels;
using test.Data;
namespace test
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DatabaseContext>();

            //UI registration
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddSingleton<NewClientPage>();

            //viewModel registration
            builder.Services.AddTransient<ClientsViewModel>();
            return builder.Build();
        }
    }
}
