using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public static SoundManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    public void PlaySingle (AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
}
