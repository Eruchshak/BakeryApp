using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using BakeryApp.Services;
using System;
using System.Collections.Generic;

namespace BakeryApp.Views;

public static class UiSupport
{
    private const string AssetsRoot = "avares://BakeryApp/Assets/";
    private static readonly Dictionary<string, Bitmap> cache = new();

    public static IBrush Brush(string key) => (IBrush)Application.Current!.Resources[key]!;

    public static Bitmap Picture(string? fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName)) fileName = "picture.png";

        string uri = AssetsRoot + "Images/" + fileName;
        if (!AssetLoader.Exists(new Uri(uri))) uri = AssetsRoot + "picture.png";

        if (!cache.TryGetValue(uri, out Bitmap? bitmap))
        {
            using var stream = AssetLoader.Open(new Uri(uri));
            bitmap = new Bitmap(stream);
            cache[uri] = bitmap;
        }
        return bitmap;
    }

    public static string ErrorText(Exception error)
    {
        if (error is AppException) return error.Message;
        return "Операция не выполнена: " + error.Message;
    }
}