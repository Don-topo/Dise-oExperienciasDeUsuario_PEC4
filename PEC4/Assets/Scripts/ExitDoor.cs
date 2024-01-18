using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private OptionsManager optionsManager;

    // Start is called before the first frame update
    void Start()
    {
        optionsManager = FindAnyObjectByType<OptionsManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            optionsManager.Exit();
        }
    }
}
