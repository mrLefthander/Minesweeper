using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreTable: MonoBehaviour
{
    private const string HIGHSCORE_CONTAINER_OBJECT_NAME = "highscoreEntryContainer";
    private const string HIGHSCORE_TEMPLATE_OBJECT_NAME = "highscoreEntry";
    private const string HIGHSCORE_TEMPLATE_RANK_TEXT_NAME = "rankText";
    private const string HIGHSCORE_TEMPLATE_SCORE_TEXT_NAME = "scoreText";
    private const string HIGHSCORE_TEMPLATE_NAME_TEXT_NAME = "nameText";

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private HighscoreHandler highscoreHandler;

    [SerializeField]
    private float templateHeight = 80f;

    private void Awake()
    {
        entryContainer = transform.Find(HIGHSCORE_CONTAINER_OBJECT_NAME);
        entryTemplate = entryContainer.Find(HIGHSCORE_TEMPLATE_OBJECT_NAME);

        highscoreHandler = FindObjectOfType<HighscoreHandler>();

        entryTemplate.gameObject.SetActive(false);

        FormHighscoreTable();
    }

    private void CreateHighscoreEntryTransform(HighscoreHandler.HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
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

    private void FormHighscoreTable()
    {
        List<HighscoreHandler.HighscoreEntry> highscoreEntryList = highscoreHandler.GetHighscoresList();

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreHandler.HighscoreEntry highscoreEntry in highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

}
