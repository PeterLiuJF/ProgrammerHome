using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ToolLib.Enum
{
    public enum DbTypeEnum
    {
        None = 0,
        MsSql = 1,
        Oracle = 2,
        MySql = 3,
    }

    public enum LanguageTypeEnum
    {
        None = 0,
        Chinese = 1,
        English = 2,
        Japanese = 3,
    }

    public enum StatusEnum
    {
        [Description("已删除")]
        Deleted = -1,//已删除
        None = 0,
        [Description("正常")]
        Active = 1, //正常
        Hide = 2, //隐藏
        Completed = 3,
        Send = 4,
        CompletePayment = 5,
        Cancel = 6,
    }

    public enum RefoundStatusEnum
    {
        None = 0,
        Success = 1,
        Fail = 2,
    }

    public enum RefoundTypeEnum
    {
        None = 0,
        ShopCartRefoundType = 1,
        ItemRefoundType = 2,
    }

    public enum GenderEnum
    {
        //[Description("未设置", true)]
        //None = 0,
        [Description("男")]
        Male = 1,
        [Description("女")]
        Female = 2
    }

    public enum ReturnEnum
    {
        Error = -1,
        None = 0,
        Success = 1,
        HasChild = 2,
    }

    public enum PublishStatusEnum
    {
        //[Description("未设置", true)]
        //None = 0,
        [Description("已发布")]
        Active = 1, //正常
        [Description("未发布")]
        Pending = 2, //未发布
        [Description("已撤销")]
        OffLine = 3, //下线 
    }

    public enum PageTypeEnum
    {
        None = 0,
        Page = 1,
        Part = 2,
    }

    public enum TableTypeEnum
    {

        None = 0,
        FormTable = 1,
        Table = 2,
        DataList = 3,
    }

    public enum EditFormModeEnum
    {
        Edit,
        View,
        Approve,
        PopBatchApprove,//弹出框批量审核
        PopEdit,//弹出框表单
        ViewWithoutTitle,//查看不带标题
        InAsynPart,//异步获取页面布局
        EditSimple, //没有标题
    }


    public enum TemplateCaseTypeEnum
    {
        None = 0,
        Even = 1,//偶数
        Last = 2,//最后一个
        First = 3,//第一个

    }

    public enum DataModelTypeEnum
    {
        None = 0,
        Entity = 1,
        Enumeration = 2,
    }

    public enum PageActionEnum
    {
        //[Description("", true)]
        //None = 0,
        [Description("Create")]
        Create = 1,
        [Description("Modify")]
        Modify = 2,
        [Description("Delete")]
        Delete = 3,
        [Description("View")]
        View = 4,
        [Description("ViewList")]
        ViewList = 5,
        [Description("Search")]
        Search = 6,
        [Description("RequestSeriveData")]
        RequestSeriveDataToClient = 7,
        [Description("All")]
        All = 8,//匹配任何页面状态
        [Description("Process")]
        Process = 9,
    }


    public enum PageJsPosition
    {
        None = 0,
        DocumentReady = 1,
        Function = 2,
        IncludeJsFile = 3,
    }

    public enum PageDataOpsCache
    {
        None = 0,
        FrontEnd = 1,
        Service = 2,
    }

    public enum PageDataDependentType
    {
        None = 0,
        List = 1,
        IntDictionary = 2,
        StringDictionary = 3,
    }


    public enum PageRankType
    {
        None = 0,
        HorizontalRound = 1,
    }

    public enum TextModeEnum
    {
        None = 0,
        Text = 1,
        MultiLine = 2,
        Password = 3,
    }

    public enum OpsSide
    {
        None = 0,
        FrontEnd = 1,
        Service = 2,
    }

    public enum BindTypeEnum
    {
        //[Description("", true)]
        //None = 0,
        [Description("同步")]
        Synchronous = 1,
        [Description("异步")]
        Asynchronous = 2,
    }

    public enum OpsPlaceEnum
    {
        None = 0,
        Service = 1,
        FrontEnd = 2,
    }

    public enum MethodTypeEnum
    {
        None = 0,
        List = 1,
        Search = 2,
        Get = 3,
        Execute = 4,
    }

    public enum DefaultMethodTypeEnum
    {
        None = 0,
        Get,
        Create,
        Update,
        Delete,
        Search,
        List,
        KeywordFilter,
    }

    public enum BindItemTypeEnum
    {
        None = 0,
        Normal = 1,
        Key = 2,
    }

    public enum BindModeEnum
    {
        None = 0,
        Normal = 1,
        Repeater = 2,
    }

    public enum TemplateBindMode
    {

        None = 0,
        Normal = 1,
        /// <summary>
        /// 满足就显示模板
        /// </summary>
        MatchBoolean = 2,
    }

    /// <summary>
    /// 输出模式
    /// </summary>
    public enum GenerateModeEnum
    {
        None = 0,
        Normal = 1,
        /// <summary>
        /// JS拼接
        /// </summary>
        JsJoint = 2,
        /// <summary>
        /// 异步获取，需要再节点上定义数据源
        /// </summary>
        Ajax = 3,
        PopUp = 4,

    }

    public enum TriggerTypeEnum
    {
        None = 0,
        Increment = 1,//增加
        Open = 2,//打开
        SelectDate = 3,//选择日期
        RemoveElement = 4,//移除页面元素
        OpenEdit = 5,//打开已存在的编辑
        CallBack = 6, //回调
        ReturnData = 7, //返回数据
        Before = 8,//执行之前
    }

    public enum TargetRangeEnum
    {
        None = 0,
        All = 1,
        ThisBlock = 2,
    }

    public enum GenerateRoleEnum
    {
        None = 0,
        OpenContent = 1,//弹出框内容
        DisplayContent = 2,//显示内容
    }

    public enum ButtonModeEnum
    {
        None = 0,
        Submit = 1,
        AjaxSubmit = 2,
        Get = 3,
        AjaxGet = 4,
        IncrementElements = 5,
        CloseWindow = 6,
        OpenWindow = 7,
        BindOpenWindow = 8,
        BindAjaxSubmit = 9,
        BindGet = 10,
    }

    public enum LineBtnTypeEnum
    {
        None = 0,
        All = 1,
        OnlyNormal = 2,
        OnlyPopupApprove = 3,
    }

    public enum ButtonTypeEnum
    {
        None,
        Normal = 1,
        WorkFlowButtonList = 2,
    }

    public enum PageExecuteType
    {
        None = 0,
        Post = 1,
        Get = 2,
        AfterPost = 3,
        AfterDependentModelPost = 4,
    }

    public enum ExecuteParameterType
    {
        None = 0,
        Required = 1, //必须
        Optional = 2,//可选
    }

    public enum LabelTypeEnum
    {
        None,
        FormTableColLabel = 1,
        SpanLabel = 2,
        Text = 3,
    }

    public enum LineItemShowMode
    {
        None,
        HideLeft = 1,
    }


    public enum ModelConfigTypeEnum
    {
        None,
        Singleton, //只能一个实例
        Iterator, //多个实例
    }

    public enum PageDataModelPropertyEvent
    {
        None,
        InitModel = 1,
        BeforeExecute = 2,
    }

    public enum TabSourceTypeEnum
    {
        None = 0,
        XmlNode = 1,
        FromApp = 2,
        XmlFile = 3,
    }

    public enum ModelInstanceTypeEnum
    {
        None = 0,
        Single = 1,
        List = 2,
    }




    public enum GetParameterEnum
    {
        None = 0,
        QueryString,
        ForeignKey,
        Property,
        CurrentUser,
        RequestForm,
        StaticValue,
        ForeignMethod,//执行外部对象的方法
        ModelMethod,//执行本对象的方法
        Normal,//普通综合性执行方法
        SingleTableListForm,//根据单列表提交数据，多用于List对象
        RemoveCache,//执行本对象相关的逻辑层中 "RemoveCache" 方法，多用于AfterPost
        ExcuteExtentPackageMethod, //执行扩展包里的函数
        SendEmail,
    }

    /// <summary>
    /// Filter过滤传参的对象
    /// </summary>
    public enum FilterParameterTypeEnum
    {
        None = 0,
        QueryString,//传querystring
        Property,
        SearchPropery,
    }

    public enum ParameterValueTypeEnum
    {
        None = 0,
        Int32,
        String,
        DateTime,
    }

    public enum FilterModeEnum
    {
        Equal,
        In,
    }

    /// <summary>
    /// 查看执行条件
    /// </summary>
    public enum ViewConditionTypeEnum
    {
        None,
        DependentModelPropertyValue,
        DependentModelPropertyInValues, //属于属性字符串中，如 11,12,212  11就符合条件
    }

    public enum ViewParameterTypeEnum
    {
        None,
        GetQueryStringParameter, //获取对象时参数
        SelectText, //下拉框等时用
        SelectValue,
        GetPropertyParameter,
        StaticValueParameter,
        SelectTriggerValue,
        FilterQueryStringParameter,//通过URL参数过滤数据
        FilterTableListTransferBatchParameter,//通过列表的批量按钮传来的值
        FilterExsitsInReferModel,//通过相关对象中的某字段匹配过滤，如 exists(select * from  [table] where [column]=xxx)
        FilterPostParameter,//通过Post方式传值过滤数据
        FilterGrandTableListTransferValue,//通过获得爷爷级的列表传值过滤
        FilterExsitsInReferModelByGrandTransferValue,//通过相关对象中的某字段匹配过滤,过滤数据来自爷爷级TableList，如 exists(select * from  [table] where [column]=xxx)
        FilterTableListTransfer,
    }

    /// <summary>
    /// 格式化模版
    /// </summary>
    public enum StringFormatTemplateEnum
    {
        None,
        Date_yyyy_mm_dd,
        DateTime_yyyy_mm_dd_hh_mm_ss,
        Tick,
        WorkFlowNodeDescription,
        UserTrueName,
        UserName,
        Decimal_0_00,
        Decimal_0_0,
        Time_HH,

    }

   




}
