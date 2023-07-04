using System.Collections.ObjectModel;
using System.Windows.Input;
using WorkoutLog.Model;

namespace WorkoutLog.CustomControls;

public partial class CalendarView : StackLayout
{
	#region BinableProperty
	public static readonly BindableProperty selected_date_property = BindableProperty.Create(
		nameof(selected_date),
		typeof(DateTime),
		declaringType: typeof(CalendarView),
		defaultBindingMode: BindingMode.TwoWay,
		defaultValue: DateTime.Now
		);

	public DateTime selected_date
	{
		get => (DateTime) GetValue(selected_date_property);
		set => SetValue(selected_date_property, value);
	}

	private DateTime _tempDate;
    #endregion

    public ObservableCollection<CalendarDay> dates { get; set; } = new ObservableCollection<CalendarDay>();

	public CalendarView()
	{
		InitializeComponent();
        Bind_Dates(DateTime.Now);
    }

	private void Bind_Dates(DateTime date)
	{
		dates.Clear();

		int days_count = DateTime.DaysInMonth(date.Year, date.Month); /* get num of days in selected month */

		/* loop through the days in the selected month */
		for (int day = 1; day <= days_count; day++)
		{
			dates.Add(new CalendarDay
			{
				date = new DateTime(date.Year, date.Month, day)
			});
		}

		var selected_date_data = dates.Where(d => d.date.Date == selected_date.Date).FirstOrDefault();

		if (selected_date_data != null)
		{
			selected_date_data.is_current_date = true;
			_tempDate = selected_date_data.date;
		}
	}

	#region Commands
	public ICommand current_date_command => new Command<CalendarDay>((current_date) =>
	{
		_tempDate = current_date.date;
		selected_date = current_date.date;
		dates.ToList().ForEach(d => d.is_current_date = false); 
		current_date.is_current_date = true;
	});

	public ICommand next_month_command => new Command(() =>
	{
		_tempDate = _tempDate.AddMonths(1);
		Bind_Dates(_tempDate);
	});

    public ICommand previous_month_command => new Command(() =>
    {
        _tempDate = _tempDate.AddMonths(-1);
        Bind_Dates(_tempDate);
    });
    #endregion
}