using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject); 
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Studio Warehouse")
        {
            Play("IncompleteBGM2");
        }
        else if (SceneManager.GetActiveScene().name == "Kimmie's House")
        {
            Play("IncompleteBGM1");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                Debug.Log("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        s.source.UnPause();
    }
    //This code is from when I tested each function
    /*public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Play("IncompleteBGM2");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            Stop("IncompleteBGM2");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Pause("IncompleteBGM2");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UnPause("IncompleteBGM2");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StopAll();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Play("IncompleteBGM1");
        }
    }*/
}
