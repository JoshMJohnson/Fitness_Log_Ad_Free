using Android.OS;
using Microsoft.Maui.Controls.Shapes;
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
    private async void Create_Category(object sender, EventArgs e)
    {
        string name = category_name.Text;

        if (name != null && name.Length != 0) /* if name field is not empty */
        {
            name = name.Trim(); /* removes leading and trailing whitespace */
            await App.RecordRepo.Add_Calendar_Category(name);
            
            error_prompt.IsVisible = false;
            Close();
        }
        else /* else; name field is empty */
        {
            error_prompt.Text = "Category name cannot be empty";
            error_prompt.IsVisible = true;
        }
    }
}