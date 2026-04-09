using System.Collections.Generic;
using TechTestMVC.Models;

namespace TechTestMVC.Services
{
    public interface IEventScheduler
    {
        bool ScheduleEvent(Event newEvent);
        bool CancelEvent(string name);
        List<Event> GetUpcomingEvents();
    }
}
