namespace WorkoutLog.Popup;

public partial class PersonalRecordAddPopup
{
	public PersonalRecordAddPopup()
	{
		InitializeComponent();
    }

    /* creates an exercise along with the PR */
    private void Submit_PR(object sender, EventArgs e)
    {

    }

    /* closes popup for creating an exercise */
    private void Cancel_PR(object sender, EventArgs e)
    {
        Close();
    }
}
