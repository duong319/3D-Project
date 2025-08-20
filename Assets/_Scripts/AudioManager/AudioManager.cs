using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        public bool loop;
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> soundDictionary = new Dictionary<string, AudioSource>();
    private bool isMuted = false;
    private void Awake()
    {

        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.loop = s.loop;
            soundDictionary[s.name] = source;
        }
        isMuted = PlayerPrefs.GetInt("AudioMuted", 0) == 1;
        ApplyMuteState();
    }

    public void Start()
    {
        Play("MenuBG");
    }

    public void Play(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            soundDictionary[name].Play();
        }
        else
        {
            Debug.LogWarning("Audio: " + name + " not found!");
        }
    }

    public void Stop(string name)
    {
        if (soundDictionary.ContainsKey(name))
        {
            soundDictionary[name].Stop();
        }
    }

    public void SetVolume(string name, float volume)
    {
        if (soundDictionary.ContainsKey(name))
        {
            soundDictionary[name].volume = volume;
        }
    }
    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AudioMuted", isMuted ? 1 : 0);
        ApplyMuteState();
    }

    private void ApplyMuteState()
    {
        foreach (var source in soundDictionary.Values)
        {
            source.mute = isMuted;
        }
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
