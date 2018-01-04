using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    public double bpm;
    public MusicPlayer musicPlayer;

    private List<IRhythm> rhythmObjects;
    private double interBeatTimeInSeconds;
    private double nextBeatTime;   

    private bool startedMusic;

    // Use this for initialization
    void Start () {
        musicPlayer = FindObjectOfType(typeof(MusicPlayer)) as MusicPlayer;
        if (musicPlayer == null) {
            Debug.Log("couldn't find music player");
            // TODO: crash or something here
        }
        startedMusic = false;

        rhythmObjects = new List<IRhythm>();

        // find all objects in scene that implement IRhythm
        var gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var obj in gameObjects) {
            var rhythmScript = obj.GetComponent(typeof(IRhythm)) as IRhythm;
            if (rhythmScript != null) {
                rhythmObjects.Add(rhythmScript);
                Debug.Log("added object");
            }
        }

        interBeatTimeInSeconds = 60.0f / bpm;
        nextBeatTime = Time.fixedTime + interBeatTimeInSeconds;
    }
    
    void Update () {
        if (!startedMusic) {
            musicPlayer.StartMusic();
            startedMusic = true;
            nextBeatTime = Time.fixedTime + interBeatTimeInSeconds;
        }
    }
    void FixedUpdate () {

        double currentTime = Time.fixedTime;
        if (currentTime > nextBeatTime) {

            Beat();
            nextBeatTime += interBeatTimeInSeconds;
        }
    }

    void Beat () {
        Debug.Log(string.Format("called beat at {0}", Time.fixedTime));
        foreach (var obj in rhythmObjects) {
            obj.OnSongBeat();
        }
    }
}
