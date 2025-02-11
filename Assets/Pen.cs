using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour, Interactable
{
    public void Interact(GameObject obj)
    {
        PlayerStats.hasPen = true;
        Destroy(obj);
    }
}
