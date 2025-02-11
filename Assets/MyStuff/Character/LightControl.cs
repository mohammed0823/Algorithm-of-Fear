using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class LightControl : MonoBehaviour
{

    public Light flashLight;

    public int battery = 100;

    public Text text;

    static bool isOn = true;

    float onCount = 0;

   void Update()
   {

        text.text = battery.ToString();

        if(isOn)
        {
            onCount += Time.deltaTime;
            check();

        }

        if (battery > 0)
        {
        
            if(Input.GetKeyDown(KeyCode.Mouse0))
                toggleLight();

        }  

        else
        {
            if (isOn)
                flashLight.enabled = false;
        }  
   }


   void check()
   {
        if (onCount >= 20)
        {
            if (battery > 0)
            { 
                battery -= 1;
                onCount = 0;
            }
        }
   }

    void toggleLight()
    {
        flashLight.enabled = !flashLight.enabled;
        isOn = !isOn;
    }

}
