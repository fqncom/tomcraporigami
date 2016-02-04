using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.ItcastOA.Model
{
    public class MyUserInfo
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "不能为空")]
        [StringLength(12, ErrorMessage = "长度过长")]
        public string UName { get; set; }
        [Required(ErrorMessage = "不能为空")]
        [StringLength(16, ErrorMessage = "长度过长")]
        public string UPwd { get; set; }
        [Required(ErrorMessage = "不能为空")]
        public System.DateTime SubTime { get; set; }
        public short DelFlag { get; set; }
        [Required(ErrorMessage = "不能为空")]
        public System.DateTime ModifiedOn { get; set; }
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度过长")]
        public string Remark { get; set; }
        [Required(ErrorMessage = "不能为空")]
        public string Sort { get; set; }
    }

    [MetadataType(typeof(MyUserInfo))]
    public partial class UserInfo
    {

    }
}
