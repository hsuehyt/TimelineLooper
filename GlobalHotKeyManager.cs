using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalHotKeyManager : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    private delegate IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam);
    private static HookCallback proc = HookCallbackMethod;
    private static IntPtr hookID = IntPtr.Zero;

    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private const int VK_SPACE = 0x20;

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int idHook, HookCallback lpfn,
        IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    static GlobalHotKeyManager instance;

    void Awake()
    {
        instance = this;

        using (Process cur = Process.GetCurrentProcess())
        using (ProcessModule module = cur.MainModule)
        {
            hookID = SetWindowsHookEx(
                WH_KEYBOARD_LL,
                proc,
                GetModuleHandle(module.ModuleName),
                0);
        }

        UnityEngine.Debug.Log("GlobalHotKeyManager: Installed global key hook.");
    }

    void OnDestroy()
    {
        UnhookWindowsHookEx(hookID);
    }

    private static IntPtr HookCallbackMethod(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vk = Marshal.ReadInt32(lParam);

            if (vk == VK_SPACE)
            {
                var looper = FindObjectOfType<TimelineLooper>();
                if (looper != null)
                    looper.ToggleLoopFromOutside();
            }
        }

        return CallNextHookEx(hookID, code, wParam, lParam);
    }
#endif
}
