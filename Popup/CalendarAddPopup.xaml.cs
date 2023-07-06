using WorkoutLog.Model;

namespace WorkoutLog.Popup;

public partial class CalendarAddPopup
{
	public CalendarAddPopup() 
    {
        InitializeComponent();
        Get_Exercise_Categories();

        /* set and display default values */
        exercise_category.SelectedIndex = 0;
        record_date.Date = DateTime.Now;
    }

    /* processes a submission to record an exercise performed */
    private async void Submit_Record(object sender, EventArgs e)
    {
        string category_name = exercise_category.SelectedItem.ToString();
        DateTime date = record_date.Date;
        
        await App.RecordRepo.Add_Calendar_Entry(category_name, date);
        Close();
    }

    /* closes popup for adding an exercise */
    private void Cancel_Record(object sender, EventArgs e)
    {
        Close();
    }

    /* gets exercise categories and lists them as options in the picker */
    public async void Get_Exercise_Categories()
    {
        List<Category> categories = await App.RecordRepo.Get_Calendar_Category_List();
        List<string> category_string_list = new List<string>();

        /* loops through the list of categories found on database */
        for (int i = 0; i < categories.Count; i++)
        {
            category_string_list.Add(categories[i].name);
        }

        exercise_category.ItemsSource = category_string_list;
    }
}
