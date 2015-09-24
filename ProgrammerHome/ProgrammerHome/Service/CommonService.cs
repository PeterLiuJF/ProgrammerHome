using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToolLib.util;

namespace ProgrammerHome.Service
{
    public class CommonService
    {
        public static string GetMaster(int mySpace)
        {
            var master = "~/Views/Shared/master.cshtml";
            switch (mySpace)
            {
                case 0:
                    master = "~/Views/Shared/master.cshtml";
                    break;
                case 1:
                    master = "~/Views/Shared/mymaster.cshtml";
                    break;
                default:
                    break;
            }

            return master;
        }
    }
}