using ImageGeneratorApi.Core.Application.Interfaces;

namespace ImageGeneratorApi.Core.Application.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;

}