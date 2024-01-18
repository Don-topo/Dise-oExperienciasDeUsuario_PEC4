using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameInfo
{
    public float generalVolume;
    public float effectsVolume;
    public int selectedLanguage;
    public float lightTone;
    public int resolution;
    public bool subtitlesEnabled;
    public bool bloodEnabled;
    public float sensibility;

    public GameInfo(float newGeneralVolume, float newEffectsVolume, int newSelectedLanguage, float newLightTone, bool newSubtitlesEnabled, bool newBloodEnabled, int newResolution, float newSensibility)
    {
        generalVolume = newGeneralVolume;
        effectsVolume = newEffectsVolume;
        selectedLanguage = newSelectedLanguage;
        lightTone = newLightTone;
        subtitlesEnabled = newSubtitlesEnabled;
        bloodEnabled = newBloodEnabled;
        resolution = newResolution;
        sensibility = newSensibility;
    }
}
