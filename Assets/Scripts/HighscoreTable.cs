using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreTable: MonoBehaviour
{
    private const string HIGHSCORE_PLAYERPREFS_KEY = "highscoreTable";
    private const string HIGHSCORE_CONTAINER_OBJECT_NAME = "highscoreEntryContainer";
    private const string HIGHSCORE_TEMPLATE_OBJECT_NAME = "highscoreEntry";
    private const string HIGHSCORE_TEMPLATE_RANK_TEXT_NAME = "rankText";
    private const string HIGHSCORE_TEMPLATE_SCORE_TEXT_NAME = "scoreText";
    private const string HIGHSCORE_TEMPLATE_NAME_TEXT_NAME = "nameText";

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find(HIGHSCORE_CONTAINER_OBJECT_NAME);
        entryTemplate = entryContainer.Find(HIGHSCORE_TEMPLATE_OBJECT_NAME);

        entryTemplate.gameObject.SetActive(false);

        FormHighscoreTable();
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 35f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entrryRectTransform = entryTransform.GetComponent<RectTransform>();
        entrryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        Transform rankTransform = entryTransform.Find(HIGHSCORE_TEMPLATE_RANK_TEXT_NAME);
        Transform scoreTransform = entryTransform.Find(HIGHSCORE_TEMPLATE_SCORE_TEXT_NAME);
        Transform nameTransform = entryTransform.Find(HIGHSCORE_TEMPLATE_NAME_TEXT_NAME);

        int rank = transformList.Count + 1;

        rankTransform.GetComponent<TMP_Text>().text = rank.ToString();
        scoreTransform.GetComponent<TMP_Text>().text = highscoreEntry.score.ToString();
        nameTransform.GetComponent<TMP_Text>().text = highscoreEntry.name.ToUpper();

        if(rank == 1)
        {
            Color leaderColor = new Color32(100, 255, 218, 255);
            rankTransform.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            rankTransform.GetComponent<TMP_Text>().color = leaderColor;

            scoreTransform.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            scoreTransform.GetComponent<TMP_Text>().color = leaderColor;

            nameTransform.GetComponent<TMP_Text>().fontStyle = FontStyles.Bold;
            nameTransform.GetComponent<TMP_Text>().color = leaderColor;
        }

        transformList.Add(entryTransform);
    }

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
        Debug.Log(numOfHighscores);
        if (numOfHighscores > 10)
        {
            highscores.highscoreEntryList.RemoveRange(9, numOfHighscores - 10);
        }

        SaveHighscores(highscores);
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

    private void FormHighscoreTable()
    {
        Highscores highscores = GetHighscores();

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    [Serializable]
    private class HighscoreEntry : IComparable<HighscoreEntry>
    {
        public int score;
        public string name;

        public int CompareTo(HighscoreEntry other)
        {
            return this.score.CompareTo(other.score);
        }
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
}
