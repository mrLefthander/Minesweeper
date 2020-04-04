using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class LocalizationManager: MonoBehaviour
{
    public event Action OnLanguageChange;
    public event Action OnDataLoaded;
    public enum Language
    {
        English,
        Russian,
        Ukrainian
    }

    public static LocalizationManager instance = null;

    public ILocalizationLoader localizationLoader;
    private Dictionary<string, string> localizedText;

    private Language currentLanguage;
    public Language CurrentLanguage
    {
        get { return currentLanguage; }
        set
        {
            if (currentLanguage == value) return;

            currentLanguage = value;

            OnLanguageChange?.Invoke();
        }
    }

    private void Awake()
    {
        SetUpSingleton();

        localizedText = new Dictionary<string, string>();

        CurrentLanguage = SettingsPlayerPrefsManager.GetSavedLanguage();

        GetLocalizationLoader();

        SubcribeOnEvents();

        if (gameObject.activeSelf) LoadLocalizedText();
    }

    private void SubcribeOnEvents()
    {
        localizationLoader.OnDataLoaded += (data) =>
        {
            PopulateLocalizedText(data);
            OnDataLoaded?.Invoke();
        };

        OnLanguageChange += () => LoadLocalizedText();
    }

    public string GetLocalizedValue(string key)
    {
        if (!localizedText.ContainsKey(key))
        {
            return "missing localized text for: " + key;
        }
        return localizedText[key];
    }

    private void LoadLocalizedText() => localizationLoader.Load(GetFileName(CurrentLanguage));

    private void PopulateLocalizedText(string dataAsJson)
    {
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        Dictionary<string, string> localizedText = new Dictionary<string, string>();
        for (int i = 0; i < loadedData.localizationItems.Length; i++)
        {
            localizedText.Add(loadedData.localizationItems[i].key, loadedData.localizationItems[i].value);
        }
        this.localizedText = localizedText;
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

    private void GetLocalizationLoader()
    {
        GetDesktopLocalozationLoader();
        GetWebGLLocalozationLoader();
    }

    [Conditional("UNITY_STANDALONE")]
    private void GetDesktopLocalozationLoader() => localizationLoader = GetComponent<LocalizationLoaderDesktop>();

    [Conditional("UNITY_WEBGL"), Conditional("UNITY_ANDROID")]
    private void GetWebGLLocalozationLoader() => localizationLoader = GetComponent<LocalizationLoaderWebGL>();

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
