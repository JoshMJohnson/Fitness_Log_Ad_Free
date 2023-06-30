using CommunityToolkit.Maui.Views;
using System;
using System.Diagnostics;
using WorkoutLog.Model;
using WorkoutLog.Popup;

namespace WorkoutLog;

public partial class WorkoutCalendar : ContentPage
{
    private VerticalStackLayout vertical_layout_empty_category_list;
    private int max_categories = 6;

	public WorkoutCalendar()
	{
		InitializeComponent();

        /* display when PR list is empty */
        vertical_layout_empty_category_list = new VerticalStackLayout();

        vertical_layout_empty_category_list.VerticalOptions = LayoutOptions.Center;
        vertical_layout_empty_category_list.HorizontalOptions = LayoutOptions.Center;

        vertical_layout_empty_category_list.Add(new Label
        {
            Text = "No Categories Yet!",
            FontSize = 12,
            FontAttributes = FontAttributes.Italic,
            HorizontalOptions = LayoutOptions.Center,
        });

        

        Grid goal_layout = category_layout;
        Grid.SetRow(vertical_layout_empty_category_list, 0);
        goal_layout.Add(vertical_layout_empty_category_list);

        Retrieve_Categories();
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
    private async void Retrieve_Categories()
    {
        List<Category> category_list = await App.RecordRepo.Get_Calendar_Category_List();
        workout_category_display.ItemsSource = category_list;

        if (category_list.Count == 0)  /* if pr list is empty - no pr's set */
        {
            vertical_layout_empty_category_list.IsVisible = true;
        }
        else  /* else pr list is not empty */
        {
            vertical_layout_empty_category_list.IsVisible = false;
        }
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