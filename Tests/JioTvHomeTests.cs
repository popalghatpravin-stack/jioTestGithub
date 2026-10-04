using Allure.NUnit;
using Allure.NUnit.Attributes;
using FluentAssertions;
using JioTv.Playwright.Core.Config;
using JioTv.Playwright.Models;
using JioTv.Playwright.Pages;
using JioTv.Playwright.TestData;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Diagnostics;

namespace JioTv.Playwright.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Self)]
public sealed class JioTvHomeTests : PageTest
{
    [Test]
    [TestCaseSource(typeof(JioTvTestData), nameof(JioTvTestData.HomePageData))]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/6")]
    public async Task HomePageLoadsAsync(JioTvPageData pageData)
    {
        var homePage = new JioTvHomePage(Page);

        await homePage.OpenAsync(pageData.Url);

        homePage.CurrentUrl.Should().Contain(pageData.ExpectedHost);
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the JioTV page body should be visible");
        (await homePage.GetTitleAsync()).Should().NotBeNullOrWhiteSpace("the JioTV page should have a title");
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/12")]
    public async Task DirectUrlNavigationOpensHomePageAsync()
    {
        var homePage = new JioTvHomePage(Page);
        // Navigate directly to the JioTV home page URL.
        await homePage.OpenAsync(JioTvConfiguration.BaseUrl);
        homePage.CurrentUrl.Should().Contain(JioTvConfiguration.ExpectedHost);
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the JioTV page body should be visible");
        (await homePage.GetTitleAsync()).Should().NotBeNullOrWhiteSpace("the JioTV page should have a title");
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/7")]
    public async Task HomePageHasNonEmptyTitleAsync()
    {
        var homePage = new JioTvHomePage(Page);

        await homePage.OpenAsync(JioTvConfiguration.BaseUrl);

        (await homePage.GetTitleAsync()).Should().NotBeNullOrWhiteSpace("the JioTV page should have a title");
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/8")]
    public async Task HomePageRemainsAvailableAfterRefreshAsync()
    {
        var homePage = new JioTvHomePage(Page);

        await homePage.OpenAsync(JioTvConfiguration.BaseUrl);
        await Page.ReloadAsync(new PageReloadOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = 60000
        });

        homePage.CurrentUrl.Should().Contain(JioTvConfiguration.ExpectedHost);
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the refreshed JioTV page body should be visible");
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/9")]
    public async Task HomePageUsesHttpsAsync()
    {
        var homePage = new JioTvHomePage(Page);

        await homePage.OpenAsync(JioTvConfiguration.BaseUrl);

        Uri.TryCreate(homePage.CurrentUrl, UriKind.Absolute, out var currentUri).Should().BeTrue();
        currentUri!.Scheme.Should().Be(Uri.UriSchemeHttps);
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/10")]
    public async Task HomePageLoadsWithinSixtySecondsAsync()
    {
        var homePage = new JioTvHomePage(Page);
        var stopwatch = Stopwatch.StartNew();

        await homePage.OpenAsync(JioTvConfiguration.BaseUrl);

        stopwatch.Stop();
        stopwatch.Elapsed.Should().BeLessThanOrEqualTo(TimeSpan.FromSeconds(60));
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the JioTV page body should be visible");
    }

    [Test]
    [AllureLink("https://dev.azure.com/popalghatpravin/demo/_workitems/edit/11")]
    public async Task BrowserBackAndForwardNavigationAsync()
    {
        var homePage = new JioTvHomePage(Page);
        var firstUrl = JioTvConfiguration.BaseUrl;
        var secondUrl = $"{JioTvConfiguration.BaseUrl}?navigation-test=second";

        await homePage.OpenAsync(firstUrl);
        await homePage.OpenAsync(secondUrl);

        homePage.CurrentUrl.Should().Contain("navigation-test=second");
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the second JioTV page should be visible");

        await homePage.GoBackAsync();

        homePage.CurrentUrl.Should().Be(firstUrl);
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the previous JioTV page should be visible");

        await homePage.GoForwardAsync();

        homePage.CurrentUrl.Should().Contain("navigation-test=second");
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the second JioTV page should be visible again");
    }
}
