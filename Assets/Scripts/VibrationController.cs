using UnityEngine;
using UnityEngine.UI;

public class VibrationController : MonoBehaviour
{
#if UNITY_ANDROID
    private Toggle muteToggle;

    void Start()
    {
        muteToggle = GetComponentInChildren<Toggle>();
        muteToggle.onValueChanged.AddListener(OnMuteToggleChange);
        SetToggleValue();
    }

    private void SetToggleValue()
    {
        muteToggle.isOn = AudioManager.instance.isVibrating;
    }

    public void OnMuteToggleChange(bool isOn)
    {
        AudioManager.instance.isVibrating = muteToggle.isOn;
        AudioManager.instance.PlaySound(Sound.Type.ToggleClick);
    }
#endif
}
