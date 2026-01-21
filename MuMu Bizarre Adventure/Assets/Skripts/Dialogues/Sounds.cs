using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    public SoundArrays[] randsound;


    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(int i, float volume = 1f, bool random = false, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f )
    {
        AudioClip clip = random ? randsound[i].soundArray[Random.Range(0, randsound[i].soundArray.Length)] : sounds[i];
        audioSrc.pitch = Random.Range(p1, p2);

        if (destroyed)
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        else
            audioSrc.PlayOneShot(clip, volume);
    }

    public void StopMusic()
    {
        audioSrc.Stop();
    }

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }

    public void SpeeedSounds(float sped)
    {
        audioSrc.pitch = sped;
    }
}
