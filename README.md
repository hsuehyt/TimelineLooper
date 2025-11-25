# TimelineLooper

A simple, reliable Unity Timeline looping system using Timeline Signals.
It allows the Timeline to loop between two signal markers (loopStart → loopEnd) endlessly, and also lets the user manually toggle looping ON/OFF at runtime using the keyboard or an Inspector toggle.

This system works no matter where the loopStart and loopEnd signals are placed, as long as loopStart occurs before loopEnd.

---

## Features

• Loop any section of the Timeline using two signals
• Press Space to toggle looping ON/OFF
• Loop toggle is visible in the Inspector (default ON)
• When looping is OFF, the Timeline skips the loop and continues forward normally
• User can turn looping back ON at any time
• Always moves forward naturally whenever loop is skipped
• Fully compatible with Timeline Signals and Signal Receiver
• No custom Playables required

---

## How It Works

1. Timeline starts at frame 0 and plays normally.
2. When playback reaches the signal named "loopStart", the script stores that time.
3. When the Timeline reaches the signal named "loopEnd":
   • If loop is enabled → the playhead jumps back to loopStart
   • If loop is disabled → the playhead continues forward
4. Pressing Space toggles loop mode ON and OFF at runtime.
5. The Inspector toggle also allows manual control of looping while the Timeline plays.

---

## Loop Toggle Behavior

Loop toggle (loopEnabled) starts as ON by default.

Press Space:
• If looping is ON → it turns OFF and the current loop is skipped
• If looping is OFF → it turns ON and looping resumes on the next loopEnd

Inspector toggle:
• Works the same as pressing Space
• Changing the toggle during playback immediately affects looping
• Turning OFF skips the loop once
• Turning ON resumes looping normally

---

## Setup Instructions

1. Add a PlayableDirector to a GameObject.
2. Add a Signal Receiver to the same GameObject.
3. Add the TimelineLooper script to the same GameObject.
4. In your Timeline, create two Signal Emitters:
   • One named "loopStart"
   • One named "loopEnd"
5. In the Signal Receiver inspector:
   • Assign loopStart → TimelineLooper → SetLoopStart
   • Assign loopEnd → TimelineLooper → OnLoopEnd
6. Assign the PlayableDirector to the “director” field of the script.
7. Enter Play Mode. Looping works automatically.

---

## Skipping or Resuming a Loop

Keyboard:
• Press Space to toggle looping

Inspector:
• Turn the toggle on/off in the "Loop Control" section

---

## Requirements

• Unity with Timeline package
• Timeline Signals enabled
• PlayableDirector and Signal Receiver on the same GameObject

---

## Why This System?

• Avoids Timeline evaluation conflicts
• Never freezes or locks the Timeline
• Minimal code and easy maintenance
• Works naturally with Unity’s Timeline workflow
• Flexible: loop, skip, resume anytime

---

## License

MIT License