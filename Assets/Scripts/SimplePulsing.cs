using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePulsing : MonoBehaviour, IRhythm {
    public float testPulseSizeLimit = 2;

    private Vector3 scaleLimit;
    private Vector3 scaleInterval;
    private Vector3 startingScale;

    // Use this for initialization
    void Start () {
        scaleLimit = new Vector3(testPulseSizeLimit, testPulseSizeLimit, testPulseSizeLimit);
        scaleInterval = new Vector3(0.1f, 0.1f, 0.1f);
        startingScale = transform.localScale;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void OnSongBeat () {
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse () {
        yield return Expand ();
        yield return Contract ();
    }

    private IEnumerator Expand () {
        while (transform.localScale != scaleLimit) {
            transform.localScale += scaleInterval;
            yield return new WaitForSeconds(0.01F);
        }
    }

    private IEnumerator Contract () {
        while (transform.localScale != startingScale) {
            transform.localScale -= scaleInterval;
            yield return new WaitForSeconds(0.01F);
        }
    }
}
