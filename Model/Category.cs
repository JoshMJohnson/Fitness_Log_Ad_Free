using SQLite;

namespace WorkoutLog.Model;

[Table("Categories")]
public class Category
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }

	[Unique]
	public string Name { get; set; }

	public string Color { get; set; }
}