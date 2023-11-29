﻿using Domain.Models;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}
