using LiteMvvm.Builder;
using Microsoft.Maui.Hosting;
using System;

namespace LiteMvvm;

/// <summary>
/// Provides extensions for the <see cref="MauiAppBuilder"/> class.
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Initialize LiteMvvm for the given <see cref="MauiAppBuilder"/>.
    /// </summary>
    /// <param name="mauiAppBuilder">Current builder.</param>
    /// <param name="builder">LiteMvvm builder.</param>
    /// <returns>Current builder.</returns>
    public static MauiAppBuilder UseLiteMvvm(this MauiAppBuilder mauiAppBuilder, Action<LiteMvvmBuilder> builder = null)
    {
        LiteMvvmBuilder liteMvvmBuilder = new(mauiAppBuilder.Services);

        if (builder is not null)
        {
            builder(liteMvvmBuilder);
        }
        else
        {
            liteMvvmBuilder.ConfigureNavigation();
        }

        return mauiAppBuilder;
    }
}
