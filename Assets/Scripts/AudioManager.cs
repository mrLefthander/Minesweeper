using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    private AudioSource audioSource;

    private void Awake()
    {
        SetUpSingleton();
        audioSource = GetComponent<AudioSource>();
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

}
