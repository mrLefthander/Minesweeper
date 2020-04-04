[System.Serializable]
public class LocalizationData
{
    public LicalizationItem[] localizationItems;
}

[System.Serializable]
public class LicalizationItem
{
    public string key;
    public string value;
}
