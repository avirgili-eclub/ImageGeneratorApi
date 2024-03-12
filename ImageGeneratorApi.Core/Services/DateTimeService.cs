using ImageGeneratorApi.Core.Interfaces;

namespace ImageGeneratorApi.Core.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;

}