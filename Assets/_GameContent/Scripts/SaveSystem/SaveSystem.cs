using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
    public static Dictionary<string, string> dts;

    private const string PREF = "DTS";

    public static void SaveInt(string key, int count)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            dts.Add(key, count.ToString());
        else
            dts[key] = count.ToString();
        SaveDTS();
    }
    public static void SaveFloat(string key, float count)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            dts.Add(key, count.ToString());
        else
            dts[key] = count.ToString();
        SaveDTS();
    }

    public static void Save(string key, string value)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            dts.Add(key, value);
        else
            dts[key] = value;
        SaveDTS();
    }

    public static float LoadFloat(string key)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            return 0.0f;
        else
            return float.Parse(dts[key]);
    }
    public static int LoadInt(string key)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            return 0;
        else
            return int.Parse(dts[key]);
    }

    public static string Load(string key)
    {
        if (dts == null)
            LoadDTS();

        if (!dts.ContainsKey(key))
            return null;
        else
            return dts[key];
    }

    public static bool HasKey(string key)
    {
        if (dts == null)
            LoadDTS();

        return dts.ContainsKey(key);
    }

    public static void DeleteKey(string key)
    {
        if (dts == null)
            LoadDTS();

        if (dts.ContainsKey(key))
        {
            bool removed = dts.Remove(key);
        }
        else
        {
            Debug.LogError("Key not found: " + key);
        }
    }

    private static string DTSToString()
    {
        return string.Join("||", dts.Select(kv => $"{kv.Key}::{kv.Value}"));
    }

    private static Dictionary<string, string> StringToDTS(string dtsString)
    {
        Dictionary<string, string> parsedDictionary = new Dictionary<string, string>();
        HashSet<string> uniqueKeys = new HashSet<string>();

        string[] pairs = dtsString.Split(new[] { "||" }, StringSplitOptions.None);
        foreach (string pair in pairs)
        {
            string[] parts = pair.Split(new[] { "::" }, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();

                if (!uniqueKeys.Contains(key))
                {
                    uniqueKeys.Add(key);
                    parsedDictionary[key] = value;
                }
                else
                {
                    Debug.LogWarning($"Duplicate key found: {key}. Ignoring this key.");
                }
            }
        }

        return parsedDictionary;
    }

    private static void LoadDTS()
    {
        string pref = "";
        //if (PlayerPrefs.HasKey(PREF))
        //{
        //    pref = PlayerPrefs.GetString(PREF);
        //    dts = StringToDTS(pref);
        //}
        //else
        //{
        //    dts = new Dictionary<string, string>();
        //}
        if (YG2.saves != null && !string.IsNullOrEmpty(YG2.saves.DataToSave))
        {
            pref = YG2.saves.DataToSave;
            dts = StringToDTS(pref);
        }
        else
        {
            dts = new Dictionary<string, string>();
        }
    }

    private static void SaveDTS()
    {
        string pref = DTSToString();
        //PlayerPrefs.SetString(PREF, pref);

        YG2.saves.DataToSave = pref;
        YG2.SaveProgress();
    }
}

namespace YG
{
    public partial class SavesYG
    {
        public string DataToSave;
    }
}