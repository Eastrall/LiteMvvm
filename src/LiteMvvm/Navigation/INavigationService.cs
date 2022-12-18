using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace LiteMvvm.Navigation;

/// <summary>
/// Provides a mechanism to navigate across the application.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Registers the given ViewModel and Page pair.
    /// </summary>
    /// <typeparam name="TViewModel">View model type</typeparam>
    /// <typeparam name="TPage">Page type</typeparam>
    void Register<TViewModel, TPage>()
       where TViewModel : class, IViewModel
       where TPage : Page;

    /// <summary>
    /// Navigates to the page associated with the given ViewModel type.
    /// If a ViewModel instance is provided, it will automatically be bound to the page's BindingContext; 
    /// otherwise a new view model instance is createde.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel type.</typeparam>
    /// <param name="viewModel">ViewModel instance.</param>
    /// <returns></returns>
    Task PushAsync<TViewModel>(TViewModel viewModel = null)
        where TViewModel : class, IViewModel;

    /// <summary>
    /// Pops the current page.
    /// </summary>
    /// <returns></returns>
    Task PopAsync();

    /// <summary>
    /// Navigates to the page associated with the given ViewModel type and clear the page history.
    /// If a ViewModel instance is provided, it will automatically be bound to the page's BindingContext; 
    /// otherwise a new view model instance is createde.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel type.</typeparam>
    /// <param name="viewModel">ViewModel instance.</param>
    /// <returns></returns>
    Task GoToAsync<TViewModel>(TViewModel viewModel = null)
        where TViewModel : class, IViewModel;

    /// <summary>
    /// Changes the application shell state.
    /// </summary>
    /// <param name="routes">Application shell route to navigate to.</param>
    /// <returns></returns>
    Task ChangeShellStateAsync(params string[] routes);
}
