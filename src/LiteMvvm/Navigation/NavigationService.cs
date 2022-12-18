using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace LiteMvvm.Navigation;

/// <summary>
/// Provides a mechanism to navigate across the application.
/// </summary>
internal sealed class NavigationService : INavigationService
{
    private readonly Dictionary<Type, Type> _viewModelPages = new();

    /// <summary>
    /// Gets the current navigation object.
    /// </summary>
    private static INavigation Navigation => Shell.Current.Navigation;

    public void Register<TViewModel, TPage>()
        where TViewModel : class, IViewModel
        where TPage : Page
    {
        Type viewModelType = typeof(TViewModel);
        Type pageType = typeof(TPage);

        if (!_viewModelPages.ContainsKey(viewModelType))
        {
            _viewModelPages.Add(viewModelType, pageType);
        }
    }

    public async Task PushAsync<TViewModel>(TViewModel viewModel = null)
        where TViewModel : class, IViewModel
    {
        Page page = BuildPage(viewModel);

        if (page is not null)
        {
            await Navigation.PushAsync(page);
        }
    }

    public async Task PopAsync()
    {
        Page page = await Navigation.PopAsync();

        if (page is not null)
        {
            page.BindingContext = null;
        }
    }

    public async Task GoToAsync<TViewModel>(TViewModel viewModel = null)
        where TViewModel : class, IViewModel
    {
        Page page = BuildPage(viewModel);

        if (page is not null)
        {
            Navigation.InsertPageBefore(page, Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();
        }
    }

    public async Task ChangeShellStateAsync(params string[] routes)
    {
        string route = $"//";

        if (routes.Any())
        {
            route += $"//{string.Join("//", routes)}";
        }

        await Shell.Current.GoToAsync(route);
    }

    private Page BuildPage<TViewModel>(TViewModel viewModel = null)
        where TViewModel : class, IViewModel
    {
        Page page = null;

        if (_viewModelPages.TryGetValue(typeof(TViewModel), out Type pageType))
        {
            page = Activator.CreateInstance(pageType) as Page;
        }

        if (page is not null)
        {
            page.BindingContext = viewModel is not null ? viewModel : ViewModelLocator.Get<TViewModel>();
        }

        return page;
    }
}
