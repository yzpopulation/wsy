﻿using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace wsy
{

    public partial class wsy

    {
        #region EnumWindows

        #region EnumWindowsFlags

        /// <summary>
        ///     Window Style Flags
        /// </summary>
        [Flags]
        public enum WindowStyleFlags : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000
        }

        /// <summary>
        ///     Extended Windows Style flags
        /// </summary>
        [Flags]
        public enum ExtendedWindowStyleFlags
        {
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_LAYERED = 0x00080000,

            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring

            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
        }

        #endregion

        #region EnumWindows

        /// <summary>
        ///     EnumWindows wrapper for .NET
        /// </summary>
        public class EnumWindows
        {
            #region Member Variables

            #endregion

            #region Constructor, Dispose

            #endregion

            /// <summary>
            ///     Returns the collection of windows returned by
            ///     GetWindows
            /// </summary>
            public EnumWindowsCollection Items { get; private set; }

            /// <summary>
            ///     Gets all top level windows on the system.
            /// </summary>
            public void GetWindows()
            {
                Items = new EnumWindowsCollection();
                UnManagedMethods.EnumWindows(
                    WindowEnum,
                    0);
            }

            /// <summary>
            ///     Gets all child windows of the specified window
            /// </summary>
            /// <param name="hWndParent">Window Handle to get children for</param>
            public void GetWindows(
                IntPtr hWndParent)
            {
                Items = new EnumWindowsCollection();
                UnManagedMethods.EnumChildWindows(
                    hWndParent,
                    WindowEnum,
                    0);
            }

            #region EnumWindows callback

            /// <summary>
            ///     The enum Windows callback.
            /// </summary>
            /// <param name="hWnd">Window Handle</param>
            /// <param name="lParam">Application defined value</param>
            /// <returns>1 to continue enumeration, 0 to stop</returns>
            private int WindowEnum(
                IntPtr hWnd,
                int lParam)
            {
                if (OnWindowEnum(hWnd))
                {
                    return 1;
                }
                return 0;
            }

            #endregion

            /// <summary>
            ///     Called whenever a new window is about to be added
            ///     by the Window enumeration called from GetWindows.
            ///     If overriding this function, return true to continue
            ///     enumeration or false to stop.  If you do not call
            ///     the base implementation the Items collection will
            ///     be empty.
            /// </summary>
            /// <param name="hWnd">Window handle to add</param>
            /// <returns>True to continue enumeration, False to stop</returns>
            protected virtual bool OnWindowEnum(
                IntPtr hWnd)
            {
                Items.Add(hWnd);
                return true;
            }

            #region Delegates

            private delegate int EnumWindowsProc(IntPtr hwnd, int lParam);

            #endregion

            #region UnManagedMethods

            private class UnManagedMethods
            {
                [DllImport("user32")]
                public static extern int EnumWindows(
                    EnumWindowsProc lpEnumFunc,
                    int lParam);

                [DllImport("user32")]
                public static extern int EnumChildWindows(
                    IntPtr hWndParent,
                    EnumWindowsProc lpEnumFunc,
                    int lParam);
            }

            #endregion
        }

        #endregion EnumWindows

        #region EnumWindowsCollection

        /// <summary>
        ///     Holds a collection of Windows returned by GetWindows.
        /// </summary>
        public class EnumWindowsCollection : ReadOnlyCollectionBase
        {
            /// <summary>
            ///     Gets the Window at the specified index
            /// </summary>
            public EnumWindowsItem this[int index]
            {
                get { return (EnumWindowsItem)InnerList[index]; }
            }

            /// <summary>
            ///     Add a new Window to the collection.  Intended for
            ///     internal use by EnumWindows only.
            /// </summary>
            /// <param name="hWnd">Window handle to add</param>
            public void Add(IntPtr hWnd)
            {
                EnumWindowsItem item = new EnumWindowsItem(hWnd);
                InnerList.Add(item);
            }
        }

        #endregion

        #region EnumWindowsItem

        /// <summary>
        ///     Provides details about a Window returned by the
        ///     enumeration
        /// </summary>
        public class EnumWindowsItem
        {
            /// <summary>
            ///     The window handle.
            /// </summary>
            private readonly IntPtr hWnd = IntPtr.Zero;

            /// <summary>
            ///     Constructs a new instance of this class for
            ///     the specified Window Handle.
            /// </summary>
            /// <param name="hWnd">The Window Handle</param>
            public EnumWindowsItem(IntPtr hWnd)
            {
                this.hWnd = hWnd;
            }

            /// <summary>
            ///     Gets the window's handle
            /// </summary>
            public IntPtr Handle
            {
                get { return hWnd; }
            }

            /// <summary>
            ///     Gets the window's title (caption)
            /// </summary>
            public string Text
            {
                get
                {
                    StringBuilder title = new StringBuilder(260, 260);
                    UnManagedMethods.GetWindowText(hWnd, title, title.Capacity);
                    return title.ToString();
                }
            }

            /// <summary>
            ///     Gets the window's class name.
            /// </summary>
            public string ClassName
            {
                get
                {
                    StringBuilder className = new StringBuilder(260, 260);
                    UnManagedMethods.GetClassName(hWnd, className,
                        className.Capacity);
                    return className.ToString();
                }
            }

            /// <summary>
            ///     Gets/Sets whether the window is iconic (mimimised) or not.
            /// </summary>
            public bool Iconic
            {
                get { return UnManagedMethods.IsIconic(hWnd) == 0 ? false : true; }
                set
                {
                    UnManagedMethods.SendMessage(
                        hWnd,
                        UnManagedMethods.WM_SYSCOMMAND,
                        (IntPtr)UnManagedMethods.SC_MINIMIZE,
                        IntPtr.Zero);
                }
            }

            /// <summary>
            ///     Gets/Sets whether the window is maximised or not.
            /// </summary>
            public bool Maximised
            {
                get { return UnManagedMethods.IsZoomed(hWnd) == 0 ? false : true; }
                set
                {
                    UnManagedMethods.SendMessage(
                        hWnd,
                        UnManagedMethods.WM_SYSCOMMAND,
                        (IntPtr)UnManagedMethods.SC_MAXIMIZE,
                        IntPtr.Zero);
                }
            }

            /// <summary>
            ///     Gets whether the window is visible.
            /// </summary>
            public bool Visible
            {
                get
                {
                    return UnManagedMethods.IsWindowVisible(hWnd) == 0
                        ? false
                        : true;
                }
            }

            /// <summary>
            ///     Gets the bounding rectangle of the window
            /// </summary>
            public Rectangle Rect
            {
                get
                {
                    RECT rc = new RECT();
                    UnManagedMethods.GetWindowRect(
                        hWnd,
                        ref rc);
                    Rectangle rcRet = new Rectangle(
                        rc.Left, rc.Top,
                        rc.Right - rc.Left, rc.Bottom - rc.Top);
                    return rcRet;
                }
            }

            /// <summary>
            ///     Gets the location of the window relative to the screen.
            /// </summary>
            public Point Location
            {
                get
                {
                    Rectangle rc = Rect;
                    Point pt = new Point(
                        rc.Left,
                        rc.Top);
                    return pt;
                }
            }

            /// <summary>
            ///     Gets the size of the window.
            /// </summary>
            public Size Size
            {
                get
                {
                    Rectangle rc = Rect;
                    Size sz = new Size(
                        rc.Right - rc.Left,
                        rc.Bottom - rc.Top);
                    return sz;
                }
            }

            public WindowStyleFlags WindowStyle
            {
                get
                {
                    return (WindowStyleFlags)UnManagedMethods.GetWindowLong(
                        hWnd, UnManagedMethods.GWL_STYLE);
                }
            }

            public ExtendedWindowStyleFlags ExtendedWindowStyle
            {
                get
                {
                    return (ExtendedWindowStyleFlags)UnManagedMethods.GetWindowLong(
                        hWnd, UnManagedMethods.GWL_EXSTYLE);
                }
            }

            /// <summary>
            ///     To allow items to be compared, the hash code
            ///     is set to the Window handle, so two EnumWindowsItem
            ///     objects for the same Window will be equal.
            /// </summary>
            /// <returns>The Window Handle for this window</returns>
            public override int GetHashCode()
            {
                return (int)hWnd;
            }

            /// <summary>
            ///     Restores and Brings the window to the front,
            ///     assuming it is a visible application window.
            /// </summary>
            public void Restore()
            {
                if (Iconic)
                {
                    UnManagedMethods.SendMessage(
                        hWnd,
                        UnManagedMethods.WM_SYSCOMMAND,
                        (IntPtr)UnManagedMethods.SC_RESTORE,
                        IntPtr.Zero);
                }
                UnManagedMethods.BringWindowToTop(hWnd);
                UnManagedMethods.SetForegroundWindow(hWnd);
            }

            #region UnManagedMethods

            private class UnManagedMethods
            {
                public const int WM_COMMAND = 0x111;
                public const int WM_SYSCOMMAND = 0x112;

                public const int SC_RESTORE = 0xF120;
                public const int SC_CLOSE = 0xF060;
                public const int SC_MAXIMIZE = 0xF030;
                public const int SC_MINIMIZE = 0xF020;

                public const int GWL_STYLE = -16;
                public const int GWL_EXSTYLE = -20;

                /// <summary>
                ///     Stop flashing. The system restores the window to its original state.
                /// </summary>
                public const int FLASHW_STOP = 0;

                /// <summary>
                ///     Flash the window caption.
                /// </summary>
                public const int FLASHW_CAPTION = 0x00000001;

                /// <summary>
                ///     Flash the taskbar button.
                /// </summary>
                public const int FLASHW_TRAY = 0x00000002;

                /// <summary>
                ///     Flash both the window caption and taskbar button.
                /// </summary>
                public const int FLASHW_ALL = FLASHW_CAPTION | FLASHW_TRAY;

                /// <summary>
                ///     Flash continuously, until the FLASHW_STOP flag is set.
                /// </summary>
                public const int FLASHW_TIMER = 0x00000004;

                /// <summary>
                ///     Flash continuously until the window comes to the foreground.
                /// </summary>
                public const int FLASHW_TIMERNOFG = 0x0000000C;

                [DllImport("user32")]
                public static extern int IsWindowVisible(
                    IntPtr hWnd);

                [DllImport("user32", CharSet = CharSet.Auto)]
                public static extern int GetWindowText(
                    IntPtr hWnd,
                    StringBuilder lpString,
                    int cch);

                [DllImport("user32", CharSet = CharSet.Auto)]
                public static extern int GetWindowTextLength(
                    IntPtr hWnd);

                [DllImport("user32")]
                public static extern int BringWindowToTop(IntPtr hWnd);

                [DllImport("user32")]
                public static extern int SetForegroundWindow(IntPtr hWnd);

                [DllImport("user32")]
                public static extern int IsIconic(IntPtr hWnd);

                [DllImport("user32")]
                public static extern int IsZoomed(IntPtr hwnd);

                [DllImport("user32", CharSet = CharSet.Auto)]
                public static extern int GetClassName(
                    IntPtr hWnd,
                    StringBuilder lpClassName,
                    int nMaxCount);

                [DllImport("user32")]
                public static extern int FlashWindow(
                    IntPtr hWnd,
                    ref FLASHWINFO pwfi);

                [DllImport("user32")]
                public static extern int GetWindowRect(
                    IntPtr hWnd,
                    ref RECT lpRect);

                [DllImport("user32", CharSet = CharSet.Auto)]
                public static extern int SendMessage(
                    IntPtr hWnd,
                    int wMsg,
                    IntPtr wParam,
                    IntPtr lParam);

                [DllImport("user32", CharSet = CharSet.Auto)]
                public static extern uint GetWindowLong(
                    IntPtr hwnd,
                    int nIndex);
            }

            #endregion

            #region Structures

            [StructLayout(LayoutKind.Sequential, Pack = 4)]
            private struct RECT
            {
                public readonly int Left;
                public readonly int Top;
                public readonly int Right;
                public readonly int Bottom;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 4)]
            private struct FLASHWINFO
            {
                public readonly int cbSize;
                public readonly IntPtr hwnd;
                public readonly int dwFlags;
                public readonly int uCount;
                public readonly int dwTimeout;
            }

            #endregion
        }

        #endregion

        #endregion
    }
}
