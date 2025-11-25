# TimelineLooper

A simple, reliable Unity Timeline looping system using Timeline Signals.
It lets the Timeline loop between two signal markers (loopStart → loopEnd) endlessly, and also allows the user to break out of the loop at any time by pressing a key (default: Space).

This solution works even if the loopStart and loopEnd signals are placed anywhere in the Timeline, as long as loopStart occurs before loopEnd.

---

## Features

• Loop any section of the Timeline using two signals
• Always moves forward normally until reaching loopEnd
• Automatically jumps back to loopStart when loopEnd is reached
• User can skip the loop at any moment and continue forward
• Works with any frame rate and any Timeline length
• Clean, easy-to-read script
• No timeline hacking or custom Playables required
• Fully compatible with Timeline Signals and Signal Receiver

---

## How It Works

1. Timeline starts at frame 0 and plays normally.
2. When playback reaches the signal named "loopStart", the script records the time.
3. When playback reaches the signal named "loopEnd", the script jumps the playhead back to loopStart, creating an endless loop.
4. If the user presses the skip key (Space by default), the loop is broken and the Timeline continues forward past loopEnd.

---

## Setup Instructions

1. Add a PlayableDirector to your GameObject.
2. Add a Signal Receiver to the same GameObject.
3. Add the TimelineLooper script to the same GameObject.
4. In your Timeline, create two Signal Emitters:
   • One named "loopStart"
   • One named "loopEnd"
5. In the Signal Receiver’s inspector, route each signal:
   • loopStart → TimelineLooper → SetLoopStart
   • loopEnd → TimelineLooper → OnLoopEnd
6. Assign the PlayableDirector to the script’s "director" field.
7. Enter Play Mode. The loop will automatically work.

---

## Skipping the Loop

Press Space at any time.
The next time loopEnd is reached, the script jumps forward and the Timeline continues normally.

You can easily change the skip key inside the script.

---

## Requirements

• Unity with Timeline package
• Timeline Signals enabled
• A PlayableDirector component
• A Signal Receiver on the same GameObject as the TimelineLooper script

---

## Why Use This System?

• Avoids Timeline locking or freezing
• Avoids Evaluation conflicts
• Works cleanly with Unity’s built-in Timeline workflow
• Very small and maintainable
• No custom Playables or PlayableGraphs needed

---

## License

MIT License (feel free to use in any project)