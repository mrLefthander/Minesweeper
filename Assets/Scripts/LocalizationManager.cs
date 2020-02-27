using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public enum Language
    {
        English,
        Russian,
        Ukrainian
    }

    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;

    private void Awake()
    {
        SetUpSingleton();
    }

    public void LoadLocalizedText(Language language)
    {
        localizedText = new Dictionary<string, string>();
        string fileName = GetFileName(language);
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for(int i = 0; i < loadedData.localizationItems.Length; i++)
            {
                localizedText.Add(loadedData.localizationItems[i].key, loadedData.localizationItems[i].value);
            }
            Debug.Log("Loaded " + localizedText.Count);
        }
        else
        {
            Debug.LogWarning("Cannot find " + fileName + " localization file");
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        return "missing localized text for: " + key;
    }

    private string GetFileName(Language language)
    {
        switch (language)
        {
            default:
            case Language.English:
                return "en_US.json";
            case Language.Russian:
                return "ru_RU.json";
            case Language.Ukrainian:
                return "uk_UA.json";
        }
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
