using SQLite;

namespace MHikePrototype.Models
{
    public class Observation
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int HikeId { get; set; }
        public string ObservationText { get; set; } = string.Empty;
        public string ObservedAtIso { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;

        // Parameterless constructor for SQLite
        public Observation() { }
    }
}
