using System;

namespace EchoNote.Models
{
    public class Transcript
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]   // <- явно указываем SQLite.*
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;   // убираем предупреждение CS8618
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public double DurationSeconds { get; set; }
        public string AudioPath { get; set; } = string.Empty; // убираем CS8618
    }
}
