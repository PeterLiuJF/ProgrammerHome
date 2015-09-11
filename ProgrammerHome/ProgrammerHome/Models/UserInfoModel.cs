using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammerHome
{
    public class UserInfoModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "性别")]
        public string Sex { get; set; }
        [Required]
        [Display(Name="用户名")]
        public string Account { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        
        [Display(Name = "出生日期")]
        public string BirthDay { get; set; }
        
        [Display(Name = "家乡")]
        public string HomeTown { get; set; }
        [Display(Name = "婚姻")]
        public string Marriage { get; set; }
        [Display(Name = "现居住地")]
        public string Position { get; set; }
        [Display(Name = "工作单位")]
        public string Company { get; set; }
        [Display(Name = "工作状况")]
        public int WorkingConditions { get; set; }
        [Display(Name = "兴趣")]
        public string InterestIn { get; set; }
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "QQ")]
        public string QQ { get; set; }
        [Display(Name = "自我介绍")]
        public string IntroduceMyself { get; set; }
        [Display(Name = "入园时间")]
        public string EntryDate { get; set; }
    }
}