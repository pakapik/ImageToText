using System;
using System.Windows.Forms;

namespace ImageToText
{
    public class ClipBoardUpdaterImageToText
    {
        public ImageToTextConverter Converter { get; }

        public event Action<string> Alarm;

        public ClipBoardUpdaterImageToText(Action<string> alarm, ImageToTextConverter converter)
        {
            Converter = converter;

            Alarm += alarm;
        }

        public void Update()
        {
            try
            {
                var text = Converter.RecognizeText();
                Clipboard.SetText(text);
                Alarm?.Invoke("Текст распознан!");
            }
            catch(ArgumentException ex)
            {
                Alarm?.Invoke(ex.Message);
            }
        }
    }
}