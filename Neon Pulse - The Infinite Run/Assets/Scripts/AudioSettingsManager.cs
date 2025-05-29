using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    [Header("Volum")]
    public Scrollbar volumeScrollbar;

    [Header("Mute")]
    public Toggle muteToggle;
    public Image muteIcon;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    [Header("Text de volum")]
    public TMP_Text volumeText;

    private float currentVolume = 1f;

    void Start()
    {
        // Recuperar preferències guardades
        currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        bool isMuted = PlayerPrefs.GetInt("IsMuted", 0) == 1;

        // Aplica el volum si no està mutejat
        AudioListener.volume = isMuted ? 0f : currentVolume;

        // Inicialitza la UI
        if (volumeScrollbar != null)
            volumeScrollbar.value = currentVolume;

        if (muteToggle != null)
            muteToggle.isOn = isMuted;

        UpdateIcon(isMuted);
        UpdateVolumeText(currentVolume, !isMuted);

        // Assignar listeners
        if (volumeScrollbar != null)
            volumeScrollbar.onValueChanged.AddListener(SetVolume);

        if (muteToggle != null)
            muteToggle.onValueChanged.AddListener(SetMute);
    }

    public void SetVolume(float value)
    {
        currentVolume = value;
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);

        // Aplica el volum si no està mutejat
        if (PlayerPrefs.GetInt("IsMuted", 0) == 0)
            AudioListener.volume = currentVolume;

        UpdateVolumeText(currentVolume, true);
    }

    public void SetMute(bool isMuted)
    {
        PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        AudioListener.volume = isMuted ? 0f : currentVolume;

        UpdateIcon(isMuted);
        UpdateVolumeText(currentVolume, !isMuted);
    }

    void UpdateIcon(bool isMuted)
    {
        if (muteIcon != null)
        {
            muteIcon.sprite = isMuted ? soundOffIcon : soundOnIcon;
        }
    }

    void UpdateVolumeText(float volume, bool isUnmuted)
    {
        if (volumeText != null)
        {
            if (isUnmuted)
                volumeText.text = $"Volume: {Mathf.RoundToInt(volume * 100)}%";
            else
                volumeText.text = "Volume: Muted";
        }
    }
}
