using fqn_FirstAppDemo.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=391641 上有介绍

namespace fqn_FirstAppDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel MainViewModel { get; set; }

        public MainPage()
        {
            //一定要注意这里的先后顺序
            this.MainViewModel = new MainPageViewModel();
            //this.MainViewModel.TransCode = "CreateNormalTiles";
            this.MainViewModel.TransCode = "CreateNotifyTiles";

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。
        /// 此参数通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: 准备此处显示的页面。

            // TODO: 如果您的应用程序包含多个页面，请确保
            // 通过注册以下事件来处理硬件“后退”按钮:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed 事件。
            // 如果使用由某些模板提供的 NavigationHelper，
            // 则系统会为您处理该事件。

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tileProperty = new TilePropertyModel();
            CommonHelper.CreateNormalTiles(tileProperty);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tileType = e.AddedItems.FirstOrDefault();
            if (tileType != null)
            {
                this.MainViewModel.TilePropertyModel.TileTmpl = (TileTemplateType)tileType;
            }
        }

    }

    public class MyTileTemplateCollection : ObservableCollection<TileTemplateType>
    {
        public MyTileTemplateCollection()
        {
            var list = Enum.GetValues(typeof(TileTemplateType));
            foreach (var item in list)
            {
                Add((TileTemplateType)item);
            }
        }
    }

    #region 自定义ViewModel
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand MyCommandBase { get; set; }

        public ObservableCollection<TileTemplateType> TileTmplList { get; set; }

        public string TransCode { get; set; }

        public TilePropertyModel TilePropertyModel { get; set; }

        public MainPageViewModel()
        {
            this.MyCommandBase = new MyCommandBase();
            this.TilePropertyModel = new TilePropertyModel();
            this.TileTmplList = new MyTileTemplateCollection();
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
    #endregion



    #region 自定义Command类
    public class MyCommandBase : ICommand
    {
        #region ICommand 成员

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var model = parameter as MainPageViewModel;
            var tileProperty = model.TilePropertyModel;

            //已设置默认值
            //tileProperty.Arguments = "Arguments";
            //tileProperty.DisplayName = "DisplayName";
            //tileProperty.PhoneticName = "PhoneticName";
            //tileProperty.TileId = "TileId";
            if (tileProperty == null)
            {
                return;
            }

            string transCode = model.TransCode;
            switch (transCode)
            {
                case "CreateNormalTiles": //创建一个动态磁贴
                    CommonHelper.CreateNormalTiles(tileProperty);
                    break;
                case "CreateNotifyTiles": //创建一个动态磁贴
                    CommonHelper.CreateNotifyTiles(tileProperty);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }

    #endregion

}
