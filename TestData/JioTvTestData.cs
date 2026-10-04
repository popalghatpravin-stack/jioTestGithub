using JioTv.Playwright.Core.Config;
using JioTv.Playwright.Models;
using NUnit.Framework;

namespace JioTv.Playwright.TestData;

public static class JioTvTestData
{
    public static IEnumerable<TestCaseData> HomePageData
    {
        get
        {
            var pageData = new JioTvPageData()
            {
                Url = JioTvConfiguration.BaseUrl,
                ExpectedHost = JioTvConfiguration.ExpectedHost
            };

            yield return new TestCaseData(pageData)
                .SetName("JioTV home page loads")
                .SetProperty("caseid", "JIO-1")
                .SetProperty("title", "[Home] Validate JioTV home page loads");
        }
    }
}
