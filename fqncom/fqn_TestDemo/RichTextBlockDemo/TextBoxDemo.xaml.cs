using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkID=390556 上有介绍

namespace RichTextBlockDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TextBoxDemo : Page
    {

        public IList<string> stringList = new List<string> { "1223", "12wer", "123422", "1werq", "12qwerq", "1asdf2", "12qwre", "123432re", "1werq232", "1wefsdf2" };
        public TextBoxDemo()
        {
            this.InitializeComponent();
            this.auto_suggest_box.ItemsSource = stringList.Where(s => stringList.IndexOf(s) < 5);//选取位置在第五位之前的数据
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.txtShow.Text = e.Parameter.ToString();
        }

        private void auto_suggest_box_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string text = this.auto_suggest_box.Text.ToUpper();
            this.auto_suggest_box.ItemsSource = stringList.Where(s => s.Contains(text));
        }

        private void auto_suggest_box_GotFocus(object sender, RoutedEventArgs e)
        {
            this.auto_suggest_box.Text = "";
        }

        private async void btnMessageDialog_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog md = new MessageDialog("this is a normal messageDialog come from messageBox", "this is title");
            await md.ShowAsync();
            //md.ShowAsync();
        }

        private async void btnContentDialog_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog();
            cd.Content = "this is a content Dialog";
            cd.Title = "content dialog";
            cd.FullSizeDesired = true;
            cd.Foreground = new SolidColorBrush(Color.FromArgb(34, 32, 34, 32));
            cd.Background = new WebViewBrush();
            cd.BorderThickness = new Thickness(10, 15, 20, 25);
            cd.CharacterSpacing = 10;
            this.auto_suggest_box.Text = cd.DesiredSize.ToString();
            await cd.ShowAsync();
        }

        private void btnCustomDialog_Click(object sender, RoutedEventArgs e)
        {
            //this property will let the tag style change to simple style ,and to be specific ,I don't know how it works
            this.commandBar.PrimaryCommands.FirstOrDefault().IsCompact = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Windows.UI.ViewManagement.StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
        }
    }
}
