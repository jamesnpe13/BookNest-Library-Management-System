﻿using BookNest.Services;
using BookNest.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace BookNest.ViewModels;

public enum PageView
{
    Dashboard,
    Bag,
    Books,
    Watchlist,
    Returns,
    Reserved,
    People,
    Account,
    SignOut,

}

public partial class MainPage_VM : ObservableObject
{
    private readonly AppData ad;
    private readonly SessionService ss;
    private readonly IServiceProvider sp;

    [ObservableProperty]
    private UserControl? currentView;

    [ObservableProperty]
    private string welcomeTextLine1 = string.Empty;

    [ObservableProperty]
    private string welcomeTextLine2 = string.Empty;

    [ObservableProperty]
    private string accountType = string.Empty;

    [ObservableProperty] private Visibility dashboardNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility booksNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility bagNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility watchlistNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility returnsNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility reservedNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility peopleNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility accountNavButtonVisibility = Visibility.Collapsed;
    [ObservableProperty] private Visibility signOutNavButtonVisibility = Visibility.Collapsed;

    public MainPage_VM(AppData _ad, SessionService _ss, IServiceProvider _sp)
    {
        ad = _ad;
        ss = _ss;
        sp = _sp;

        NavbarInit();

        // default view
        SetCurrentView(PageView.Dashboard);

    }

    // view router
    [RelayCommand]
    public void SetCurrentView(PageView targetView)
    {
        if (targetView == PageView.Dashboard)
        {
            CurrentView = ad.CurrentAccount.AccountType == "Administrator" ? sp.GetRequiredService<Admin_Dashboard_V>() : sp.GetRequiredService<Member_Dashboard_V>();
        }
        if (targetView == PageView.Bag) CurrentView = sp.GetRequiredService<Member_Bag_V>();
        if (targetView == PageView.Books) CurrentView = sp.GetRequiredService<Member_Books_V>();
        if (targetView == PageView.Watchlist) CurrentView = sp.GetRequiredService<Member_Watchlist_V>();
        if (targetView == PageView.Account) CurrentView = sp.GetRequiredService<Member_Account_V>();
        //if (targetView == PageView.Reserved) // reserved view
        //if (targetView == PageView.Returns) // returns view 
        //if (targetView == PageView.Account) // sign out view
    }

    // navbar style (member or admin)
    public void NavbarInit()
    {
        // set welcome message
        WelcomeTextLine1 = "Hi, " + ad.CurrentAccount.FirstName ?? "User";
        WelcomeTextLine2 = "Let's get started";

        // set account type display
        AccountType = ad.CurrentAccount.AccountType;

        // show view buttons
        if (ad.CurrentAccount.AccountType == "Member")
        {
            DashboardNavButtonVisibility = Visibility.Visible;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Visible;
            WatchlistNavButtonVisibility = Visibility.Visible;
            ReturnsNavButtonVisibility = Visibility.Collapsed;
            ReservedNavButtonVisibility = Visibility.Collapsed;
            PeopleNavButtonVisibility = Visibility.Collapsed;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
        if (ad.CurrentAccount.AccountType == "Administrator")
        {
            DashboardNavButtonVisibility = Visibility.Visible;
            BooksNavButtonVisibility = Visibility.Visible;
            BagNavButtonVisibility = Visibility.Collapsed;
            WatchlistNavButtonVisibility = Visibility.Collapsed;
            ReturnsNavButtonVisibility = Visibility.Visible;
            ReservedNavButtonVisibility = Visibility.Visible;
            PeopleNavButtonVisibility = Visibility.Visible;
            AccountNavButtonVisibility = Visibility.Visible;
            SignOutNavButtonVisibility = Visibility.Visible;
        }
    }

    public void HandleUserSignOut() => ss.HandleUserSignOut();

}
