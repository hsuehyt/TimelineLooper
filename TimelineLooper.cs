using UnityEngine;
using UnityEngine.Playables;

public class TimelineLooper : MonoBehaviour
{
    public PlayableDirector director;

    double loopStart = -1;
    double loopEnd = -1;
    bool skipLoop = false;

    void Update()
    {
        // User wants to break the loop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skipLoop = true;
        }
    }

    // ================================
    //  CALLED BY SIGNAL: loopStart
    // ================================
    public void SetLoopStart()
    {
        loopStart = director.time;
        Debug.Log("LoopStart recorded at: " + loopStart);
    }

    // ================================
    //  CALLED BY SIGNAL: loopEnd
    // ================================
    public void OnLoopEnd()
    {
        loopEnd = director.time;
        Debug.Log("Reached loopEnd at: " + loopEnd);

        // Skip loop if user presses space
        if (skipLoop)
        {
            skipLoop = false;
            director.time = loopEnd + 0.01;  // continue forward
            director.Evaluate();
            Debug.Log("Loop skipped!");
            return;
        }

        // NORMAL LOOP
        if (loopStart >= 0)
        {
            director.time = loopStart;
            director.Evaluate();
            Debug.Log("Looped back to: " + loopStart);
        }
    }
}
