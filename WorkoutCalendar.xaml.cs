using CommunityToolkit.Maui.Views;
using System;
using System.Diagnostics;
using WorkoutLog.Model;
using WorkoutLog.Popup;

namespace WorkoutLog;

public partial class WorkoutCalendar : ContentPage
{
    private int max_categories = 6;

	public WorkoutCalendar()
	{
		InitializeComponent();

        Refresh_Categories();
    }

    /* button clicked to create a workout category */
    private async void Create_Category(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new CalendarCategoryAddPopup());
    }

    /* button clicked to remove a workout category */
    private async void Remove_Category(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new CalendarCategoryRemovePopup());
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

        temp1.id = 1;
        temp1.name = "Push";
        temp1.color = "red";

        temp2.id = 2;
        temp2.name = "Pull";
        temp2.color = "green";

        temp3.id = 3;
        temp3.name = "Legs";
        temp3.color = "blue";

        temp4.id = 4;
        temp4.name = "Running";
        temp4.color = "yellow";

        temp5.id = 5;
        temp5.name = "calisthenics";
        temp5.color = "brown";

        temp6.id = 6;
        temp6.name = "yoga";
        temp6.color = "pink";

        category_list.Add(temp1);
        category_list.Add(temp2);
        category_list.Add(temp3);
        category_list.Add(temp4);
        category_list.Add(temp5);
        category_list.Add(temp6);
        /* TESTING TEMP LIST end */

        workout_category_display.ItemsSource = category_list;
    }

    /* executes when the plus button is clicked to record an exercise */
    private async void Record_Exercise(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new CalendarAddPopup());                
    }

    /* executes when the minus button is clicked to record an exercise */
    private async void Unrecord_Exercise(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new CalendarRemovePopup());
    }
}