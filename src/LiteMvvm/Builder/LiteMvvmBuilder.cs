using LiteMvvm.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LiteMvvm.Builder;

public sealed class LiteMvvmBuilder
{
    private readonly IServiceCollection _services;

    public LiteMvvmBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public void ConfigureNavigation(Action<NavigationOptions> options = null)
    {
        _services.AddSingleton<INavigationService, NavigationService>(_ =>
        {
            NavigationService navigation = new();
            NavigationOptions navigationOptions = new(navigation);

            options?.Invoke(navigationOptions);

            return navigation;
        });
    }

    public void ConfigureViewModels(Action<ViewModelOptions> options = null)
    {
        ViewModelOptions viewModelOptions = new();

        options?.Invoke(viewModelOptions);
    }
}
