using Microsoft.Playwright;
using JioTv.Playwright.Core.Config;

namespace JioTv.Playwright.Pages;

public sealed class JioTvHomePage
{
    private readonly IPage _page;
    private ILocator PageBody => _page.Locator("body");

    public JioTvHomePage(IPage page)
    {
        _page = page;
    }

    public async Task OpenAsync(string url)
    {
        await _page.GotoAsync(url, new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = JioTvConfiguration.TimeoutMilliseconds
        });
    }

    public async Task GoBackAsync()
    {
        await _page.GoBackAsync(new PageGoBackOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = JioTvConfiguration.TimeoutMilliseconds
        });
    }

    public async Task GoForwardAsync()
    {
        await _page.GoForwardAsync(new PageGoForwardOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = JioTvConfiguration.TimeoutMilliseconds
        });
    }

    public Task<string> GetTitleAsync() => _page.TitleAsync();

    public Task<bool> IsPageDisplayedAsync() => PageBody.IsVisibleAsync();

    public string CurrentUrl => _page.Url;
}
