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

        if (name != null)
        {
            name = name.Trim(); /* removes leading and trailing whitespace */
            error_prompt.IsVisible = false;
            await App.RecordRepo.Add_Calendar_Category(name);
            Close();
        }
        else
        {
            error_prompt.Text = "Category name cannot be empty";
            error_prompt.IsVisible = true;
        }

        
    }
}