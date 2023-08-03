using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkoutLog.Model;

namespace WorkoutLog.CustomControls;

public partial class ChartView : Grid
{
    #region BinableProperty
    /* updating data after selecting a new date */
    public static readonly BindableProperty selected_entry_property = BindableProperty.Create(
        nameof(selected_entry),
        typeof(DateTime),
        declaringType: typeof(ChartView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: null,
        propertyChanged: Selected_Entry_Property_Changed);

    /* executes when a date is selected */
    private static void Selected_Entry_Property_Changed(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (ChartView) bindable;

        if (newValue != null)
        {
            var new_date = (DateTime) newValue;
            var current_date = controls.entries.Where(d => d.date == new_date.Date).FirstOrDefault();

            if (current_date != null)
            {
                controls.entries.ToList().ForEach(d => d.is_current_date_selected = false);
                current_date.is_current_date_selected = true;
            }
        }
    }

    public static readonly BindableProperty displaying_graph_command_property = BindableProperty.Create(
        nameof(displaying_graph_command),
        typeof(ICommand),
        declaringType: typeof(ChartView));

    public ICommand displaying_graph_command
    {
        get => (ICommand)GetValue(displaying_graph_command_property);
        set => SetValue(displaying_graph_command_property, value);
    }

    /* contains data for the current date selected */
    public DateTime selected_entry
    {
        get => (DateTime) GetValue(selected_entry_property);
        set => SetValue(selected_entry_property, value);
    }

    public static readonly BindableProperty selected_date_command_property = BindableProperty.Create(
        nameof(selected_date_command),
        typeof(ICommand),
        declaringType: typeof(ChartView));

    public ICommand selected_date_command
    {
        get => (ICommand) GetValue(selected_date_command_property);
        set => SetValue(selected_date_command_property, value);
    }

    public event EventHandler<DateTime> on_entry_selected;
    #endregion

    public ObservableCollection<BodyWeightEntryDot> entries { get; set; } = new ObservableCollection<BodyWeightEntryDot>();

    public ChartView()
    {
        InitializeComponent();
        Fill_Chart();
    }

    /* prepares the body weight graph to be displayed; links chart entries with appropriate data */
    public async void Fill_Chart()
    {
        entries.Clear();

        List<BodyWeightEntry> entry_list = await App.RecordRepo.Get_Body_Weight_List();
        entry_list = entry_list.OrderBy(progress => progress.date).ToList();

        /* loop through the days in the selected month */
        for (int i = 0; i < entry_list.Count; i++)
        {
            entries.Add(new BodyWeightEntryDot
            {
                date = entry_list[i].date,
                weight = entry_list[i].weight,
            });
        }

        Update_Y_Axis(entry_list);
    }

    /* picks accurate y axis label values and displays them */
    public void Update_Y_Axis(List<BodyWeightEntry> entry_list)
    {
        int highest_body_weight_value = -1;
        int lowest_body_weight_value = -1;
        bool has_entry = false;

        for (int i = 0; i < entry_list.Count; i++) /* loops through list of body weight entries */
        {
            if (has_entry == false) /* if first entry from list */
            {
                has_entry = true;
                highest_body_weight_value = entry_list[i].weight;
                lowest_body_weight_value = entry_list[i].weight;
                continue;
            }

            if (entry_list[i].weight > highest_body_weight_value) /* if current entry is higher weight value than the current high value */
            {
                highest_body_weight_value = entry_list[i].weight;
            }
            else if (entry_list[i].weight < lowest_body_weight_value) /* if current entry is lower weight value than the current low value */
            {
                lowest_body_weight_value = entry_list[i].weight;
            }
        }

        if (has_entry) /* if at least 1 body weight entry */
        {
            if (highest_body_weight_value == lowest_body_weight_value) /* if only one entry */
            {
                y_axis_3.Text = highest_body_weight_value.ToString();
            }
            else /* more than one entry */
            {
                int total_weight_change = highest_body_weight_value - lowest_body_weight_value;
                double total_weight_change_gap = total_weight_change / 5;

                int y_value5 = (int) ((total_weight_change_gap * 4) + lowest_body_weight_value);
                int y_value4 = (int) ((total_weight_change_gap * 3) + lowest_body_weight_value);
                int y_value3 = (int) ((total_weight_change_gap * 2) + lowest_body_weight_value);
                int y_value2 = (int) ((total_weight_change_gap) + lowest_body_weight_value);

                y_axis_6.Text = highest_body_weight_value.ToString();
                y_axis_5.Text = y_value5.ToString();
                y_axis_4.Text = y_value4.ToString();
                y_axis_3.Text = y_value3.ToString();
                y_axis_2.Text = y_value2.ToString();
                y_axis_1.Text = lowest_body_weight_value.ToString();
            }

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            double screen_height = mainDisplayInfo.Height;
            double chart_height = screen_height * .188;

            for (int i = 0; i < entries.Count; i++) /* adjusts y value of entry marker */
            {
                int total_weight_change = highest_body_weight_value - lowest_body_weight_value;
                int entry_value_lowest_value_diff = entries[i].weight - lowest_body_weight_value;
                
                double adjustment_ratio = (double) (entry_value_lowest_value_diff) / total_weight_change;
                double adjustment_pixels = chart_height * adjustment_ratio;
                int adjustment_pixels_int = (int) adjustment_pixels;

                if (entries[i].weight > int.Parse(y_axis_5.Text)) /* if above second from top bar; adjustment for rounding */
                {
                    adjustment_pixels_int -= 12;
                }

                entries[i].y_adjustment = adjustment_pixels_int * (-1);
            }
        }
    }

    #region Commands
    /* update calendar currently selected date info */
    public ICommand current_date_command => new Command<BodyWeightEntryDot>((current_date) =>
    {
        selected_entry = current_date.date;
        on_entry_selected?.Invoke(null, current_date.date);
        selected_date_command?.Execute(current_date.date);
    });
    #endregion
}