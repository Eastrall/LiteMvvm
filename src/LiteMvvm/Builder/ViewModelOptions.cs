namespace LiteMvvm.Builder;

public sealed class ViewModelOptions
{
    internal ViewModelOptions()
    {
    }

    public void Configure<TViewModel>(bool isCached)
    {
        ViewModelLocator.Configure<TViewModel>(isCached);
    }
}