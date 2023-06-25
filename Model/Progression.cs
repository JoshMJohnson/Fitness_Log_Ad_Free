using SQLite;

namespace WorkoutLog.Model;

[Table("Progressions")]
public class Progression
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Image_Date { get; set; }

    public string Image_Full_Path { get; set; }
}