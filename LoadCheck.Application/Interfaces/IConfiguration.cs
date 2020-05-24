namespace LoadCheck.Application.Interfaces
{
    public interface IConfiguration
    {
        int UrlMeasurements { get; }

        int MaxUrlsCount { get; }

        int MaxSitemapCount { get; }
    }
}