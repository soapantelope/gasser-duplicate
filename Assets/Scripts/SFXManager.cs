using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    private Dictionary<string, AudioSource> sounds;
    public AudioMixer mixer;

    void Awake()
    {
        sounds = new Dictionary<string, AudioSource>();

        foreach (AudioSource sound in gameObject.GetComponents<AudioSource>()) {
            sounds.Add(sound.clip.name, sound);
        }
    }

    public void play(string name) {
        sounds[name].Play();
    }

    public void stop(string name) {
        sounds[name].Pause();
    }

    public void changeVolume(Slider slider) {
        mixer.SetFloat("master", slider.value);
    }
}
