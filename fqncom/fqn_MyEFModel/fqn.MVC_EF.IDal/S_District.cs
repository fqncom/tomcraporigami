using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.IDal
{

    public partial class S_District
    {
        private long districtID;
        [Key]
        public long DistrictID
        {
            get { return districtID; }
            set { districtID = value; }
        }

        private string districtName;
        [Required]
        [StringLength(32, ErrorMessage = "街道名称不能为空")]
        public string DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        private long cityID;
        [Required]
        public long CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }
    }
}
