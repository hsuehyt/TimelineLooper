using UnityEngine;
using UnityEngine.Playables;

public class TimelineLooper : MonoBehaviour
{
    public PlayableDirector director;

    [Header("Loop Control")]
    public bool loopEnabled = true;   // default ON

    double loopStart = -1;
    double loopEnd = -1;

    bool skipOnce = false;

    void Update()
    {
        // SPACE toggles looping ON/OFF
        if (Input.GetKeyDown(KeyCode.Space))
        {
            loopEnabled = !loopEnabled;

            // If we just turned OFF loop, skip the current loop once
            if (!loopEnabled)
                skipOnce = true;
        }
    }

    // ======================
    // Called by loopStart
    // ======================
    public void SetLoopStart()
    {
        loopStart = director.time;
    }

    // ======================
    // Called by loopEnd
    // ======================
    public void OnLoopEnd()
    {
        loopEnd = director.time;

        // If loop is OFF ¡÷ skip looping
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

        // If loop is ON ¡÷ loop normally
        if (loopStart >= 0)
        {
            director.time = loopStart;
            director.Evaluate();
        }
    }
}
