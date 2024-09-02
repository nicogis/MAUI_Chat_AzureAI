﻿using ChatAI.ViewModels;
using ChatAI.Views;
using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.Core;

namespace ChatAI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        ThemeManager.ApplyThemeToSystemBars = true;
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseDevExpress(useLocalization: true)
            .UseDevExpressControls()
            .UseDevExpressCollectionView()
            .UseDevExpressEditors()
            .UseDevExpressHtmlEditor()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("roboto-regular.ttf", "Roboto");
                fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
            })
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews()
            .ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });







        return builder.Build();
    }

    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<Services.IOpenAIService, Services.OpenAIService>();
        
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<ChatViewModel>();
        mauiAppBuilder.Services.AddSingleton<AboutViewModel>();
        mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ChatPage>();
        mauiAppBuilder.Services.AddSingleton<AboutPage>();
        mauiAppBuilder.Services.AddSingleton<SettingsPage>();
        return mauiAppBuilder;
    }

}