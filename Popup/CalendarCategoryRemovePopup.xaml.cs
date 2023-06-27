namespace WorkoutLog.Popup;

public partial class CalendarCategoryRemovePopup
{
	public CalendarCategoryRemovePopup()
	{
		InitializeComponent();
	}

    /* closes popup for removing a workout calendar category */
    private void Cancel_Remove_Category(object sender, EventArgs e)
    {
        Close();
    }

    /* submits the removal of a workout calendar category */
    private void Remove_Category(object sender, EventArgs e)
    {

    }
}