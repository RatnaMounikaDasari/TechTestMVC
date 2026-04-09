using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TechTestMVC.Models;

namespace TechTestMVC.Services
{
    public class EventScheduler : IEventScheduler
    {
        private List<Event> _events = new List<Event>();
        private readonly string _filePath = "events.json";

        public EventScheduler()
        {
            LoadEvents();
        }

        // Schedule Event
        public bool ScheduleEvent(Event newEvent)
        {
            // Prevent double booking
            if (_events.Any(e => e.DateTime == newEvent.DateTime))
            {
                return false;
            }

            _events.Add(newEvent);
            SaveEvents();
            return true;
        }

        // Cancel Event
        public bool CancelEvent(string name)
        {
            var ev = _events.FirstOrDefault(e => e.Name == name);

            if (ev == null)
                return false;

            _events.Remove(ev);
            SaveEvents();
            return true;
        }

        // Get Upcoming Events
        public List<Event> GetUpcomingEvents()
        {
            return _events
                .Where(e => e.DateTime > DateTime.Now)
                .OrderBy(e => e.DateTime)
                .ToList();
        }

        // Save to file (Persistence)
        private void SaveEvents()
        {
            var json = JsonSerializer.Serialize(_events);
            File.WriteAllText(_filePath, json);
        }

        // Load from file
        private void LoadEvents()
        {
            if (!File.Exists(_filePath))
            {
                _events = new List<Event>();
                return;
            }

            var json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                _events = new List<Event>();
                return;
            }

            try
            {
                _events = JsonSerializer.Deserialize<List<Event>>(json) ?? new List<Event>();
            }
            catch
            {
                _events = new List<Event>(); // fallback if JSON is invalid
            }
        }
    }
}
