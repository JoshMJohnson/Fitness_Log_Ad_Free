namespace WorkoutLog.Popup;

public partial class CalendarRemovePopup
{
	public CalendarRemovePopup()
	{
        InitializeComponent();

        /* set and display default values */
        exercise_category.SelectedIndex = 0;
        record_date.Date = DateTime.Now;
    }

    /* processes a submission to record an exercise performed */
    private void Remove_Record(object sender, EventArgs e)
    {
        
    }

    /* closes popup for adding an exercise */
    private void Cancel_Record(object sender, EventArgs e)
    {
        Close();
    }
}
