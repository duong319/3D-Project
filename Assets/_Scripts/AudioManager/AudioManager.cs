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
}
