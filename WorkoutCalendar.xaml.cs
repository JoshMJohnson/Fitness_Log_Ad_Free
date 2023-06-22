using WorkoutLog.Model;

namespace WorkoutLog;

public partial class WorkoutCalendar : ContentPage
{
	public WorkoutCalendar()
	{
		InitializeComponent();

        Refresh_Categories();
    }

    /* button clicked to create a workout category */
    private async void Create_Category(object sender, EventArgs e)
    {
        await DisplayAlert("Create Category", "ya", "Create");
    }

    /* button clicked to remove a workout category */
    private async void Remove_Category(object sender, EventArgs e)
    {
        await DisplayAlert("Remove Category", "ya", "Remove");
    }

    /* updates category display */
    private void Refresh_Categories()
    {
        List<Category> category_list = new List<Category>();

        /* TESTING TEMP LIST start */
        Category temp1 = new Category();
        Category temp2 = new Category();
        Category temp3 = new Category();
        Category temp4 = new Category();

        temp1.Id = 1;
        temp1.Name = "Push";
        temp1.Color = "red";

        temp2.Id = 2;
        temp2.Name = "Pull";
        temp2.Color = "green";

        temp3.Id = 3;
        temp3.Name = "Legs";
        temp3.Color = "blue";

        temp4.Id = 4;
        temp4.Name = "Running";
        temp4.Color = "yellow";

        category_list.Add(temp1);
        category_list.Add(temp2);
        category_list.Add(temp3);
        category_list.Add(temp4);
        /* TESTING TEMP LIST end */

        workout_category_display.ItemsSource = category_list;
    }
}