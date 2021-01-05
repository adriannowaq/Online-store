namespace OnlineStore.Infrastructure.Services
{
    public interface IHtmlSanitizationService
    {
        string SanitizeData(string data);
    }
}
