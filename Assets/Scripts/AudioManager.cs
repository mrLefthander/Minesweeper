using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private const string AUDIOMIXER_EXPOSED_VAR_NAME = "volume";

    [SerializeField] private AudioMixer mainMixer;
    public float volume;
#if UNITY_ANDROID
    public bool isVibrating;
#endif
    public Sound[] sounds;


    private AudioSource audioSource;

    private void Awake()
    {
        SetUpSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetVolume(SettingsPlayerPrefsManager.GetSavedVolume());
#if UNITY_ANDROID
        isVibrating = SettingsPlayerPrefsManager.GetSavedIsVibrating();
#endif
    }

    public void PlaySound(Sound.Type soundName)
    {
        Sound sound = GetSoundByName(soundName);
        if (sound == null)
        {
            Debug.LogWarning("Could not find Sound " + soundName.ToString());
            return;
        }

        audioSource.PlayOneShot(sound.clip);
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
    }

    private Sound GetSoundByName(Sound.Type soundName)
    {
        return Array.Find(sounds, s => s.soundName == soundName);
    }


    public void PlayRevealSound(MapGridObject.Type gridObjectType)
    {
        switch (gridObjectType)
        {
            default:
                PlaySound(Sound.Type.RevealCell);
                break;
            case MapGridObject.Type.Mine:
                PlaySound(Sound.Type.RevealMineCell);
                break;
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

    public void SetVolume(float sliderValue)
    {
        volume = sliderValue;
        mainMixer.SetFloat(AUDIOMIXER_EXPOSED_VAR_NAME, Mathf.Log10(volume) * 20);
    }

}
