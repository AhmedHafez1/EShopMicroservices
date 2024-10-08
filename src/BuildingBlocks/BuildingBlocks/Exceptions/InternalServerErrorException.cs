﻿namespace BuildingBlocks.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string? message) : base(message ?? "Internal server error")
        {
        }
    }
}
