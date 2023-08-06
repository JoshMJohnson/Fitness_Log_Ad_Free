using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
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

        double highest_val_entry = -1;
        double lowest_val_entry = -1;

        /* loop through the body weight entries in database */
        for (int i = 0; i < entry_list.Count; i++)
        {
            if (i == 0) /* if first entry */
            {
                highest_val_entry = entry_list[i].weight;
                lowest_val_entry = entry_list[i].weight;
            }
            else if (entry_list[i].weight > highest_val_entry) /* else if current value is new high value */
            {
                highest_val_entry = entry_list[i].weight;
            }
            else if (entry_list[i].weight < lowest_val_entry) /* else if current value is new low value */
            {
                lowest_val_entry = entry_list[i].weight;
            }
        }

        /* loop through the body weight entries in database */
        for (int i = 0; i < entry_list.Count; i++)
        {
            entries.Add(new BodyWeightEntryDot
            {
                date = entry_list[i].date,
                weight = entry_list[i].weight,
                highest_value = highest_val_entry,
                lowest_value = lowest_val_entry
            });
        }

        Update_Y_Axis(entry_list);
    }

    /* picks accurate y axis label values and displays them */
    public void Update_Y_Axis(List<BodyWeightEntry> entry_list) 
    {
        double highest_body_weight_value = -1;
        double lowest_body_weight_value = -1;
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
                y_axis_6.Text = entries[0].weight.ToString();
                y_axis_5.Text = "----";
                y_axis_4.Text = "----";
                y_axis_3.Text = "----";
                y_axis_2.Text = "----";
                y_axis_1.Text = "----";
            }
            else /* more than one entry */ 
            {
                double total_weight_change = highest_body_weight_value - lowest_body_weight_value;
                double total_weight_change_gap = total_weight_change / 5.0; 

                double y_value5 = (total_weight_change_gap * 4.0) + lowest_body_weight_value;
                double y_value4 = (total_weight_change_gap * 3.0) + lowest_body_weight_value;
                double y_value3 = (total_weight_change_gap * 2.0) + lowest_body_weight_value;
                double y_value2 = total_weight_change_gap + lowest_body_weight_value;

                /* rounds y-axis values to 1 decimal point */
                y_value5 = Math.Round(y_value5, 2);
                y_value4 = Math.Round(y_value4, 2);
                y_value3 = Math.Round(y_value3, 2);
                y_value2 = Math.Round(y_value2, 2);

                y_axis_6.Text = highest_body_weight_value.ToString();
                y_axis_5.Text = y_value5.ToString();
                y_axis_4.Text = y_value4.ToString();
                y_axis_3.Text = y_value3.ToString();
                y_axis_2.Text = y_value2.ToString();
                y_axis_1.Text = lowest_body_weight_value.ToString();

                double half_gap = total_weight_change_gap / 2.0;

                for (int i = 0; i < entries.Count; i++) /* adjusts y value of entry marker */
                {
                    double min_group6 = highest_body_weight_value - half_gap;
                    double min_group5 = highest_body_weight_value - half_gap - total_weight_change_gap;
                    double min_group4 = highest_body_weight_value - half_gap - (total_weight_change_gap * 2.0);
                    double min_group3 = highest_body_weight_value - half_gap - (total_weight_change_gap * 3.0);
                    double min_group2 = highest_body_weight_value - half_gap - (total_weight_change_gap * 4.0);

                    int final_adjustments = -3;

                    var main_display_info = DeviceDisplay.MainDisplayInfo;
                    double screen_pixels_height = main_display_info.Height;

                    int heading_height = 220;
                    double heading_height_pixels = heading_height * 5.4;

                    double chart_height_pixels = screen_pixels_height - heading_height_pixels;
                    chart_height_pixels = Math.Abs(chart_height_pixels);

                    double chart_adjustment_height_pixels = chart_height_pixels / 10.0;

                    Console.WriteLine($"----------------------");
                    Console.WriteLine($"******min_group6: {min_group6}");
                    Console.WriteLine($"******min_group5: {min_group5}");
                    Console.WriteLine($"******min_group4: {min_group4}");
                    Console.WriteLine($"******min_group3: {min_group3}");
                    Console.WriteLine($"******min_group2: {min_group2}");
                    Console.WriteLine($"----------------------");

                    /* slight y value adjustments for entry dot */
                    if (entries[i].weight >= min_group6) /* only positive adjustment */
                    {
                        Console.WriteLine($"******1");
                        double diff_value_from_line = highest_body_weight_value - entries[i].weight;
                        double ratio_from_line =  diff_value_from_line / half_gap;
                        double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                        int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                        final_adjustments += adjustment_value;
                    }
                    else if (entries[i].weight >= min_group5) 
                    {
                        if (entries[i].weight > y_value5) /* negative adjustment; above line */
                        {
                            Console.WriteLine($"******2");
                            double diff_value_from_line = entries[i].weight - y_value5;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments -= adjustment_value;
                        }
                        else /* positive adjustment; below line */ 
                        {
                            Console.WriteLine($"******3");
                            double diff_value_from_line = y_value5 - entries[i].weight;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments += adjustment_value;
                        }
                    }
                    else if (entries[i].weight >= min_group4) 
                    {
                        if (entries[i].weight > y_value4) /* negative adjustment; above line */
                        {
                            Console.WriteLine($"******4");
                            double diff_value_from_line = entries[i].weight - y_value4;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments -= adjustment_value;
                        }
                        else /* positive adjustment; below line */
                        {
                            Console.WriteLine($"******5");
                            double diff_value_from_line = y_value4 - entries[i].weight;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments += adjustment_value;
                        }
                    }
                    else if (entries[i].weight >= min_group3)
                    {
                        if (entries[i].weight > y_value3) /* negative adjustment; above line */
                        {
                            Console.WriteLine($"******6");
                            double diff_value_from_line = entries[i].weight - y_value3;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments -= adjustment_value;
                        }
                        else /* positive adjustment; below line */
                        {
                            Console.WriteLine($"******7");
                            double diff_value_from_line = y_value3 - entries[i].weight;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments += adjustment_value;
                        }
                    }
                    else if (entries[i].weight >= min_group2)
                    {
                        if (entries[i].weight > y_value2) /* negative adjustment; above line */
                        {
                            Console.WriteLine($"******8");
                            double diff_value_from_line = entries[i].weight - y_value2;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments -= adjustment_value;
                        }
                        else /* positive adjustment; below line */
                        {
                            Console.WriteLine($"******9");
                            double diff_value_from_line = y_value2 - entries[i].weight;
                            double ratio_from_line = diff_value_from_line / half_gap;
                            double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                            int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                            final_adjustments += adjustment_value;
                        }
                    }
                    else /* only negative adjustment */
                    {
                        Console.WriteLine($"******10");
                        double diff_value_from_line = entries[i].weight - lowest_body_weight_value;
                        double ratio_from_line = diff_value_from_line / half_gap;
                        double chart_adjustment_height = chart_adjustment_height_pixels / 4.0;
                        int adjustment_value = (int) (chart_adjustment_height * ratio_from_line);

                        final_adjustments -= adjustment_value;
                    }

                    Console.WriteLine($"******final_adjustments: {final_adjustments}");
                    entries[i].y_adjustment = final_adjustments;
                }
            }
        }
        else /* else no body weight entries found */
        {
            y_axis_6.Text = "----";
            y_axis_5.Text = "----";
            y_axis_4.Text = "----";
            y_axis_3.Text = "----";
            y_axis_2.Text = "----";
            y_axis_1.Text = "----";
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