﻿

#pragma checksum "D:\---vs_projects\TickTick_BackUp\TickTick for WP_03\TickTick\TickTick.WindowsPhone\Views\TasksDetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D9A2B92726EEAFA0807580394C920E47"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TickTick.Views
{
    partial class TasksDetailPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 36 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListPickerFlyout)(target)).ItemsPicked += this.ListViewPriorities_Clicked;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 75 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.TxtDueDate_Changed;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 90 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.TxtDueDateTime_Changed;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 135 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.DatePickerFlyout)(target)).DatePicked += this.DueDatePicker_Picked;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 112 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListPickerFlyout)(target)).ItemsPicked += this.RemindPicker_Picked;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 97 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TimePickerFlyout)(target)).TimePicked += this.DueDateTimePicker_Picked;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 82 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.DatePickerFlyout)(target)).DatePicked += this.DueDatePicker_Picked;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 153 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarMove_Clicked;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 168 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.TestBack_Clicked;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 157 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListPickerFlyout)(target)).ItemsPicked += this.MovePicker_Picked;
                 #line default
                 #line hidden
                break;
            case 11:
                #line 172 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarBtnPriorities_Clicked;
                 #line default
                 #line hidden
                break;
            case 12:
                #line 174 "..\..\..\Views\TasksDetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarBtnDelete_Clicked;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


