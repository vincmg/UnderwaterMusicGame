using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour {
    public double bpm;

    private List<IRhythm> rhythmObjects;
    private double interBeatTimeInSeconds;
    private double nextBeatTime;   

    // Use this for initialization
    void Start () {
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
        nextBeatTime = Time.fixedTime;
    }
    
    // Update is called once per frame
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
