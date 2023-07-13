using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarCategoryRemovePopup
{
	public CalendarCategoryRemovePopup()
	{
		InitializeComponent();
        Retrieve_Categories();        
    }

    /* closes popup for removing a workout calendar category */
    private void Cancel_Remove_Category(object sender, EventArgs e)
    {
        Close();
    }

    /* submits the removal of a workout calendar category */
    private async void Remove_Category(object sender, EventArgs e)
    {
        string name = exercise_category.SelectedItem.ToString();
        await App.RecordRepo.Remove_Calendar_Category(name);
        Close();
    }

    /* retrieves list of categories from database and displays them within the picker */
    private async void Retrieve_Categories()
    {
        List<Category> category_list = await App.RecordRepo.Get_Calendar_Category_List();
        List<string> category_string_list = new List<string>();

        /* gets list of category names */
        for (int i = 0; i < category_list.Count; i++)
        {
            category_string_list.Add(category_list[i].name);
        }

        exercise_category.ItemsSource = category_string_list;

        if (category_list.Count == 0) /* if not categories in database */
        {
            exercise_category_empty_display.IsVisible = true;
            exercise_category.IsVisible = false;
        }
        else /* else categories found in database; not empty */
        {
            exercise_category_empty_display.IsVisible = false;
            exercise_category.IsVisible = true;

            exercise_category.SelectedIndex = 0;
        }
    }
}