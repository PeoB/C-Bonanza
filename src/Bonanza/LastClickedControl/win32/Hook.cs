using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LastClickedControl.win32
{
    public class Hook
    {
        #region win32 methods
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        private readonly HookProc _callback;


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        private int _hook;
        public Hook()
        {
            _callback = MouseHook;
        }
        ~Hook()
        {
            //I'm not sure how windows wants to handle this, so I'll just unhook the mouse to be on the safe side
            UnHookMouse();
        }

        public void HookMouse()
        {
            if (_hook != 0) UnHookMouse();

            _hook = SetWindowsHookEx(WH_MOUSE_LL, _callback, (IntPtr)0, 0);
            if (_hook == 0)
            {
                //TODO: hook failed, throw an exception or something
            }
        }
        public void UnHookMouse()
        {
            UnhookWindowsHookEx(_hook);
            _hook = 0;
        }
        private int MouseHook(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                var mouseStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));

                if (wParam.ToInt32() == WM_LBUTTONDOWN)
                {
                    var bgWorker = new BackgroundWorker();
                    bgWorker.DoWork += (sender, eventArgs) => OnMouseButtonDown(new MouseEventArgs(mouseStruct.pt));
                    bgWorker.RunWorkerAsync();
                }

                return CallNextHookEx(_hook, nCode, wParam, lParam);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }

        public event EventHandler<MouseEventArgs> MouseButtonDown;

        public void OnMouseButtonDown(MouseEventArgs e)
        {
            var handler = MouseButtonDown;
            if (handler != null) handler(this, e);
        }
    }
}