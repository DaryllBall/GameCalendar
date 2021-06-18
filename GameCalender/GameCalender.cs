using UnityEngine;

[DisallowMultipleComponent]

//Attach to a persistant gameObject like a gameManager

#pragma warning disable IDE0059
#pragma warning disable IDE0060

public class GameCalender : MonoBehaviour
{
    [SerializeField] private bool isLeapYear;
    [SerializeField] private bool isGregorian = false;
    [Space]
    [SerializeField, Min(0)] private int startYear;
    [SerializeField, Min(0)] private int currentYear;
    [SerializeField, Min(0)] private int dayOfMonth;
    [Space]
    private protected int dayOfYear;
    private protected int daysInYear = 365;

    [SerializeField] private Month currentMonth;
    [SerializeField] private DaysOfWeek currentDayOfWeek;
    [SerializeField] private Season currentSeason;

    public bool IsLeapYear { get => isLeapYear; set => isLeapYear = value; }
    public bool IsGregorian { get => isGregorian; set => isGregorian = value; }
    public int StartYear { get => startYear; set => startYear = value; }
    public int CurrentYear { get => currentYear; set => currentYear = value; }
    public int DayOfMonth { get => dayOfMonth; set => dayOfMonth = value; }
    public Month CurrentMonth { get => currentMonth; set => currentMonth = value; }
    public DaysOfWeek CurrentDayOfWeek { get => currentDayOfWeek; set => currentDayOfWeek = value; }
    public Season CurrentSeason { get => currentSeason; set => currentSeason = value; }
    public int DayOfYear { get => dayOfYear; set => dayOfYear = value; }
    public int DaysInYear { get => daysInYear; set => daysInYear = value; }

    private void Start()
    {
        SetGameStartDate(startYear, currentMonth, currentDayOfWeek, dayOfMonth, dayOfYear);
    }

    private void SetGameStartDate(int startYear, Month month, DaysOfWeek dayOfWeek, int dayOfMonth, int dayOfYear)
    {
        startYear = StartYear;
        CurrentYear = StartYear;
        month = CurrentMonth;
        dayOfWeek = CurrentDayOfWeek;
        dayOfMonth = DayOfMonth;
        dayOfYear = DayOfYear;
        LeapYearJulian();
    }

    private void LateUpdate()
    {
        DaysInMonth();
        LeapYearData();
        SetSeason();
        CalandarInput();
    }

    private void CalandarInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeDay();
        }
    }

    private void LeapYearData()
    {
        if (CurrentYear >= 1582 && CurrentMonth >= Month.October && DayOfMonth >= 5)
        {
            AdjustForGregorian();
        }

        if (CurrentYear <= 1582 && currentYear >= 709)
        {
            LeapYearJulian();
            IsGregorian = false;
        }
        else
        {
            if (CurrentYear >= 1582)
            {
                LeapYearGregorian();
                IsGregorian = true;
            }
        }

        if (CurrentMonth == Month.December && DayOfMonth > 31)
        {
            CurrentMonth = Month.January;
            DayOfMonth = 1;
            LeapYearGregorian();
        }

        if (DayOfYear > 365 && IsLeapYear == false)
        {
            CurrentYear += 1;
            DayOfYear = 1;
        }

        if (DayOfYear > 366 && IsLeapYear == true)
        {
            CurrentYear += 1;
            DayOfYear = 1;
        }

        if (IsLeapYear == true)
        {
            DaysInYear = 366;
        }
        else
        {
            DaysInYear = 365;
        }
    }

    private void DaysInMonth()
    {
        if (CurrentMonth == Month.January && DayOfMonth > 31)
        {
            CurrentMonth = Month.Febuary;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.Febuary && DayOfMonth > 28 && !IsLeapYear)
        {
            CurrentMonth = Month.March;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.Febuary && DayOfMonth > 29 && IsLeapYear == true)
        {
            CurrentMonth = Month.March;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.March && DayOfMonth > 31)
        {
            CurrentMonth = Month.April;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.April && DayOfMonth > 30)
        {
            CurrentMonth = Month.May;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.May && DayOfMonth > 31)
        {
            CurrentMonth = Month.June;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.June && DayOfMonth > 30)
        {
            CurrentMonth = Month.July;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.July && DayOfMonth > 31)
        {
            CurrentMonth = Month.Augest;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.Augest && DayOfMonth > 31)
        {
            CurrentMonth = Month.September;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.September && DayOfMonth > 30)
        {
            CurrentMonth = Month.October;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.October && DayOfMonth > 31)
        {
            CurrentMonth = Month.November;
            DayOfMonth = 1;
        }

        if (CurrentMonth == Month.November && DayOfMonth > 30)
        {
            CurrentMonth = Month.December;
            DayOfMonth = 1;
        }
    }

    public void ChangeDay()
    {
        DayOfYear += 1;
        DayOfMonth += 1;
        CurrentDayOfWeek += 1;

        if (CurrentDayOfWeek > DaysOfWeek.Sunday)
        {
            CurrentDayOfWeek = DaysOfWeek.Monday;
        }
    }

    public void LeapYearGregorian()
    {
        if (CurrentYear % 4 == 0 && CurrentYear % 100 != 0 || CurrentYear % 400 == 0)
        {
            IsLeapYear = true;
        }
        else
        {
            IsLeapYear = false;
        }
    }

    public void LeapYearJulian()
    {
        if (CurrentYear % 4 == 0)
        {
            IsLeapYear = true;
        }
        else
        {
            IsLeapYear = false;
        }
    }

    public void AdjustForGregorian()
    {
        DayOfMonth = 15;
        DayOfYear = 288;
    }

    public void SetSeason()
    {
        if (CurrentMonth == Month.March || CurrentMonth == Month.April || CurrentMonth == Month.May)
        {
            CurrentSeason = Season.Spring;
        }

        if (CurrentMonth == Month.June || CurrentMonth == Month.July || CurrentMonth == Month.Augest)
        {
            CurrentSeason = Season.Summer;
        }

        if (CurrentMonth == Month.September || CurrentMonth == Month.October || CurrentMonth == Month.November)
        {
            CurrentSeason = Season.Fall;
        }

        if (CurrentMonth == Month.December || CurrentMonth == Month.January || CurrentMonth == Month.Febuary)
        {
            CurrentSeason = Season.Winter;
        }
    }
}