using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace fqn_FirstAppDemo.MyModel
{
    public class TilePropertyModel
    {

        private string arguments;
        public string Arguments
        {
            get { return this.arguments ?? "default Argument"; }
            set { arguments = value; }
        }

        private string displayName;
        public string DisplayName
        {
            get { return this.displayName ?? "default DisplayName"; }
            set { displayName = value; }
        }

        private string phoneticName;
        public string PhoneticName
        {
            get { return this.phoneticName ?? "default PhoneticName"; }
            set { phoneticName = value; }
        }

        private string tileId;
        public string TileId
        {
            get { return this.tileId ?? "default_TileId"; }
            set { tileId = value; }
        }

        private TileTemplateType tileTmpl;
        public TileTemplateType TileTmpl
        {
            get { return TileTemplateType.TileSquare150x150Image; }
            set { this.tileTmpl = value; }
        }

    }
}
