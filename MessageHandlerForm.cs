using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Tulpep.NotificationWindow;

namespace ImageToText
{
    public partial class MessageHandlerForm : Form
    {
        private ClipBoardUpdaterImageToText _updater;

        private PopupNotifier _popup;

        public MessageHandlerForm()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            Init();
            RegisterHotKey(Handle, 0, (int)KeyModifier.Control, Keys.M.GetHashCode());
            FormClosing += MessageHandler_FormClosing;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                _updater.Update();
            }

            base.WndProc(ref m);
        }

        private void Init()
        {
            _popup = PopupInit();

            _updater = new ClipBoardUpdaterImageToText(
                str =>
                {
                    _popup.ContentText = str;
                    _popup.Popup();
                },
                new ImageToTextConverter()
            );
        }

        private static PopupNotifier PopupInit()
        {
            var popup = new PopupNotifier
            {
                TitleText = "Распознавание текста",
                TitleFont = new System.Drawing.Font("Consolas", 14),
                ContentFont = new System.Drawing.Font("Consolas", 12)
            };

            return popup;
        }

        private void MessageHandler_FormClosing(object sender, FormClosingEventArgs e) => UnregisterHotKey(Handle, 0);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
