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

		actual_weight = Preferences.Get("ActualWeight", 0);
        change_weight = Preferences.Get("ChangeWeight", 0);
        week_change = Preferences.Get("WeekChangeWeight", 0);
        month_change = Preferences.Get("MonthChangeWeight", 0);
        total_change = Preferences.Get("TotalChangeWeight", 0);

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
        /* todo 'Actual' cell */
        /* todo 'Change' cell */
        /* todo 'Closest Goal' cell */
        /* todo 'This Week' cell */
        /* todo 'This Month' cell */
        /* todo 'Total' cell */

        chart_data_display.ItemsSource = await App.RecordRepo.Get_Body_Weight_List();


        actual_weight_display.Text = actual_weight.ToString();
        change_weight_display.Text = change_weight.ToString();
        goal_display.Text = goal_weight.ToString();
        week_change_display.Text = week_change.ToString();
        month_change_display.Text = month_change.ToString();
        total_change_display.Text = total_change.ToString();
    }
}