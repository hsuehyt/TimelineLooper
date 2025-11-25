# TimelineLooper

A simple, reliable Unity Timeline looping system using Timeline Signals.
It lets the Timeline loop endlessly between two signal markers (loopStart → loopEnd), allows the user to break out of the loop at any time, and also provides a Loop Toggle that can automatically activate/deactivate any GameObjects in the scene.

This system works no matter where the loopStart and loopEnd signals are placed, as long as loopStart occurs before loopEnd.

---

## Features

• Loop any section of the Timeline using two signals
• Press Space to toggle looping ON/OFF
• Inspector toggle also controls looping
• Assign any number of GameObjects to automatically enable/disable based on loop state
• When looping is OFF, the Timeline skips the loop and plays forward normally
• When looping is ON, the Timeline repeats between loopStart and loopEnd
• Looping can be resumed at any time
• Fully compatible with Timeline Signals and Signal Receiver
• No custom PlayableGraph or Timeline hacking required

---

## How It Works

1. The Timeline starts at frame 0 and plays normally.
2. At the loopStart signal, the script records the current time.
3. At the loopEnd signal:
   • If looping is enabled, the playhead jumps back to loopStart
   • If looping is disabled, playback continues forward
4. Pressing Space toggles looping ON/OFF.
5. The Inspector toggle also allows manual loop control at runtime.
6. Any GameObjects in the toggle list will automatically activate when looping is ON and deactivate when looping is OFF.

---

## Loop Toggle Behavior

Loop toggle (loopEnabled) starts ON by default.

Press Space:
• If looping is ON → it turns OFF and the current loop is skipped
• If looping is OFF → it turns ON and looping resumes at the next loopEnd

Inspector (Loop Enabled):
• Changing it during gameplay immediately updates loop state
• Turning it OFF hides the assigned GameObjects and skips the current loop once
• Turning it ON shows the assigned GameObjects and restores looping

---

## GameObject Toggle List

The script contains a section called “Objects Affected By Loop Toggle.”

You can drag any number of GameObjects into this list, such as:
• Subtitles
• UI canvases
• Cameras
• Props
• Effects
• Scene hints or prompts

Whenever loopEnabled changes, these objects instantly turn ON/OFF.

---

## Setup Instructions

1. Add a PlayableDirector to a GameObject.
2. Add a Signal Receiver to the same GameObject.
3. Add the TimelineLooper script to the same GameObject.
4. In your Timeline:
   • Add a signal named “loopStart”
   • Add a signal named “loopEnd”
5. In the Signal Receiver inspector:
   • loopStart → TimelineLooper → SetLoopStart
   • loopEnd → TimelineLooper → OnLoopEnd
6. In the TimelineLooper inspector:
   • Assign the PlayableDirector
   • Add any GameObjects you want to be toggled when loop state changes
7. Enter Play Mode. Looping works automatically.

---

## Skipping or Resuming a Loop

Keyboard:
• Press Space to toggle looping ON/OFF

Inspector:
• Turn the “Loop Enabled” checkbox ON/OFF at runtime

GameObjects in the toggle list will update automatically.

---

## Requirements

• Unity Timeline package
• Timeline Signals enabled
• PlayableDirector and Signal Receiver on the same GameObject

---

## License

MIT License