using System;
using System.Linq;
using System.Windows.Forms;

class ControlFreezer {

    public static void Freeze(Control Control) {

        foreach (Control control in Control.Controls) {

            Freeze(control);

        }

        Message targetMessage = Message.Create(Control.Handle, 11, System.IntPtr.Zero, System.IntPtr.Zero);
        NativeWindow targetWindow = NativeWindow.FromHandle(Control.Handle);

        targetWindow.DefWndProc(ref targetMessage);

    }

    public static void Unfreeze(Control Control) {

        foreach (Control control in Control.Controls) {

            Unfreeze(control);

        }

        IntPtr wparam = new IntPtr(1);
        Message targetMessage = Message.Create(Control.Handle, 11, wparam, System.IntPtr.Zero);
        NativeWindow targetWindow = NativeWindow.FromHandle(Control.Handle);

        targetWindow.DefWndProc(ref targetMessage);

        Control.Invalidate();
        Control.Refresh();
      
    }

}