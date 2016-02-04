using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fqn.MVC_EF.IDal
{
    public partial class UserInfoPartial
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(32)]
        public string Name { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",ErrorMessage = "密码格式不正确，请使用邮箱格式")]//增加约束，正则表达式验证密码
        public string Password { get; set; }
        
        public System.DateTime CreateTime { get; set; }
    }

    [MetadataType(typeof(UserInfoPartial))]
    public partial class UserInfo
    {

    }
}
