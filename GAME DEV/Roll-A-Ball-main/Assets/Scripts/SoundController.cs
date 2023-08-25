using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }

    void PlaySound(AudioClip _newSound)
    {
        //Setting the audio clip source to be passed in sound
        audioSource.clip = _newSound;
        //To play the audio source clip
        audioSource.Play();
    }

    public void PlayCollisionSound(GameObject _go)
    {
        //Failsafe for if audio is not attached to wall object.
        if (_go.GetComponent<AudioSource>() != null)
        {
            //pLay audio on the wall object
            _go.GetComponent<AudioSource>().Play();
        }
    }
}
