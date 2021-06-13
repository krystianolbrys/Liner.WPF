using System;
using System.Text;
using System.Windows.Controls;
using Liner.Infrastructure;

namespace Liner.App.Loggers
{
    public class UILogger : ILogger
    {
        private readonly TextBox _textBox;

        public UILogger(TextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        public void Log(object message)
        {
            var builder = new StringBuilder();

            builder.Append(_textBox.Text);
            builder.AppendLine(message.ToString());

            _textBox.Text = builder.ToString();
            _textBox.ScrollToEnd();
        }
    }
}
