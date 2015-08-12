using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolLib.Enum
{
    public enum SearchTypeEnum
    {
        SplitPage = 1,//分页
        Top = 2, //获取前数位记录
        Total = 3 //全部记录
    }

    public enum SearchOrderType
    {
        Asc = 0,
        Desc
    }

    public enum SelectColumnEnum
    {
        All = 0,
        OnlyTitle = 1,//只包括主键，标题等关键信息
        NotIncludeContent = 2, //不包括内容
    }

    public enum ModelReferenceModeEnum
    {
        In, //主键in () 方式
        Join,//联表方式
    }
}
