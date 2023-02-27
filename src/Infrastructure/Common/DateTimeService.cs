using OliveBranch.Application.Common.Interfaces;

namespace OliveBranch.Infrastructure.Common;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}