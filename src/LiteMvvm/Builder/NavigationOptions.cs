using LiteMvvm.Navigation;
using Microsoft.Maui.Controls;

namespace LiteMvvm.Builder;

public sealed class NavigationOptions
{
    private readonly INavigationService _navigationService;

    internal NavigationOptions(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void Register<TViewModel, TPage>()
        where TViewModel : class, IViewModel
        where TPage : Page
    {
        _navigationService.Register<TViewModel, TPage>();
    }
}
