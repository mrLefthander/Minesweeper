using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour
{
    private const string HIGHSCORE_PLAYERPREFS_KEY = "highscoreTable";

    public bool IsHighscore(int score)
    {
        Highscores highscores = GetHighscores();
        int numOfHighscores = highscores.highscoreEntryList.Count;
        if (numOfHighscores <= 10 || score < highscores.highscoreEntryList[numOfHighscores - 1].score)
        {
            return true;
        }
        return false;
    }

    public void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        Highscores highscores = GetHighscores();

        highscores.highscoreEntryList.Add(highscoreEntry);
        highscores.highscoreEntryList.Sort();

        int numOfHighscores = highscores.highscoreEntryList.Count;
        if (numOfHighscores > 10)
        {
            highscores.highscoreEntryList.RemoveRange(10, numOfHighscores - 10);
        }

        SaveHighscores(highscores);
    }

    public void ResetHighscores()
    {
        SaveHighscores(new Highscores());
    }

    private void SaveHighscores(Highscores highscores)
    {
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(HIGHSCORE_PLAYERPREFS_KEY, json);
        PlayerPrefs.Save();
    }

    private Highscores GetHighscores()
    {
        string jsonString = PlayerPrefs.GetString(HIGHSCORE_PLAYERPREFS_KEY);
        return JsonUtility.FromJson<Highscores>(jsonString);
    }

    public List<HighscoreEntry> GetHighscoresList()
    {
        return GetHighscores().highscoreEntryList;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [Serializable]
    public class HighscoreEntry: IComparable<HighscoreEntry>
    {
        public int score;
        public string name;

        public int CompareTo(HighscoreEntry other)
        {
            return this.score.CompareTo(other.score);
        }
    }
}
