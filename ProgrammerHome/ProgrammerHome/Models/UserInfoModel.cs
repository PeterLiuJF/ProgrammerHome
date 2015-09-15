using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgrammerHome
{
    public class UserInfoModel
    {
        public int ID { get; set; }

        #region 注册时必须输入
        [Required(ErrorMessage = "请输入用户名。")]
        [StringLength(160, MinimumLength = 3, ErrorMessage = "用户名长度不够。")]
        [Display(Name="用户名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "请输入密码。")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "密码长度不够。")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请确认密码。")]
        [Compare("Password", ErrorMessage = "密码不一致。")]
        [Display(Name = "确认密码")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "请输入邮箱。")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "请输入正确邮箱")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        #endregion

        #region 注册时可选择填写
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请选择性别。")]
        [Display(Name = "性别")]
        public string Sex { get; set; }

        [Display(Name = "出生日期")]
        public string BirthDay { get; set; }
        
        [Display(Name = "家乡")]
        public string HomeTown { get; set; }

        [Display(Name = "婚姻")]
        public string Marriage { get; set; }

        [Display(Name = "现居住地")]
        public string TheLatter{ get; set; }

        [Display(Name = "职位")]
        public string Position { get; set; }

        [Display(Name = "工作单位")]
        public string Company { get; set; }

        [Display(Name = "工作状况")]
        public int WorkingConditions { get; set; }

        [Display(Name = "兴趣")]
        public string InterestIn { get; set; }

        [Display(Name = "QQ")]
        [RegularExpression("^\\d{5,10}$", ErrorMessage = "请输入正确QQ格式")]
        public string QQ { get; set; }

        [Display(Name = "自我介绍")]
        public string IntroduceMyself { get; set; }
        #endregion

        [Display(Name = "入园时间")]
        public string EntryDate { get; set; }
    }
}