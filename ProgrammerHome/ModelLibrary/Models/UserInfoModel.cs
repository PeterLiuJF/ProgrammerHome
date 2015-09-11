using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelLibrary
{
    public class UserInfoModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string BirthDay { get; set; }
        public string HomeTown { get; set; }
        public string Marriage { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public int WorkingConditions { get; set; }
        public string InterestIn { get; set; }
        public string QQ { get; set; }
        public string IntroduceMyself { get; set; }
        public string EntryDate { get; set; }
    }
}