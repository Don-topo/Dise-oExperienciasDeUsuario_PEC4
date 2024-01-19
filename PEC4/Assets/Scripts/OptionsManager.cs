using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider generalVolumeSlider;
    public Slider soundEffectsSlider;
    public AudioClip selectClip;
    public TextMeshProUGUI resolutionText;
    public Toggle subtitlesCheckBox;
    public Toggle bloodCheckbox;
    public Slider sensibilitySlider;
    private GameInfo gameInfo;
    private AudioSource audioSource;
    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private float currentSensibility;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 90;
        gameInfo = FileManager.LoadGameConfig();
        resolutions = Screen.resolutions;
        SetResolution();
        PrepareBoard();
        PrepareBlood();
        PrepareAudioMixers();
        SetAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVol(float amount)
    {
        if(generalVolumeSlider.value + amount > 0.1f && generalVolumeSlider.value + amount < 100f) 
        {
            generalVolumeSlider.value += amount;
            FileManager.SaveGameInfo(gameInfo);
        }        
    }

    public void SetLight(float amount)
    {
        if(gameInfo.lightTone + amount > 0.01f && gameInfo.lightTone + amount < 1.0f)
        {
            gameInfo.lightTone += amount;
            UpdateLights();
            FileManager.SaveGameInfo(gameInfo);
        }        
    }

    public void SelectLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        gameInfo.selectedLanguage = languageIndex;
        PlaySelectSound();
    }

    public void SetEffect(float amount)
    {
        if(soundEffectsSlider.value + amount > 0.1f && soundEffectsSlider.value + amount < 100f)
        {
            soundEffectsSlider.value += amount;
            FileManager.SaveGameInfo(gameInfo);
        }
    }

    public void Exit()
    {
        SaveData();
        Debug.Log("Application exit");
        Application.Quit();
    }

    public void Continue()
    {
        Debug.Log("Loading level");
        SceneManager.LoadScene(0);
    }

    public void SetGeneralVolume()
    {
        float db = 20 * Mathf.Log10(generalVolumeSlider.value / 100);
        audioMixer.SetFloat("General", db);
        gameInfo.generalVolume = generalVolumeSlider.value;
    }

    public void SetSoundEffects()
    {
        float db = 20 * Mathf.Log10(soundEffectsSlider.value / 100);
        audioMixer.SetFloat("Effects", db);
        gameInfo.effectsVolume = soundEffectsSlider.value;
    }

    public void SetSensibility()
    {
        currentSensibility = sensibilitySlider.value;
    }

    public void SetResolution()
    {
        Screen.SetResolution(resolutions[gameInfo.resolution].width, resolutions[gameInfo.resolution].height, true);
        UpdateResolutionText();
    }

    public void SelectLeftResolution()
    {
        if(currentResolutionIndex - 1 >= 0)
        {
            currentResolutionIndex--;
            UpdateResolutionText();
        }
        PlaySelectSound();
    }

    public void SelectRightResolution()
    {
        if(currentResolutionIndex + 1 < resolutions.Length)
        {
            currentResolutionIndex++;
            UpdateResolutionText();
        }
        PlaySelectSound();
    }

    public void UpdateResolutionText()
    {
        resolutionText.text = resolutions[currentResolutionIndex].width + " x " + resolutions[currentResolutionIndex].height;
    }

    public void ChangeSubtitles()
    {
        PlaySelectSound();
    }

    public void ChangeBlood()
    {
        PlaySelectSound();
        PrepareBlood();
    }

    public void SaveData()
    {
        PlaySelectSound();
        gameInfo.sensibility = currentSensibility;
        gameInfo.resolution = currentResolutionIndex;
        gameInfo.bloodEnabled = bloodCheckbox.isOn;
        gameInfo.subtitlesEnabled = subtitlesCheckBox.isOn;
        FileManager.SaveGameInfo(gameInfo);
        PrepareBlood();
        SetResolution();

    }

    public GameInfo GetGameInfo() 
    {
        if(gameInfo == null) gameInfo = FileManager.LoadGameConfig();
        return gameInfo;
    }
    

    private void PrepareAudioMixers()
    {
        generalVolumeSlider.maxValue = 100;
        generalVolumeSlider.minValue = 0;
        generalVolumeSlider.value = gameInfo.generalVolume;
        soundEffectsSlider.maxValue = 100;
        soundEffectsSlider.minValue = 0;
        soundEffectsSlider.value = gameInfo.effectsVolume;
    }

    private void SetAudio()
    {
        float db = 20 * Mathf.Log10(gameInfo.generalVolume / 100);
        audioMixer.SetFloat("General", db);
        db = 20 * Mathf.Log10(generalVolumeSlider.value / 100);
        audioMixer.SetFloat("Effects", db);
    }

    private void PlaySelectSound()
    {
        audioSource.clip = selectClip;
        audioSource.Play();
    }

    private void PrepareBoard()
    {
        currentResolutionIndex = gameInfo.resolution;
        UpdateResolutionText();
        subtitlesCheckBox.isOn = gameInfo.subtitlesEnabled;
        bloodCheckbox.isOn = gameInfo.bloodEnabled;        
    }

    private void PrepareBlood()
    {
        GameObject[] bloods = GameObject.FindGameObjectsWithTag("Blood");
        foreach(GameObject blood in bloods)
        {
            blood.GetComponent<MeshRenderer>().enabled = bloodCheckbox.isOn;
        }
    }

    private void UpdateLights()
    {
        Light[] lights = FindObjectsOfType<Light>();
        foreach(Light l in lights)
        {
            l.intensity = gameInfo.lightTone;
        }
    }
}
