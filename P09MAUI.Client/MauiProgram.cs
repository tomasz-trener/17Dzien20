using Microsoft.Extensions.Logging;
using P09MAUI.Client.Tools;
using P09MAUI.Client.ViewModels.ProductViewModel;

namespace P09MAUI.Client
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
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<ProductsApiTool>();
            builder.Services.AddSingleton<ProdcutWindowVM>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}