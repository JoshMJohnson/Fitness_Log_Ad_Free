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
    private async void Fill_Chart()
    {
        List<BodyWeightEntry> entry_list = await App.RecordRepo.Get_Body_Weight_List();

        /* loop through the days in the selected month */
        for (int i = 0; i < entry_list.Count; i++)
        {
            entries.Add(new BodyWeightEntryDot
            {
                date = entry_list[i].date,
                weight = entry_list[i].weight,
            });
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