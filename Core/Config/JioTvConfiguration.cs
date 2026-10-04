using System.Text.Json;

namespace JioTv.Playwright.Core.Config;

public static class JioTvConfiguration
{
    private static readonly Lazy<Uri> BaseUri = new(LoadBaseUri);

    public static string BaseUrl => BaseUri.Value.ToString();

    public static string ExpectedHost => BaseUri.Value.Host;

    private static Uri LoadBaseUri()
    {
        var configurationPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (!File.Exists(configurationPath))
        {
            throw new FileNotFoundException("JioTV configuration file was not found.", configurationPath);
        }

        using var stream = File.OpenRead(configurationPath);
        using var document = JsonDocument.Parse(stream);
        var baseUrl = document.RootElement
            .GetProperty("JioTv")
            .GetProperty("BaseUrl")
            .GetString();

        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            throw new InvalidOperationException("JioTv:BaseUrl must be an absolute HTTP or HTTPS URL.");
        }

        return uri;
    }
}