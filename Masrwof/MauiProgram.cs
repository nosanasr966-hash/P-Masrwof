using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Masrwof.Data;
using Masrwof.Interfaces;
using Masrwof.Services;
using System.IO;
using Microsoft.Maui.Storage;

namespace Masrwof;

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
            });

        builder.Services.AddMauiBlazorWebView();

      
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "masrwof.db3");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"), ServiceLifetime.Scoped);

       
        builder.Services.AddScoped<IExpenseService, ExpenseService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

    
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        return app;
    }
}