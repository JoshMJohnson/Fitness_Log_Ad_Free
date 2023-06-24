namespace WorkoutLog.Popup;

public partial class GoalPRPopup
{
	public GoalPRPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private void Submit_Goal(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_Goal(object sender, EventArgs e)
    {
        Close();
    }
}
