namespace WorkoutLog.Model;

public class CalendarDay : CalendarDatePropertyChanged
{
	public DateTime date {  get; set; }

	private bool _isCurrentDate;
	public bool is_current_date
	{
		get => _isCurrentDate;
		set => SetProperty(ref _isCurrentDate, value);
	}

	public bool has_day_entry { get; set; }
}