using WorkoutLog.Model;

namespace WorkoutLog.Cells;

public class EntryDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Min_Group_6 { get; set; }
    public DataTemplate Min_Group_5 { get; set; }
    public DataTemplate Min_Group_4 { get; set; }
    public DataTemplate Min_Group_3 { get; set; }
    public DataTemplate Min_Group_2 { get; set; }
    public DataTemplate Group_1 { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var body_weight_entry = (BodyWeightEntryDot) item;

        int total_weight_change = body_weight_entry.highest_value - body_weight_entry.lowest_value;
        double total_weight_change_gap = total_weight_change / 5;

        int half_gap = (int) (total_weight_change_gap / 2);

        int min_group6 = (int) (body_weight_entry.highest_value - half_gap);
        int min_group5 = (int) (body_weight_entry.highest_value - half_gap - total_weight_change_gap);
        int min_group4 = (int) (body_weight_entry.highest_value - half_gap - (total_weight_change_gap * 2));
        int min_group3 = (int) (body_weight_entry.highest_value - half_gap - (total_weight_change_gap * 3));
        int min_group2 = (int) (body_weight_entry.highest_value - half_gap - (total_weight_change_gap * 4));

        if (body_weight_entry.weight >= min_group6) /* if entry in top 1/6 */
        {
            return Min_Group_6;
        }
        else if (body_weight_entry.weight >= min_group5) /* else if entry is top 2/6th */
        {
            return Min_Group_5;
        }
        else if (body_weight_entry.weight >= min_group4)  /* else if entry is top 2/6th */
        {
            return Min_Group_4;
        }
        else if (body_weight_entry.weight >= min_group3)  /* else if entry is top 2/6th */
        {
            return Min_Group_3;
        }
        else if (body_weight_entry.weight >= min_group2)  /* else if entry is top 2/6th */
        {
            return Min_Group_2;
        }
        else /* else; entry is lowest 6th */
        {
            return Group_1;
        }
    }
}