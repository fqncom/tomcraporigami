using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.IDal
{

    public partial class S_City
    {
        public S_City()
        {
            this.S_District = new HashSet<S_District>();
        }
        private long cityID;
        [Key]
        public long CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }
        private string cityName;
        [Required]
        [StringLength(32, ErrorMessage = "城市名称不能为空")]
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        private long provinceID;
        [Required]
        public long ProvinceID
        {
            get { return provinceID; }
            set { provinceID = value; }
        }

        public ICollection<S_District> S_District { get; set; }
    }
}
