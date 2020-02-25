﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    private AudioSource[] source;

    public enum AudioState
    {
        Play,
        Pause,
        Resume,
        Stop,
    }

    private void Awake()
    {
        source = GetComponents<AudioSource>();
        if (source == null)
            Debug.LogError("No audio source found on: " + gameObject.name);

        if (sounds.Length > 0)
        {
            foreach (Sound s in sounds)
            {
                //s.source = gameObject.AddComponent<AudioSource>();
                //s.source.clip = s.clip;
                //s.source.volume = s.volume;
                //s.source.pitch = s.pitch;
                //s.source.loop = s.loop;

                //if (s.sfx)
                //    sfxSounds.Add(s);
                //else
                //    musicSounds.Add(s);

                if (s.playOnAwake)
                    Play(s.name);
            }
        }
    }

    public void SetAudio(string name, AudioState state, int index = 0)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        if (s == null)
        {
            Debug.LogError("[Audio] " + name + " sound does not exist!");
            return;
        }
        source[index].clip = s.clip;
        source[index].clip = s.clip;
        source[index].volume = s.volume;
        source[index].pitch = s.pitch;
        source[index].loop = s.loop;
        switch (state)
        {
            case AudioState.Play:
                source[index].Play();
                break;

            case AudioState.Pause:
                source[index].Pause();
                break;

            case AudioState.Resume:
                source[index].UnPause();
                break;

            case AudioState.Stop:
                source[index].Stop();
                break;
        }
    }

    public void Play(string name, int index = 0)
    {
        SetAudio(name, AudioState.Play, index);
    }
}