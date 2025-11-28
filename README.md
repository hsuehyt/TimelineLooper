# TimelineLooper

TimelineLooper allows a Unity Timeline to loop between two user-defined signal points: **loopStart** and **loopEnd**.
It is designed for immersive projection setups, installations, and gallery environments where a Timeline must loop indefinitely until manually interrupted.

This package now supports **background keyboard input** (Space key) through a separate **GlobalHotKeyManager.cs** so the loop can be toggled even when Unity is not in focus (for example when Resolume Arena is the active foreground application).

---

## Features

• Loop any Timeline section using two Signals
• Press **Space** to toggle looping ON/OFF
• Space key works:
– when Unity is focused (normal Input)
– when Unity is NOT focused (via GlobalHotKeyManager)
• Allows skipping the next loop once when loopEnabled is turned off
• Automatically re-applies object activation states when changed from Inspector
• Supports toggling any GameObjects in a list (such as subtitles, UI elements, etc.)

---

## How It Works

### LoopStart Signal

Place a Signal on the Timeline where the loop should begin.
It calls:
`SetLoopStart()`

### LoopEnd Signal

Place another Signal later in the Timeline where looping should end.
It calls:
`OnLoopEnd()`

When the Timeline hits loopEnd:

• If **loopEnabled = true** → Timeline jumps back to loopStart
• If **loopEnabled = false** → Timeline continues normally
• If loopEnabled was just turned off, the loop is skipped **once**, allowing a clean exit

---

## Space Key Control

### When Unity Game Window IS focused

The original TimelineLooper.cs uses:
Input.GetKeyDown(KeyCode.Space)

### When Unity Game Window is NOT focused

(GlobalHotKeyManager.cs detects the key via Windows low-level system hook)

The manager calls:
`ToggleLoopFromOutside()`
inside TimelineLooper.

Both inputs trigger the same behavior.

---

## GlobalHotKeyManager Integration

**GlobalHotKeyManager.cs** installs a Windows low-level keyboard hook (WH_KEYBOARD_LL).
It listens for the **Space bar globally**, even if:
• Resolume Arena is the foreground window
• Unity is running in background
• The game window does not have focus

When Space is detected:
• The manager finds TimelineLooper in the scene
• Calls `ToggleLoopFromOutside()`
• Loop mode toggles cleanly

This keeps TimelineLooper.cs simple:
all OS-specific code lives in **GlobalHotKeyManager.cs**.

---

## Inspector Options

### Loop Enabled (default ON)

Master switch for looping behavior.
Turning it OFF also triggers one “skip loop once” behavior during the next OnLoopEnd.

### Toggle Objects

A list of GameObjects that should activate/deactivate according to loopEnabled.
For example:
• Subtitle group
• UI overlay
• Lighting objects
• Audio cue objects

When loop is ON → Objects become active
When loop is OFF → Objects become inactive

---

## Public Method for External Control

TimelineLooper includes:

`ToggleLoopFromOutside()`

This is called by GlobalHotKeyManager.cs when the user presses Space while Unity is not focused.
It performs the exact same behavior as the internal Input.GetKeyDown.

---

## Recommended Setup

1. Add **TimelineLooper.cs** to any GameObject with a PlayableDirector
2. Add **GlobalHotKeyManager.cs** to an object in your Master Scene
3. Add two Signals to your Timeline:
   – loopStart → calls SetLoopStart
   – loopEnd → calls OnLoopEnd
4. Run the project
5. Press Space:
   – When Unity is focused → loop toggles
   – When other software is focused (e.g., Resolume) → loop still toggles

---

## Notes

• Windows only (global hook uses Win32 API)
• Works in both Editor (focus only) and Standalone build (focus + background)
• Only one GlobalHotKeyManager should exist in the scene
• TimelineLooper does not contain any OS-specific code
