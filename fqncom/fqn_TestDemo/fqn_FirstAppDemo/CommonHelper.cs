using fqn_FirstAppDemo.MyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace fqn_FirstAppDemo
{
    public static class CommonHelper
    {

        public async static void CreateNormalTiles(TilePropertyModel tileProperty)
        {

            Uri uri = new Uri("ms-appx:///Assets/bird.png", UriKind.RelativeOrAbsolute);
            var tile = new SecondaryTile();
            tile.Arguments = tileProperty.Arguments;
            tile.DisplayName = tileProperty.DisplayName;
            tile.PhoneticName = tileProperty.PhoneticName;
            tile.TileId = tileProperty.TileId;
            
            tile.VisualElements.BackgroundColor = Color.FromArgb(255, 34, 32, 222);
            tile.VisualElements.ForegroundText = ForegroundText.Light;
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            tile.VisualElements.ShowNameOnSquare310x310Logo = true;
            tile.VisualElements.ShowNameOnWide310x150Logo = true;
            tile.VisualElements.Square150x150Logo = uri;
            tile.VisualElements.Square30x30Logo = uri;
            tile.VisualElements.Square310x310Logo = uri;
            //tile.VisualElements.Square70x70Logo = uri;
            tile.VisualElements.Square71x71Logo = uri;
            tile.VisualElements.Wide310x150Logo = uri;
            if (await tile.RequestCreateAsync())
            {
                System.Diagnostics.Debug.WriteLine("a tile has been created");
            }
        }

        public static void CreateNotifyTiles(TilePropertyModel tileProperty)
        {
            XmlDocument tmplContent = TileUpdateManager.GetTemplateContent(tileProperty.TileTmpl);
            var tmplNodes = tmplContent.GetElementsByTagName("image");

            foreach (var item in tmplNodes)
            {
                item.Attributes[1].NodeValue = "Assets/bird.png";
            }
            var tile = new TileNotification(tmplContent);
            tile.ExpirationTime = DateTimeOffset.Now.AddSeconds(1);
            //使用以下方法时，要求页面上先有一个磁贴才可以
            var tileTemp = TileUpdateManager.CreateTileUpdaterForSecondaryTile(tileProperty.TileId);
            tileTemp.Update(tile);
            
        }

    }
}
