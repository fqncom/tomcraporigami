using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TickTick.Helper
{
    public static class MessageDialogHelper
    {

        public static async Task MessageDialogShowAsync(string content)
        {
            await MessageDialogShowAsync(content, string.Empty);
        }
        public static async Task MessageDialogShowAsync(string content, string title)
        {
            await MessageDialogShowAsync(content, title, null);
        }
        public static async Task MessageDialogShowAsync(string content, string title, params IUICommand[] commands)
        {
            MessageDialog message = new MessageDialog(content, title);
            if (commands != null)
            {
                foreach (var item in commands)
                {
                    message.Commands.Add(item);
                }
            }
            await message.ShowAsync();
        }
    }
}
