using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set;}

    [SerializeField] private AudioMixer audioMixer;
    private const string VolumeParameter = "Volume";
    private float volume = 0.05f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetVolume(float newVol)
    {
        volume = newVol;
        audioMixer.SetFloat(VolumeParameter, Mathf.Log10(volume) * 20);
    }

    public float GetVolume()
    {
        return volume;
    }
}
