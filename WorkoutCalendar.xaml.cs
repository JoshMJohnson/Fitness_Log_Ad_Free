using System;
using System.Diagnostics;
using WorkoutLog.Model;

namespace WorkoutLog;

public partial class WorkoutCalendar : ContentPage
{
    private int max_categories = 6;

	public WorkoutCalendar()
	{
		InitializeComponent();

        Refresh_Categories();

        /* set and display default values */
        exercise_category.SelectedIndex = 0;
        record_date.Date = DateTime.Now;
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
        Category temp5 = new Category();
        Category temp6 = new Category();

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

        temp5.Id = 5;
        temp5.Name = "calisthenics";
        temp5.Color = "brown";

        temp6.Id = 6;
        temp6.Name = "yoga";
        temp6.Color = "pink";

        category_list.Add(temp1);
        category_list.Add(temp2);
        category_list.Add(temp3);
        category_list.Add(temp4);
        category_list.Add(temp5);
        category_list.Add(temp6);
        /* TESTING TEMP LIST end */

        workout_category_display.ItemsSource = category_list;
    }

    /* processes a change in exercise category dropdown box selected for recording an exercise event */
    private void Exercise_Category_Change(object sender, EventArgs e)
    {

    }

    /* processes a submission to record an exercise performed */
    private async void Record_Exercise(object sender, EventArgs e)
    {
        /* retrieve recording data */
        DateTime date_time = record_date.Date;
        object category = exercise_category.SelectedItem;

        /* format date to remove the time and leave date */
        string date_time_string = date_time.ToString();
        string[] date_array = date_time_string.Split(' ');
        string date = date_array[0];

        await DisplayAlert("Record Exercise", $"Date: {date}\nCategory: {category}", "Submit", "Cancel");
    }

    /* processes a submission to record an exercise performed */
    private async void Unrecord_Exercise(object sender, EventArgs e)
    {
        /* retrieve unrecording data */
        DateTime date_time = record_date.Date;
        object category = exercise_category.SelectedItem;

        /* format date to remove the time and leave date */
        string date_time_string = date_time.ToString();
        string[] date_array = date_time_string.Split(' '); 
        string date = date_array[0];

        await DisplayAlert("Remove Exercise", $"Date: {date}\nCategory: {category}", "Submit", "Cancel");
    }
}