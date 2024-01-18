using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDial : MonoBehaviour
{
    private float lastRotation;
    private OptionsManager optionsManager;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        lastRotation = transform.rotation.eulerAngles.y;
        optionsManager = FindAnyObjectByType<OptionsManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.eulerAngles.y != lastRotation)
        {
            if (transform.rotation.eulerAngles.y < lastRotation)
            {
                optionsManager.SetVol(-0.5f);
            }
            else if (transform.rotation.eulerAngles.y > lastRotation)
            {
                optionsManager.SetVol(0.5f);
            }
            if (!audioSource.isPlaying) audioSource.Play();
        }        
        lastRotation = transform.rotation.eulerAngles.y;
    }
}
