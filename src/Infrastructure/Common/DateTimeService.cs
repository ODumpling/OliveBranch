﻿using Application.Common.Interfaces;

namespace Infrastructure.Common;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}