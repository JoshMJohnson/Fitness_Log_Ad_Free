namespace WorkoutLog.Model;

public class BodyWeightEntryDot : BodyWeightEntryPropertyChanged
{
    public DateTime date { get; set; }
    public int weight { get; set; }
    public int y_adjustment { get; set; }
    public int highest_value { get; set; }
    public int lowest_value { get; set; }

    private bool _isCurrentDateSelected;
    public bool is_current_date_selected
    {
        get => _isCurrentDateSelected;
        set => SetProperty(ref _isCurrentDateSelected, value);
    }
}