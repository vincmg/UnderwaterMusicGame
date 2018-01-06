using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public double bpm;
    public MusicPlayer musicPlayer;

    private List<IRhythm> rhythmObjects;
    private double interBeatTimeInSeconds;
    private double nextBeatTime;

    private bool startedMusic;

    // Use this for initialization
    void Start()
    {
        musicPlayer = FindObjectOfType(typeof(MusicPlayer)) as MusicPlayer;
        if (musicPlayer == null)
        {
            Debug.LogError("couldn't find music player");
            // TODO: crash or something here
        }

        startedMusic = false;

        rhythmObjects = new List<IRhythm>();

        // find all objects in scene that implement IRhythm
        var gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        if (gameObjects == null)
        {
            Debug.Log("Did not find any IRhythm objects in the scene!");
        }

        foreach (var obj in gameObjects)
        {
            var rhythmScript = obj.GetComponent(typeof(IRhythm)) as IRhythm;
            if (rhythmScript == null) return;
            
            rhythmObjects.Add(rhythmScript);
            Debug.Log("Metronome.rhythmObjects: added " + obj.name);
        }

        interBeatTimeInSeconds = 60.0f / bpm;
        nextBeatTime = AudioSettings.dspTime + interBeatTimeInSeconds;
    }

    void Update()
    {
        if (startedMusic) return;

        startedMusic = true;
        nextBeatTime = AudioSettings.dspTime;
        // TODO: use a Unity Asset for synchronizing music instead of doing it manually
        musicPlayer.StartMusic();
    }

    void FixedUpdate()
    {
        double currentTime = AudioSettings.dspTime;
        if (currentTime >= nextBeatTime)
        {
            Beat();
            nextBeatTime += interBeatTimeInSeconds;
        }
    }

    void Beat()
    {
        //Debug.Log(string.Format("called beat at {0}", AudioSettings.dspTime));
        foreach (var obj in rhythmObjects)
        {
            obj.OnSongBeat();
        }
    }
}