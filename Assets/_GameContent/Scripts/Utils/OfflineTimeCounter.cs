using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class OfflineTimeCounter : MonoBehaviour
{
    [SerializeField] private string pref;
    private DateTime lastQuitTime;

    public void LoadTime()
    {
        if (SaveSystem.HasKey(pref))
        {
            string lastQuitTimeString = SaveSystem.Load(pref);
            lastQuitTime = Convert.ToDateTime(lastQuitTimeString);
        }
        else
        {
            lastQuitTime = DateTime.Now;
        }
    }

    public bool IsHasLastQuitTime()
    {
        if (SaveSystem.HasKey(pref))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SaveTime()
    {
        string quitTimeString = DateTime.Now.ToString();
        SaveSystem.Save(pref, quitTimeString);
    }
    public long CalculateInactiveMinutes()
    {
        LoadTime();
        TimeSpan inactiveTime = DateTime.Now - lastQuitTime;
        long offlineMinutes = (long)inactiveTime.TotalMinutes;
        return offlineMinutes;
    }
    public long CalculateInactiveSeconds()
    {
        LoadTime();
        TimeSpan inactiveTime = DateTime.Now - lastQuitTime;
        long offlineSeconds = (long)inactiveTime.TotalSeconds;
        return offlineSeconds;
    }
    public long CalculateInactiveHours()
    {
        LoadTime();
        TimeSpan inactiveTime = DateTime.Now - lastQuitTime;
        long offlineSeconds = (long)inactiveTime.TotalHours;
        return offlineSeconds;
    }
    public int CurrentWeek()
    {
        System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;

        // Получаем номер недели
        int weekNumber = culture.Calendar.GetWeekOfYear(DateTime.Now, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        return weekNumber;
    }
}
