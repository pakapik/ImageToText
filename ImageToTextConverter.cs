using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageToText
{
    public class ImageToTextConverter
    {
        private readonly Tesseract _tesseractRus;
        private readonly Tesseract _tesseractEng;

        private Tesseract Tesseract => Language == Language.English ? _tesseractEng : _tesseractRus;

        private Language Language => (Language)GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero));

        public ImageToTextConverter()
        {
            _tesseractRus = new Tesseract(@"C:\tessdata", "rus", OcrEngineMode.TesseractLstmCombined);
            _tesseractEng = new Tesseract(@"C:\tessdata", "eng", OcrEngineMode.TesseractLstmCombined);
        }

        public string RecognizeText()
        {
            SetTesseractImage();
            Tesseract.Recognize();
            return Tesseract.GetUTF8Text();
        }

        private void SetTesseractImage()
        {
            if (!Clipboard.ContainsImage())
            {
                throw new ArgumentNullException("Null image", "Буфер не содержит изображения!");
            }

            Tesseract.SetImage(new Image<Bgr, byte>((Bitmap)Clipboard.GetImage()));
        }

        #region DllImport

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId([In] IntPtr hWnd, [Out, Optional] IntPtr lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern ushort GetKeyboardLayout([In] int idThread);

        #endregion
    }
}