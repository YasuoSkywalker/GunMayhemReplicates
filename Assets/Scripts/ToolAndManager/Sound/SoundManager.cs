using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<Sound> soundList;
    public AudioSource audioSource;

    public void playAudio(string name)
    {
        Sound temp = soundList.Find(x=>x.name == name);
        audioSource.clip = temp.audioClip;
        audioSource.Play();
    }

    public void playRandom()
    {
        Sound temp = soundList[Random.Range(0, soundList.Count)];
        audioSource.clip = temp.audioClip;
        audioSource.Play();
    }
}
