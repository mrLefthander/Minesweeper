using System;

public interface ILocalizationLoader
{
    event Action<string> OnDataLoaded;
    void Load(string fileName);
}
