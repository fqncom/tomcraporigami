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
    public sealed class TasksItemStatusControl : Control
    {
        public TasksItemStatusControl()
        {
            this.DefaultStyleKey = typeof(TasksItemStatusControl);
        }

        //public Visibility RepeatIconVisibility
        //{
        //    get { return (Visibility)GetValue(RepeatIconVisibilityProperty); }
        //    set { SetValue(RepeatIconVisibilityProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for RepeatIconVisibility.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty RepeatIconVisibilityProperty =
        //    DependencyProperty.Register("RepeatIconVisibility", typeof(Visibility), typeof(TasksItemStatusControl), new PropertyMetadata(Windows.UI.Xaml.Visibility.Collapsed));



        //public Visibility RemindIconVisibility
        //{
        //    get { return (Visibility)GetValue(RemindIconVisibilityProperty); }
        //    set { SetValue(RemindIconVisibilityProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for RemindIconVisibility.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty RemindIconVisibilityProperty =
        //    DependencyProperty.Register("RemindIconVisibility", typeof(Visibility), typeof(TasksItemStatusControl), new PropertyMetadata(Windows.UI.Xaml.Visibility.Collapsed));



        //public Visibility ContentIconVisibility
        //{
        //    get { return (Visibility)GetValue(ContentIconVisibilityProperty); }
        //    set { SetValue(ContentIconVisibilityProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ContentIconVisibility.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ContentIconVisibilityProperty =
        //    DependencyProperty.Register("ContentIconVisibility", typeof(Visibility), typeof(TasksItemStatusControl), new PropertyMetadata(Windows.UI.Xaml.Visibility.Collapsed));


        
    }
}
