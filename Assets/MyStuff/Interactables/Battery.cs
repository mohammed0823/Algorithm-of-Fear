using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Battery : MonoBehaviour, Interactable
{

    public int percentage;

    public GameObject flashlight;

     public void Interact(GameObject obj)
     {
        LightControl lightcontrol = flashlight.GetComponent<LightControl>();

        lightcontrol.battery += percentage;

        if (lightcontrol.battery > 100)
            lightcontrol.battery = 100;

        Destroy(obj);
     }

}