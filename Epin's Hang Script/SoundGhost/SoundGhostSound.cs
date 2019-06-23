using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGhostSound : MonoBehaviour
{
    public List<AudioClip> audioClips;

    public void SoundSelect(int index)
    {
        this.GetComponent<AudioSource>().PlayOneShot(audioClips[index]);
    }

    public void RunSound()
    {
        this.gameObject.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[1]);
    }

    public void WalkSound()
    {
        this.gameObject.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[0]);
    }

    public void BellowSound()
    {
        this.gameObject.transform.GetComponent<AudioSource>().PlayOneShot(audioClips[2]);
    }
}
