using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeyBoard
{
    public static class KeyboardHook
    {
        public static event EventHandler ToggleWindow;
        public static event EventHandler ToggleConversionMode;
        public static event EventHandler CopySinhala;
        public static event EventHandler CopyLegacy;
        public static event EventHandler ClearAll;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hMod, uint threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr _hookID = IntPtr.Zero;

        public static void Start()
        {
            var module = Process.GetCurrentProcess().MainModule;
            var hMod = GetModuleHandle(module.ModuleName);
            _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, hMod, 0);
        }

        public static void Stop()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                Keys modifiers = Control.ModifierKeys;

                // Ctrl + Alt + W → Toggle window
                if (modifiers == (Keys.Control | Keys.Alt) && key == Keys.W)
                {
                    ToggleWindow?.Invoke(null, EventArgs.Empty);
                    return (IntPtr)1;
                }

                // Ctrl + Alt + Q → Toggle conversion mode
                if (modifiers == (Keys.Control | Keys.Alt) && key == Keys.Q)
                {
                    ToggleConversionMode?.Invoke(null, EventArgs.Empty);
                    return (IntPtr)1;
                }

                // Ctrl + Alt + S → Copy Sinhala
                if (modifiers == (Keys.Control | Keys.Alt) && key == Keys.S)
                {
                    CopySinhala?.Invoke(null, EventArgs.Empty);
                    return (IntPtr)1;
                }

                // Ctrl + Alt + A → Copy Legacy
                if (modifiers == (Keys.Control | Keys.Alt) && key == Keys.A)
                {
                    CopyLegacy?.Invoke(null, EventArgs.Empty);
                    return (IntPtr)1;
                }

                // Esc → Clear All
                if (key == Keys.Escape)
                {
                    ClearAll?.Invoke(null, EventArgs.Empty);
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}