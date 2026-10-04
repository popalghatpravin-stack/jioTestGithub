using Allure.NUnit;
using FluentAssertions;
using JioTv.Playwright.Models;
using JioTv.Playwright.Pages;
using JioTv.Playwright.TestData;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace JioTv.Playwright.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Self)]
public sealed class JioTvHomeTests : PageTest
{
    [Test]
    [TestCaseSource(typeof(JioTvTestData), nameof(JioTvTestData.HomePageData))]
    public async Task HomePageLoadsAsync(JioTvPageData pageData)
    {
        var homePage = new JioTvHomePage(Page);

        await homePage.OpenAsync(pageData.Url);

        homePage.CurrentUrl.Should().Contain(pageData.ExpectedHost);
        (await homePage.IsPageDisplayedAsync()).Should().BeTrue("the JioTV page body should be visible");
        (await homePage.GetTitleAsync()).Should().NotBeNullOrWhiteSpace("the JioTV page should have a title");
    }
}
