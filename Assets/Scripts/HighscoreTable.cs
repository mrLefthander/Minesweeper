using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable: MonoBehaviour
{
    private const string HIGHSCORE_PLAYERPREFS_KEY = "highscoreTable";
    private const string HIGHSCORE_CONTAINER_OBJECT_NAME = "highscoreEntryContainer";
    private const string HIGHSCORE_TEMPLATE_OBJECT_NAME = "highscoreEntry";
    private const string HIGHSCORE_TEMPLATE_RANK_TEXT_NAME = "rankText";
    private const string HIGHSCORE_TEMPLATE_SCORE_TEXT_NAME = "scoreText";
    private const string HIGHSCORE_TEMPLATE_NAME_TEXT_NAME = "nameText";
    private const string HIGHSCORE_TEMPLATE_BACKGOUND_NAME = "highscoreEntryBackground";
    private const string HIGHSCORE_TEMPLATE_TROPHY_NAME = "trophy";

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find(HIGHSCORE_CONTAINER_OBJECT_NAME);
        entryTemplate = entryContainer.Find(HIGHSCORE_TEMPLATE_OBJECT_NAME);

        entryTemplate.gameObject.SetActive(false);
                
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Sort();
        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
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
        Transform backgroundTransform = entryTransform.Find(HIGHSCORE_TEMPLATE_BACKGOUND_NAME);
        Transform trophyTransform = entryTransform.Find(HIGHSCORE_TEMPLATE_TROPHY_NAME);
        


        int rank = transformList.Count + 1;

        rankTransform.GetComponent<Text>().text = rank.ToString();
        scoreTransform.GetComponent<Text>().text = highscoreEntry.score.ToString();
        nameTransform.GetComponent<Text>().text = highscoreEntry.name.ToUpper();
        //backgroundTransform.gameObject.SetActive(rank % 2 == 1);

        if(rank == 1)
        {
            //trophyTransform.gameObject.SetActive(true);
            rankTransform.GetComponent<Text>().fontStyle = FontStyle.Bold;
            rankTransform.GetComponent<Text>().color = Color.white;

            scoreTransform.GetComponent<Text>().fontStyle = FontStyle.Bold;
            scoreTransform.GetComponent<Text>().color = Color.white;

            nameTransform.GetComponent<Text>().fontStyle = FontStyle.Bold;
            nameTransform.GetComponent<Text>().color = Color.white;
        }

/*        switch (rank)
        {
            default:
                trophyTransform.gameObject.SetActive(false);
                break;
            case 1:
                trophyTransform.GetComponent<Image>().color = new Color32(212, 175, 55, 255);
                break;
            case 2:
                trophyTransform.GetComponent<Image>().color = new Color32(169, 169, 169, 255);
                break;
            case 3:
                trophyTransform.GetComponent<Image>().color = new Color32(205, 127, 50, 255);
                break;
        }*/

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString(HIGHSCORE_PLAYERPREFS_KEY);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString(HIGHSCORE_PLAYERPREFS_KEY, json);
        PlayerPrefs.Save();
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
