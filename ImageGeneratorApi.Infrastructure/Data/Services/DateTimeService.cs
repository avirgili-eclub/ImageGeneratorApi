using ImageGeneratorApi.Infrastructure.Data.Interfaces;

namespace ImageGeneratorApi.Infrastructure.Data.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;

}