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
                Url = "https://www.jiotv.com/",
                ExpectedHost = "jiotv.com"
            };

            yield return new TestCaseData(pageData)
                .SetName("JioTV home page loads")
                .SetProperty("caseid", "JIO-1")
                .SetProperty("title", "[Home] Validate JioTV home page loads");
        }
    }
}
