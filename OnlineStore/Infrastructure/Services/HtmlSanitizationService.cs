using Ganss.XSS;

namespace OnlineStore.Infrastructure.Services
{
    public class HtmlSanitizationService : IHtmlSanitizationService
    {
        private readonly HtmlSanitizer htmlSanitizer;

        public HtmlSanitizationService()
        {
            this.htmlSanitizer = new HtmlSanitizer();
        }

        public string SanitizeData(string data)
        {
            return htmlSanitizer.Sanitize(data);
        }
    }
}
