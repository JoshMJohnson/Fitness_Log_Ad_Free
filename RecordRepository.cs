using Android.OS;
using SQLite;
using System.Net;
using System.Xml.Linq;
using WorkoutLog.Model;
using static SQLite.SQLite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkoutLog;

public class RecordRepository
{
	private static SQLiteAsyncConnection conn;
    private string database_path;
    public string status_message { get; set; }

	/* constructor - creates a database */
	public RecordRepository(string db_path)
	{
        database_path = db_path;
	}

    /* initializes database */
    private async Task Init_Database()
    {
        if (conn != null) { return; }

        conn = new SQLiteAsyncConnection(database_path); /* create database */

        /* create database tables */
        await conn.CreateTablesAsync<Category, PR, GoalPR, GoalBW, CalendarEntry>();
        await conn.CreateTableAsync<Progression>();


        /*
         await conn.CreateTableAsync<BodyWeight>();
        */
    }

    /* * body progression section */
    /* todo adds an entry to the body progression table within the database */
    public async Task Add_Progression(string date)
	{

	}

    /* todo removes an entry in the body progression table within the database */
    public async Task Remove_Progression()
    {

    }

    /* ? returns a list of progressions from the database */
    public async Task<List<Progression>> Get_Progression_List()
    {
        try
        {
            await Init_Database();
            List<Progression> body_progression_list = await conn.Table<Progression>().ToListAsync();
            return body_progression_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<Progression>();
    }

    /* * body weight section */
    /* todo adds an entry to the body weight table within the database */
    public async Task Add_Body_Weight(int weight, DateTime date)
    {

    }

    /* ? edit an entry in the body weight table within the database */
    public async Task Edit_Body_Weight(DateTime date, int updated_weight)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(updated_weight, nameof(updated_weight));

        try
        {
            await Init_Database();

            BodyWeightEntry updating_body_weight_entry = await conn.FindAsync<BodyWeightEntry>(date);
            updating_body_weight_entry.weight = updated_weight;

            await conn.UpdateAsync(updating_body_weight_entry);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to update. Error: {0}", e.Message);
        }
    }

    /* todo removes an entry in the body weight table within the database */
    public async Task Remove_Body_Weight()
    {

    }

    /* ? returns a list of body weight entries from the database */
    public async Task<List<BodyWeight>> Get_Body_Weight_List()
    {
        try
        {
            await Init_Database();
            List<BodyWeight> body_weight_list = await conn.Table<BodyWeight>().ToListAsync();
            return body_weight_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<BodyWeight>();
    }

    /* * body weight goals section */
    /* adds a body weight entry to the goal table within the database */
    public async Task Add_Goal_Body_Weight(string goal_name, DateTime date, bool has_desired, int goal_weight)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(has_desired, nameof(has_desired));
        ArgumentNullException.ThrowIfNull(goal_weight, nameof(goal_weight));

        try
        {
            await Init_Database();
            string date_only;

            if (has_desired) /* if a date set for pr goal */
            {
                /* translate DateTime to string; removes the time display */
                string[] date_time_temp = date.ToString().Split(' ');
                date_only = date_time_temp[0];
            }
            else /* else no date of goal set */
            {
                date_only = "N/A";
            }

            GoalBW new_goal = new GoalBW
            {
                name = goal_name,
                goal_achieve_by_date = date_only,
                date_desired = has_desired,
                weight = goal_weight,
            };

            int result = await conn.InsertAsync(new_goal);

            status_message = string.Format("{0} goal added (Goal date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add goal date: {0}. Error: {1}", date, e.Message);
        }
    }

    /* removes a body weight entry in the goal table within the database */
    public async Task Remove_Goal_Body_Weight(string goal_name)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));

        try
        {
            await Init_Database();
            GoalBW removing_bw_goal = await conn.FindAsync<GoalBW>(goal_name);
            await conn.DeleteAsync(removing_bw_goal);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", goal_name, e.Message);
        }
    }

    /* returns a list of body weight goals from the database */
    public async Task<List<GoalBW>> Get_Body_Weight_Goal_List()
    {
        try
        {
            await Init_Database();

            List<GoalBW> body_weight_goal_list = await conn.Table<GoalBW>().ToListAsync();            
            body_weight_goal_list = body_weight_goal_list.OrderBy(goal => goal.date_desired).ToList();

            List<GoalBW> sorted_body_weight_goal_list = new List<GoalBW>();
            List<GoalBW> final_list = new List<GoalBW>();

            int paused_index = 0;

            /* loops through list of body weight goal entries found in database */
            for (int i = 0; i < body_weight_goal_list.Count; i++)
            {
                string[] date_array = body_weight_goal_list[i].goal_achieve_by_date.Split('/');

                if (body_weight_goal_list[i].date_desired) /* if goal has no date desired set */
                {
                    if (i == 0) /* if there are no N/A dates in database */
                    {
                        paused_index = i;
                        break;
                    }

                    body_weight_goal_list[i].date_sort = new DateTime(int.Parse(date_array[2]), int.Parse(date_array[0]), int.Parse(date_array[1]));
                    sorted_body_weight_goal_list.Add(body_weight_goal_list[i]); 
                }
                else /* else; no goal date set */
                {
                    paused_index = i;
                    break;
                }
            }

            sorted_body_weight_goal_list = sorted_body_weight_goal_list.OrderBy(goal => goal.date_sort).ToList();

            /* adds all none date desired body weight goals to final list */
            for (int i = paused_index; i < body_weight_goal_list.Count; i++)
            {
                final_list.Add(body_weight_goal_list[i]);
            }

            /* adds all date desired body weight goals to final list */
            for (int i = 0; i < sorted_body_weight_goal_list.Count; i++)
            {
                final_list.Add(sorted_body_weight_goal_list[i]);
            }

            return final_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<GoalBW>();
    }

    /* * pr goals section */
    /* adds a pr entry to the goal table within the database */
    public async Task Add_Goal_PR(string goal_name, DateTime date, bool has_desired, bool is_weight, 
                                    int goal_weight, int hours, int mins, int sec)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(has_desired, nameof(has_desired));
        ArgumentNullException.ThrowIfNull(is_weight, nameof(is_weight));
        ArgumentNullException.ThrowIfNull(goal_weight, nameof(goal_weight));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(mins, nameof(mins));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        if (is_weight) /* if weight pr type */
        {
            hours = -1;
            mins = -1;
            sec = -1;
        }
        else /* if time pr type */
        {
            goal_weight = -1;
        }

        try
        {
            await Init_Database();
            string date_only;

            if (has_desired) /* if a date set for pr goal */
            {
                /* translate DateTime to string; removes the time display */
                string[] date_time_temp = date.ToString().Split(' ');
                date_only = date_time_temp[0];
            }
            else /* else no date of goal set */
            {
                date_only = "N/A";
            }

            GoalPR new_goal = new GoalPR
            {
                name = goal_name,
                goal_achieve_by_date = date_only,
                date_desired = has_desired,
                is_weight_goal = is_weight,
                weight = goal_weight,
                time_hours = hours,
                time_min = mins,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_goal);

            status_message = string.Format("{0} PR goal added (Goal date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add PR goal date: {0}. Error: {1}", date, e.Message);
        }
    }

    /* removes a pr goal entry in the GoalPR table within the database */
    public async Task Remove_Goal_PR(string goal_name)
    {
        ArgumentNullException.ThrowIfNull(goal_name, nameof(goal_name));

        try
        {
            await Init_Database();
            GoalPR removing_pr_goal = await conn.FindAsync<GoalPR>(goal_name);
            await conn.DeleteAsync(removing_pr_goal);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", goal_name, e.Message);
        }
    }

    /* returns a list of pr goals from the database */
    public async Task<List<GoalPR>> Get_Goal_PR_List()
    {
        try
        {
            await Init_Database();

            List<GoalPR> pr_goal_list = await conn.Table<GoalPR>().ToListAsync();
            pr_goal_list = pr_goal_list.OrderBy(goal => goal.date_desired).ToList();

            List<GoalPR> sorted_pr_goal_list = new List<GoalPR>();
            List<GoalPR> final_list = new List<GoalPR>();

            int paused_index = 0;

            /* loops through list of pr goal entries found in database */
            for (int i = 0; i < pr_goal_list.Count; i++)
            {
                string[] date_array = pr_goal_list[i].goal_achieve_by_date.Split('/');

                if (pr_goal_list[i].date_desired) /* if goal has no date desired set */
                {
                    if (i == 0) /* if there are no N/A dates in database */
                    {
                        paused_index = i;
                        break;
                    }

                    pr_goal_list[i].date_sort = new DateTime(int.Parse(date_array[2]), int.Parse(date_array[0]), int.Parse(date_array[1]));
                    sorted_pr_goal_list.Add(pr_goal_list[i]);
                }
                else /* else; no goal date set */
                {
                    paused_index = i;
                    break;
                }
            }

            sorted_pr_goal_list = sorted_pr_goal_list.OrderBy(goal => goal.date_sort).ToList();

            /* adds all none date desired pr goals to final list */
            for (int i = paused_index; i < pr_goal_list.Count; i++)
            {
                final_list.Add(pr_goal_list[i]);
            }

            /* adds all date desired pr goals to final list */
            for (int i = 0; i < sorted_pr_goal_list.Count; i++)
            {
                final_list.Add(sorted_pr_goal_list[i]);
            }

            return final_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<GoalPR>();
    }

    /* * personal records section */
    /* adds a pr entry to the pr table within the database */
    public async Task Add_PR(string name, DateTime date, bool is_weight_pr_type,
                                int weight_pr, int hours, int min, int sec)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(is_weight_pr_type, nameof(is_weight_pr_type));
        ArgumentNullException.ThrowIfNull(weight_pr, nameof(weight_pr));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(min, nameof(min));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));

        try
        {
            await Init_Database();

            /* translate DateTime to string; removes the time display */
            string[] date_time_temp = date.ToString().Split(' ');
            string date_only = date_time_temp[0];

            PR new_pr = new PR
            {
                exercise_name = name,
                date_achieved = date_only,
                is_weight_pr = is_weight_pr_type,
                weight = weight_pr,
                time_hours = hours,
                time_min = min,
                time_sec = sec
            };

            int result = await conn.InsertAsync(new_pr);

            status_message = string.Format("{0} PR added (PR Name: {1})", result, name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add {0}. Error: {1}", name, e.Message);
        }
    }

    /* updates a pr entry in the pr table within the database */
    public async Task Update_PR(string exercise_name, DateTime date, int updated_weight, int hours, int mins, int sec)
    {
        ArgumentNullException.ThrowIfNull(exercise_name, nameof(exercise_name));
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(updated_weight, nameof(updated_weight));
        ArgumentNullException.ThrowIfNull(hours, nameof(hours));
        ArgumentNullException.ThrowIfNull(mins, nameof(mins));
        ArgumentNullException.ThrowIfNull(sec, nameof(sec));
        
        try
        {
            await Init_Database();
            PR updating_pr = await conn.FindAsync<PR>(exercise_name);

            /* translate DateTime to string; removes the time display */
            string[] date_time_temp = date.ToString().Split(' ');
            string date_only = date_time_temp[0];

            updating_pr.date_achieved = date_only;

            if (updating_pr.is_weight_pr) /* if updating pr is a weight pr */
            {
                updating_pr.weight = updated_weight;
            }
            else /* else updating pr is a timed pr */
            {
                updating_pr.time_hours = hours;
                updating_pr.time_min = mins;
                updating_pr.time_sec = sec;
            }

            await conn.UpdateAsync(updating_pr);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to update. Error: {0}", e.Message);
        }
    }

    /* removes a pr entry in the pr table within the database */
    public async Task Remove_PR(string exercise_name)
    {
        ArgumentNullException.ThrowIfNull(exercise_name, nameof(exercise_name));

        try
        {
            await Init_Database();
            PR removing_pr = await conn.FindAsync<PR>(exercise_name);
            await conn.DeleteAsync(removing_pr);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove {0}. Error: {1}", exercise_name, e.Message);
        }
    }

    /* returns a pr object from database with primary key matching parameter */
    public async Task<PR> Get_PR(string exercise_name)
    {
        PR updating_pr = await conn.FindAsync<PR>(exercise_name);
        return updating_pr;
    }

    /* returns a list of PR's from the database */
    public async Task<List<PR>> Get_PR_List()
    {
        try
        {
            await Init_Database();
            List<PR> pr_list = await conn.Table<PR>().ToListAsync();
            pr_list = pr_list.OrderBy(pr => pr.exercise_name).ToList();
            return pr_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<PR>();
    }

    /* * workout calendar section*/
    /* adds an entry to the workout calendar table within the database */
    public async Task Add_Calendar_Entry(string category_name, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));

        try
        {
            await Init_Database();

            CalendarEntry calendar_entry = new CalendarEntry
            {
                entry_date = date,
                calendar_category_name = category_name
            };

            int result = await conn.InsertAsync(calendar_entry);

            status_message = string.Format("{0} calendar entry made for {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* removes an entry in the workout calendar table within the database */
    public async Task Remove_Calendar_Entry(DateTime date, Category category)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        try
        {
            await Init_Database();

            List<CalendarEntry> day_entries_list = await Get_Calendar_Entries_List(date); /* gets list of all entries for that day */

            /* gets list of calendar entries for selected date that match category for removal */
            List<CalendarEntry> day_entries_list_matching_removal_category = new List<CalendarEntry>();
            foreach (CalendarEntry day_entry in day_entries_list)
            {
                if (day_entry.calendar_category_name == category.name) /* if category matches removal category */ 
                { 
                    day_entries_list_matching_removal_category.Add(day_entry);
                }
            }

            /* removes 1 instance of entry for that day */
            int result = await conn.DeleteAsync(day_entries_list_matching_removal_category[0]);

            status_message = string.Format("{0} calendar entry removed (Date: {1})", result, date);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove calendar entry on {0}. Error: {1}", date, e.Message);
        }
    }

    /* returns a list of all calendar entries from the database with a date equal to the parameter */
    public async Task<List<CalendarEntry>> Get_Calendar_Entries_List(DateTime date)
    {
        ArgumentNullException.ThrowIfNull(date, nameof(date));

        try
        {
            await Init_Database();
            List<CalendarEntry> calendar_entry_list = await conn.Table<CalendarEntry>().Where(d => d.entry_date == date).ToListAsync();
            return calendar_entry_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<CalendarEntry>();
    }

    /* creates a category to the categories table within the database */
    public async Task Add_Calendar_Category(string category_name)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));

        try
        {
            await Init_Database();

            /* if category already exists but marked as not available; mark as available */
            Category existing_category = await Get_Category(category_name);
            int result;

            if (existing_category != null) /* if category already exists; mark as available */
            {
                existing_category.still_available = true;
                result = await conn.UpdateAsync(existing_category);
            }
            else /* else no record of category; create a new category */
            {
                Category new_category = new Category
                {
                    name = category_name,
                    still_available = true
                };

                result = await conn.InsertAsync(new_category);
            }

            status_message = string.Format("{0} category added (Category Name: {1})", result, category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to add category {0}. Error: {1}", category_name, e.Message);
        }
    }
    
    /* removes a category in the categories table within the database */
    public async Task Remove_Calendar_Category(string category_name)
    {
        ArgumentNullException.ThrowIfNull(category_name, nameof(category_name));

        try
        {
            await Init_Database();

            Category removing_category = await conn.FindAsync<Category>(category_name);

            removing_category.still_available = false;

            await conn.UpdateAsync(removing_category);

            status_message = string.Format("category removed (Category Name: {1})", category_name);
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to remove category {0}. Error: {1}", category_name, e.Message);
        }
    }

    /* returns a list of all calendar categories that are still available from the database */
    public async Task<List<Category>> Get_Calendar_Category_List()
    {
        try
        {
            await Init_Database();

            List<Category> category_list = await conn.Table<Category>().ToListAsync();
            List<Category> available_category_list = new List<Category>();

            /* do not include unavailable categories */
            foreach(Category category in category_list) 
            {
                if (category.still_available)
                {                    
                    available_category_list.Add(category);
                }
            }

            return available_category_list;
        }
        catch (Exception e)
        {
            status_message = string.Format("Failed to retrieve data. {0}", e.Message);
        }

        return new List<Category>();
    }

    /* returns a category object from database with primary key matching parameter */
    public async Task<Category> Get_Category(string category_name)
    {
        Category temp_category = await conn.FindAsync<Category>(category_name);
        return temp_category;
    }
}