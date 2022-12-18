using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System;

namespace LiteMvvm;

/// <summary>
/// Provides a mechanism to resolve ViewModels.
/// </summary>
public static class ViewModelLocator
{
    private static readonly Dictionary<Type, ViewModelConfiguration> _viewModelConfigurations = new();
    private static readonly Dictionary<Type, object> _viewModelCache = new();

    public static readonly BindableProperty ViewModelTypeProperty =
            BindableProperty.Create("ViewModelType",
                typeof(Type),
                typeof(ViewModelLocator),
                default,
                propertyChanged: OnViewModelTypeChanged);

    public static Type GetViewModelType(BindableObject bindable) => (Type)bindable.GetValue(ViewModelTypeProperty);

    public static void SetViewModelType(BindableObject bindable, bool value) => bindable.SetValue(ViewModelTypeProperty, value);

    /// <summary>
    /// Configures a ViewModel.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel type.</typeparam>
    /// <param name="isCached">Boolean value that indicates if the ViewModel should be cached.</param>
    public static void Configure<TViewModel>(bool isCached = false)
    {
        Type viewModelType = typeof(TViewModel);

        if (!_viewModelConfigurations.ContainsKey(viewModelType))
        {
            _viewModelConfigurations.Add(viewModelType, new ViewModelConfiguration
            {
                IsCached = isCached
            });
        }
    }

    /// <summary>
    /// Gets the ViewModel instance based on the given ViewModel type.
    /// </summary>
    /// <typeparam name="TViewModel">ViewModel type.</typeparam>
    /// <returns>ViewModel instance.</returns>
    public static TViewModel Get<TViewModel>()
        where TViewModel : class, IViewModel
    {
        return (TViewModel)Get(typeof(TViewModel));
    }

    /// <summary>
    /// Gets the ViewModel instance based on the given ViewModel type.
    /// </summary>
    /// <param name="viewModelType">ViewModel type.</param>
    /// <returns>ViewModel instance.</returns>
    public static object Get(Type viewModelType)
    {
        if (_viewModelConfigurations.TryGetValue(viewModelType, out ViewModelConfiguration viewModelConfiguration))
        {
            if (viewModelConfiguration.IsCached)
            {
                object viewModel;

                if (_viewModelCache.TryGetValue(viewModelType, out viewModel))
                {
                    return viewModel;
                }
                else
                {
                    viewModel = CreateViewModel(viewModelType);

                    _viewModelCache.TryAdd(viewModelType, viewModel);

                    return viewModel;
                }
            }
        }

        return CreateViewModel(viewModelType);
    }

    /// <summary>
    /// Clears the ViewModel cache.
    /// </summary>
    public static void ClearCache()
    {
        _viewModelCache.Clear();
    }

    private static object CreateViewModel(Type viewModelType) => Activator.CreateInstance(viewModelType);

    private static void OnViewModelTypeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not Element view)
        {
            return;
        }

        if (newValue is Type viewModelType)
        {
            view.BindingContext = Get(viewModelType);
        }
    }

    private readonly struct ViewModelConfiguration
    {
        /// <summary>
        /// Gets a boolean value that indicates if the view model should be cached.
        /// </summary>
        public bool IsCached { get; init; }
    }
}