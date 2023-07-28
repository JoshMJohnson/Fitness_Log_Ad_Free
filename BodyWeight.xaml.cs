namespace WorkoutLog;
using WorkoutLog.Popup;
using WorkoutLog.Model;
using CommunityToolkit.Maui.Views;

public partial class BodyWeight : ContentPage
{
	/* heading variables */
	public double actual_weight { get; set; }
	public double change_weight { get; set; }
    public double goal_weight { get; set; }
    public double week_change { get; set; }
    public double month_change { get; set; }
    public double total_change { get; set; }

    public BodyWeight()
	{
		InitializeComponent();
        Refresh_Table_Data();
    }

    /* executed when body weight plus button clicked to record an update in body weight */
    private async void Record_Body_Weight(object sender, EventArgs e)
    {
        object result = await this.ShowPopupAsync(new BodyWeightAddPopup());

        if (result != null) /* if body weight entry was made */
        {
            Refresh_Table_Data();
        }
    }

    /* refreshes the Body Weight table data */
    private async void Refresh_Table_Data()
    {
        List<BodyWeightEntry> entries = await App.RecordRepo.Get_Body_Weight_List();

        chart_data_display.ItemsSource = entries; /* updates the chart data */

        /* 'Actual' cell */
        actual_weight_display.Text = entries[0].weight.ToString();

        /* 'Change' cell */
        int most_recent_weight_entry = entries[0].weight;
        int second_most_recent_weight_entry = entries[1].weight;
        int weight_change = most_recent_weight_entry - second_most_recent_weight_entry;

        change_weight_display.Text = weight_change.ToString();


        /* todo 'Closest Goal' cell */
        //goal_display.Text = goal_weight.ToString();


        /* todo 'This Week' cell */
        //week_change_display.Text = week_change.ToString();


        /* todo 'This Month' cell */
        //month_change_display.Text = month_change.ToString();


        /* todo 'Total' cell */
        //total_change_display.Text = total_change.ToString();




    }
}