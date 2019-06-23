using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGhostAudioRandPlay : MonoBehaviour
{

    public List<AudioClip> audioClips;
    AudioSource patrolAudio;

    int index = 0;
    float delayTime = 0;

    private void Awake()
    {
        patrolAudio = this.gameObject.transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!patrolAudio.isPlaying)
        {
            delayTime += Time.deltaTime;
            if (delayTime > 15)
            {
                if (index == audioClips.Count) index = 0;
                patrolAudio.PlayOneShot(audioClips[index]);
                index++;
                delayTime = 0;
            }
        }
    }

    public void Die()
    {
        patrolAudio.PlayOneShot(audioClips[6]);
    }
}
