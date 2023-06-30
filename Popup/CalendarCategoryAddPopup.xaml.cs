using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarCategoryAddPopup
{
    private int max_categories = 6; /* max number of workout categories available to database */
    
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