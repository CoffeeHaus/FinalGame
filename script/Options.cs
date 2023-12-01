using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // Required for interacting with the UI elements
using TMPro;

public class Options : MonoBehaviour
{
    
    public Slider volumeMaster;
    public Slider volumeMusic; 
    public Slider volumeEffects;
    public TMP_Text master;
    public TMP_Text music;
    public TMP_Text effects;
    
    public Toggle fullscreenToggle;
    public AudioSource audioSource; // Reference to the audio source
    private static Options instance = null;

    void Awake()
    {

    }

    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        fullscreenToggle.onValueChanged.AddListener(SetFullScreenMode);

        volumeMaster.value = audioSource.volume;

        bool isFullScreen = PlayerPrefs.GetInt("FullScreenMode", 1) == 1;
        fullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    // Call this method when the slider's value is changed
    public void SetMasterVolume(float volume)
    {
        GameManager.Instance.SetVolume("MasterVolume",volume);
    }

    public void SetMusicVolume(float volume)
    {
        GameManager.Instance.SetVolume("MusicVolume",volume);
    }

    public void SetEffectsVolume(float volume)
    {
        GameManager.Instance.SetVolume("EffectsVolume",volume);
    }

        public void SetFullScreenMode(bool isFullScreen)
    {
        GameManager.Instance.SetFullScreenMode(isFullScreen);
    }
}
