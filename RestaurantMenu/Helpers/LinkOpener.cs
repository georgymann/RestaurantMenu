using System;
using Avalonia.Controls;
using Avalonia.Input;

namespace RestaurantMenu;

public static class LinkOpener
{
    public static void Open(object? sender)
    {
        if (sender is not Control { DataContext: InfoField field } control || !field.IsLink)
        {
            return;
        }

        string url = field.Url.Trim();
        if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            url = "https://" + url;
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
        {
            return;
        }

        TopLevel? topLevel = TopLevel.GetTopLevel(control);
        _ = topLevel?.Launcher.LaunchUriAsync(uri);
    }

    public static void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Open(sender);
    }
}
