using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : MonoBehaviour, IRhythm {

	// Use this for initialization
	void Start () {
		Debug.Log("testsphere: called Start()");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSongBeat () {
    }
}
