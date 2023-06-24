namespace WorkoutLog.Popup;

public partial class GoalBodyWeightPopup
{
	public GoalBodyWeightPopup()
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
