using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// “用户控件”项模板在 http://go.microsoft.com/fwlink/?LinkId=234235 上有介绍

namespace TickTick.Controls
{
    public sealed class AppBarBtnControl : Control
    {
        public AppBarBtnControl()
        {
            this.DefaultStyleKey = typeof(AppBarBtnControl);
        }


        /// <summary>
        /// 用于控件的图标
        /// </summary>
        public Symbol BtnSymbol
        {
            get { return (Symbol)GetValue(BtnSymbolProperty); }
            set { SetValue(BtnSymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnSymbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnSymbolProperty =
            DependencyProperty.Register("BtnSymbol", typeof(Symbol), typeof(AppBarBtnControl), new PropertyMetadata(Symbol.Refresh));


        /// <summary>
        /// 用于控件的名称
        /// </summary>
        public string BtnContent
        {
            get { return (string)GetValue(BtnContentProperty); }
            set { SetValue(BtnContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnContentProperty =
            DependencyProperty.Register("BtnContent", typeof(string), typeof(AppBarBtnControl), new PropertyMetadata("同步"));


        /// <summary>
        /// 用于控件的字体大小
        /// </summary>
        public double BtnFontSize
        {
            get { return (double)GetValue(BtnFontSizeProperty); }
            set { SetValue(BtnFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BtnFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnFontSizeProperty =
            DependencyProperty.Register("BtnFontSize", typeof(double), typeof(AppBarBtnControl), new PropertyMetadata(36));



    }
}
