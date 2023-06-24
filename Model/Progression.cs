using SQLite;

namespace WorkoutLog.Model;

[Table("Progressions")]
public class Progression
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Image_Sting { get; set; }
}