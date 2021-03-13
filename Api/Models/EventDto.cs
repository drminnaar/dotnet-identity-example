using System;

namespace Api.Models
{
    public sealed class EventDto
    {
        public Guid EventId { get; init; } = Guid.Empty;
        public string Name { get; init; } = string.Empty!;
        public string Description { get; init; } = string.Empty;
        public DateTimeOffset Date { get; init; } = DateTimeOffset.Now;
        public string HostedBy { get; init; } = string.Empty;
        public string Venue { get; init; } = string.Empty;
    }
}
