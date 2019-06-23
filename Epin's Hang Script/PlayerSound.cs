using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    static private PlayerSound instance = null;
    static public PlayerSound GetInstance
    {
        get
        {
            return instance;
        }
    }
    
    public List<AudioClip> audioClips;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this);
            return;
        }

        instance = this;
    }

    public void SoundSelect(int index)
    {
        this.GetComponent<AudioSource>().PlayOneShot(audioClips[index]);
    }
}
