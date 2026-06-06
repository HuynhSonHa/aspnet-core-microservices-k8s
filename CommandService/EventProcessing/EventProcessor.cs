using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommandService.Dtos.Event;
using CommandService.Dtos.Platform;
using CommandService.Interfaces;
using CommandService.Mappers.Platform;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEventType(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    AddPlatform(message);
                    break;
                default:
                    Console.WriteLine("Could not determine the event type");
                    break;
            }

        }

        private void AddPlatform(string message)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var commandRepo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

                var platformRepo = scope.ServiceProvider.GetRequiredService<IPlatformRepository>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishDto>(message);

                try
                {
                    var platform = platformPublishedDto.ToPlatform();

                    if (!platformRepo.ExternalPlatformExists(platform.ExternalId))
                    {
                        platformRepo.CreatePlatform(platform);
                  
                        Console.WriteLine("Platform added to DB");
                    }
                    else
                    {
                        Console.WriteLine("Platform already exists in DB");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not add Platform to DB {ex.Message}");
                }

            }

           
        }

        private EventType DetermineEventType(string message)
        {
            Console.WriteLine("Determining event type");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);
            // Just deserialize the field "Event" in the dto from the message


            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}