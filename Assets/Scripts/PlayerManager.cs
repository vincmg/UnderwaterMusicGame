using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IRhythm
{
    public AudioClip TESTbeatSyncSfx;

    private Dictionary<string, bool> inputState;
    private AudioSource TESTbeatSyncSfxPlayer;

    // Use this for initialization
    void Start()
    {
        inputState = new Dictionary<string, bool>
        {
            {"left", true},
            {"right", true},
            {"up", true},
            {"down", true}
        };

        TESTbeatSyncSfxPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // check which keys are pressed down and set inputState
        // when OnSongBeat() is called, inputState will be read and appropriate forces and animations will play
        var keys = new List<string>(inputState.Keys);
        foreach (var keyName in keys)
        {
            inputState[keyName] = Input.GetKey(keyName);
        }
    }

    public void OnSongBeat()
    {
        bool didAnyActions = false;
        foreach (var keyName in inputState)
        {
            if (!keyName.Value) continue;

            DoAction(keyName.Key);
            didAnyActions = true;
        }

        if (didAnyActions)
        {
            TESTbeatSyncSfxPlayer.PlayOneShot(TESTbeatSyncSfx);
        }
    }

    private void DoAction(string keyName)
    {
        switch (keyName)
        {
            case "left":
                gameObject.transform.Translate(Vector3.left);
                break;
            case "right":
                gameObject.transform.Translate(Vector3.right);
                break;
            case "up":
                gameObject.transform.Translate(Vector3.up);
                break;
            case "down":
                gameObject.transform.Translate(Vector3.down);
                break;
            default:
                Debug.LogError("Unsupported keyName encountered! Check control mappings.");
                break;
        }
    }
}