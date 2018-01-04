using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : MonoBehaviour, IRhythm {
    private bool flip;

	// Use this for initialization
	void Start () {
        flip = false;
		Debug.Log("testsphere: called Start()");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnSongBeat () {
        if (flip) {
            transform.Translate(Vector3.forward);
        } else {
            transform.Translate(Vector3.back);
        }

        flip = !flip;
    }
}
