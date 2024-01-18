using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{

    private Light l;
    private OptionsManager optionsManager;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
        optionsManager = FindObjectOfType<OptionsManager>();
        l.intensity = optionsManager.GetGameInfo().lightTone;
    }
}
