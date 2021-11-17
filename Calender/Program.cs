using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please Enter Birth:");
        var inputDate = Console.ReadLine();


        PersianCalendar calander = new PersianCalendar();
        DateTime date = DateTime.Parse(inputDate);
        var converDate = calander.ToDateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        Console.WriteLine(converDate.ToString("yyyy/MM/dd"));
        Console.ReadLine();
    }
}


public class PersianCulture : CultureInfo
{
    private readonly Calendar cal;
    private readonly Calendar[] optionals;

    public PersianCulture(string cultureName, bool useUserOverride)
        : base(cultureName, useUserOverride)
    {
        cal = base.OptionalCalendars[0];


        var optionalCalendars = new List<Calendar>();
        optionalCalendars.AddRange(base.OptionalCalendars);
        optionalCalendars.Insert(0, new PersianCalendar());



        Type calendarType = typeof(Calendar);


        PropertyInfo idProperty = calendarType.GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);


        var newOptionalCalendarIDs = new Int32[optionalCalendars.Count];
        for (int i = 0; i < newOptionalCalendarIDs.Length; i++)
            newOptionalCalendarIDs[i] = (Int32)idProperty.GetValue(optionalCalendars[i], null);


        optionals = optionalCalendars.ToArray();
        cal = optionals[0];
        DateTimeFormat.Calendar = optionals[0];

    }

    public override Calendar Calendar
    {
        get { return cal; }
    }

    public override Calendar[] OptionalCalendars
    {
        get { return optionals; }
    }
}

