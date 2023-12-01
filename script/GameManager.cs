using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const string MusicVolumeKey = "MusicVolume";
    private const string EffectsVolumeKey = "EffectsVolume";
    private const string MasterVolumeKey = "MasterVolume";
    public AudioMixer MusicMixer;
    public AudioMixer EffectsMixer;
    public AudioMixer MasterMixer;
    public static GameManager Instance { get; private set; }
    public int intMasterVolume;
    public int intMusicVolume;
    public int intEffectsVolume;
    public int score;
    public string playerName;
    public Toggle fullscreenToggle;
    public AudioSource audioSource;
    public GameObject Player;
    public int Score = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
        
        
        LoadVolumeSettings();
        //fullscreenToggle.isOn = Screen.fullScreen;

        //fullscreenToggle.onValueChanged.AddListener(SetFullScreenMode);


        bool isFullScreen = PlayerPrefs.GetInt("FullScreenMode", 1) == 1;
        //fullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // This keeps the GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }

    // Example method to update score
    public void UpdateScore(int points)
    {
        score += points;
        // You can implement other logic here, like updating UI
    }

    public void SetVolume(string mixerName, float volume)
    {
        switch (mixerName)
        {
            case "Music":
                MusicMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
                SaveVolumeSettings(MusicVolumeKey, volume);
                break;
            case "Effects":
                EffectsMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
                SaveVolumeSettings(EffectsVolumeKey, volume);
                break;
            case "Master":
                MasterMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
                SaveVolumeSettings(MasterVolumeKey, volume);
                break;
        }
    }

        private void SaveVolumeSettings(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
         float Volume;
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            Volume = PlayerPrefs.GetFloat(MusicVolumeKey);
            MusicMixer.SetFloat("MusicVolume", Mathf.Log10(Volume) * 20);
        }
        if (PlayerPrefs.HasKey(EffectsVolumeKey))
        {
            Volume = PlayerPrefs.GetFloat(EffectsVolumeKey);
            EffectsMixer.SetFloat("EffectsVolume", Mathf.Log10(Volume) * 20);
        }
        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            Volume = PlayerPrefs.GetFloat(MasterVolumeKey);
            MasterMixer.SetFloat("MasterVolume", Mathf.Log10(Volume) * 20);
        }
        
    }

    public void SetFullScreenMode(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreenMode", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    public void MusicToggle()
    {
        if(audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }


    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }

    public void addScore()
    {
        Score++; 
    }

}
