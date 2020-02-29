using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalizationManager : MonoBehaviour
{
    public enum Language
    {
        English,
        Russian,
        Ukrainian
    }

    public static LocalizationManager instance;
    public Language currentLanguage;

    private Dictionary<string, string> localizedText;

    private void Awake()
    {
        SetUpSingleton();
        currentLanguage = SettingsPlayerPrefsManager.GetSavedLanguage();
        LoadLocalizedText(currentLanguage);
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
        }
        else
        {
            Debug.LogWarning("Cannot find " + fileName + " localization file");
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (!localizedText.ContainsKey(key))
        {
            return "missing localized text for: " + key;
        }
        return localizedText[key];
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
