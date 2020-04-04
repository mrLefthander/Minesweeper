using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum Type
    {
        RevealCell,
        RevealMineCell,
        FlagCell,
        GameWin,
        GameLose,
        ButtonClick,
        ToggleClick,
        MapReveal
    }

    public Sound.Type soundName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

}
