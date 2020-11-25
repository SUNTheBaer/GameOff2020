using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] universalSounds = null;
    [SerializeField] private LevelSounds levelSounds = null;

    private void Awake()
    {
        foreach (Sound s in universalSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in levelSounds.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(universalSounds, sound => sound.name == name);
        if (s == null)
            s = Array.Find(levelSounds.sounds, sound => sound.name == name);
        if (s == null)
            Debug.LogWarning("Sound not found");

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(universalSounds, sound => sound.name == name);
        if (s == null)
            s = Array.Find(levelSounds.sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Stop();
    }
}
