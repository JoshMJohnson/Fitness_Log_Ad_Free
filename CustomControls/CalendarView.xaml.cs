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
    #endregion

    public ObservableCollection<CalendarDay> dates { get; set; } = new ObservableCollection<CalendarDay>();

	public CalendarView()
	{
		InitializeComponent();
        Bind_Dates(DateTime.Now);
    }

	private void Bind_Dates(DateTime selected_date)
	{
		int days_count = DateTime.DaysInMonth(selected_date.Year, selected_date.Month); /* get num of days in selected month */

		/* loop through the days in the selected month */
		for (int day = 1; day <= days_count; day++)
		{
			dates.Add(new CalendarDay
			{
				date = new DateTime(selected_date.Year, selected_date.Month, day)
			});
		}

		dates.Where(d => d.date.Date == selected_date.Date).FirstOrDefault().is_current_date = true;
	}

	#region Commands
	public ICommand current_date_command => new Command<CalendarDay>((current_date) =>
	{
		selected_date = current_date.date;
		dates.ToList().ForEach(d => d.is_current_date = false); 
		current_date.is_current_date = true;
	});
    #endregion
}