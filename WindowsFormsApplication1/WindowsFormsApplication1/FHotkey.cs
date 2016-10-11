using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FHotkey
{
    public enum KeyModifier
    {
        None,
        Alt = 0x0001,
        Control,
        Shift = 0x0004,
        Win = 0x0008
    }

    public class FHotkey : IMessageFilter
    {
        #region WinApi
        [DllImport("user32.dll")]
        static extern int RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        static extern int UnregisterHotKey(IntPtr hWnd, int id);

        public const int WM_HOTKEY = 0x0312;

        #endregion

        public delegate void HotKeyCallback();

        private Dictionary<int, HotKeyCallback> callBacks;
        private Stack<int> hotKeysId;
        private int hotKeysCount;

        public FHotkey(int hotKeysCount)
        {
            this.hotKeysCount = hotKeysCount;
            callBacks = new Dictionary<int, HotKeyCallback>(hotKeysCount);
            hotKeysId = new Stack<int>(hotKeysCount);

            for (int i = 1; i <= hotKeysCount; i++)
                hotKeysId.Push(i);

            Application.AddMessageFilter(this);
        }

        ~FHotkey()
        {
            Application.RemoveMessageFilter(this);
            UnRegisterAll();
        }

        public bool RegisterHotKey(KeyModifier modifier, Keys key, HotKeyCallback callBack, out int hotKeyId)
        {
            if (hotKeysId.Count > 0)
            {
                hotKeyId = hotKeysId.Pop();
                int result = RegisterHotKey(IntPtr.Zero, hotKeyId, (int)modifier, (int)key);

                if (result > 0)
                {
                    callBacks.Add(hotKeyId, callBack);

                    return true;
                } else
                {
                    hotKeysId.Push(hotKeyId);
                }
            }
            hotKeyId = -1;
            return false;
        }

        public bool UnRegisterHotKey(int id)
        {
            lock (callBacks)
            {
                if (callBacks.ContainsKey(id))
                {
                    callBacks.Remove(id);
                    hotKeysId.Push(id);

                    UnRegisterHotKey(id);

                    return true;
                }
            }
            return false;
        }

        public bool UnRegisterAll()
        {
            int successes = 0;

            lock (callBacks)
            {
                foreach (int hkId in callBacks.Keys)
                {
                    successes += UnregisterHotKey(IntPtr.Zero, hkId);
                    hotKeysId.Push(hkId);
                }
            }

            int size = callBacks.Count;

            callBacks.Clear();

            return (successes == size);
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_HOTKEY:
                    lock (callBacks)
                    {
                        if (callBacks.ContainsKey((int)m.WParam))
                        {
                            callBacks[(int)m.WParam]();
                        }
                    }
                    break;
            }
            return false;
        }
    }
}
