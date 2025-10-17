using SQLite;
using System;

namespace MHikePrototype.Models
{
    public class Hike
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string DateIso { get; set; } = string.Empty;
        public string Parking { get; set; } = string.Empty;
        public double Length { get; set; } = 0;
        public string Difficulty { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Terrain { get; set; } = string.Empty;
        public string EstimatedTime { get; set; } = string.Empty;
        public string CreatedAtIso { get; set; } = DateTime.UtcNow.ToString("s");

        // Parameterless constructor for SQLite
        public Hike() { }
    }
}
