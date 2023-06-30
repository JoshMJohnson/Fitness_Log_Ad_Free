using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarCategoryAddPopup
{
	public CalendarCategoryAddPopup()
	{
		InitializeComponent();
	}

    /* closes popup for creating a category */
    private void Cancel_Create_Category(object sender, EventArgs e)
    {
        Close();
    }

    /* submits category creation */
    private void Create_Category(object sender, EventArgs e)
    {

    }
}