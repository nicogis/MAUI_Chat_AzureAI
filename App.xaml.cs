﻿using ChatAI.Services;
using ChatAI.Views;
using Application = Microsoft.Maui.Controls.Application;

namespace ChatAI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<NavigationService>();

            //Routing.RegisterRoute(typeof(ItemDetailPage).FullName, typeof(ItemDetailPage));
            //Routing.RegisterRoute(typeof(NewItemPage).FullName, typeof(NewItemPage));
            MainPage = new MainPage();
            // Use the NavigateToAsync<ViewModelName> method
            // to display the corresponding view.
            // Code lines below show how to navigate to a specific page.
            // Comment out all but one of these lines
            // to open the corresponding page.
            //var navigationService = DependencyService.Get<INavigationService>();
            //navigationService.NavigateToAsync<LoginViewModel>(true);
        }
    }
}
