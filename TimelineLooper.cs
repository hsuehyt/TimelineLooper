using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineLooper : MonoBehaviour
{
    public PlayableDirector director;

    [Header("Loop Control")]
    public bool loopEnabled = true;   // default ON

    [Header("Objects Affected By Loop Toggle")]
    public List<GameObject> toggleObjects = new List<GameObject>();

    double loopStart = -1;
    double loopEnd = -1;

    bool skipOnce = false;
    bool previousLoopEnabled; // to detect inspector changes


    void Start()
    {
        previousLoopEnabled = loopEnabled;
        ApplyToggleToObjects(loopEnabled);
    }


    void Update()
    {
        // SPACE toggles loop mode
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loopEnabled = !loopEnabled;

            // Apply toggle to the GameObjects
            ApplyToggleToObjects(loopEnabled);

            // if we turned looping OFF, skip current loop once
            if (!loopEnabled)
                skipOnce = true;
        }

        // Detect manual Inspector changes (runtime)
        if (loopEnabled != previousLoopEnabled)
        {
            previousLoopEnabled = loopEnabled;
            ApplyToggleToObjects(loopEnabled);

            if (!loopEnabled)
                skipOnce = true;
        }
    }


    // Called by loopStart signal
    public void SetLoopStart()
    {
        loopStart = director.time;
    }

    // Called by loopEnd signal
    public void OnLoopEnd()
    {
        loopEnd = director.time;

        // If looping OFF, skip once
        if (!loopEnabled)
        {
            if (skipOnce)
            {
                skipOnce = false;
                director.time = loopEnd + 0.01;
                director.Evaluate();
            }
            return;
        }

        // Looping ON ¡÷ normal loop back
        if (loopStart >= 0)
        {
            director.time = loopStart;
            director.Evaluate();
        }
    }


    // Enable/disable all assigned objects
    void ApplyToggleToObjects(bool state)
    {
        foreach (var go in toggleObjects)
        {
            if (go != null)
                go.SetActive(state);
        }
    }
}
