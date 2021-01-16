using AngleSharp.Io;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;

namespace XUnitTestOnlineStore.Extensions
{
    public static class MockCookieCollection
    {
        public static IRequestCookieCollection RequestCookieCollection(string key, string value)
        {
            var requestFeature = new HttpRequestFeature();
            var featureCollection = new FeatureCollection();

            requestFeature.Headers = new HeaderDictionary
            {
                { HeaderNames.Cookie, new StringValues(key + "=" + value) }
            };

            featureCollection.Set<IHttpRequestFeature>(requestFeature);

            var cookiesFeature = new RequestCookiesFeature(featureCollection);

            return cookiesFeature.Cookies;
        }
    }
}
