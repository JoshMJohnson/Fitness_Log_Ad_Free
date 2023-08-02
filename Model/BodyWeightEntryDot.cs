namespace WorkoutLog.Model;

public class BodyWeightEntryDot : BodyWeightEntryPropertyChanged
{
    public DateTime date { get; set; }
    public int weight { get; set; }

    private bool _isCurrentDateSelected;
    public bool is_current_date_selected
    {
        get => _isCurrentDateSelected;
        set => SetProperty(ref _isCurrentDateSelected, value);
    }
}