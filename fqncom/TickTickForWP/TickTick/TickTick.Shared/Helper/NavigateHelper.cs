using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TickTick.Helper
{
    public static class NavigateHelper
    {
        /// <summary>
        /// 导航页面方法
        /// </summary>
        /// <param name="type"></param>
        public static void NavigateToPage(Type type)
        {
            NavigateToPageWithParam(type, new FrameTransitionParam { });
        }
        public async static void NavigateToPageWithParam(Type type, FrameTransitionParam param)
        {
            var frame = Window.Current.Content as Frame;
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                frame.Navigate(type, param);
            });
        }

        public static async void NavigateToBackPage()
        {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        public static void NavigateToForwardPage()
        {
            var frame = Window.Current.Content as Frame;
            if (frame.CanGoForward)
            {
                frame.GoForward();
            }
        }
    }
}
