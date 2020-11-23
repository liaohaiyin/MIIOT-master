
using System.ComponentModel;

namespace MIIOT.DiagManager.Model
{
	/// <summary>
	/// 操作员账号状态
	/// </summary>
	public enum enumUserStatus
	{
		/// <summary>
		/// 正常
		/// </summary>
		[Description("正常")]
		Normal = 0,
		/// <summary>
		/// 停用
		/// </summary>
		[Description("停用")]
		Stop = 1,
		/// <summary>
		/// 锁定
		/// </summary>
		[Description("锁定")]
		Lock = 2,
	}

	/// <summary>
	/// 软件系统类型
	/// </summary>
	public enum enumSystemType
	{
		/// <summary>
		/// 全部
		/// </summary>
		[Description("全部")]
		All = -1,
		/// <summary>
		/// 停车场管理软件
		/// </summary>
		[Description("体外诊断")]
		ParkManager = 0,		
	}

	public enum enumModuleStatus
	{
		[Description("未加载")]
		Unloaded = 0,
		[Description("已加载")]
		Loaded = 1,
		[Description("加载失败")]
		LoadError = 2
	}

	public enum enumConfigValueType
	{
		/// <summary>
		/// 字符串配置
		/// Ex1：字符串最大长度
		/// </summary>
		String = 0,
		/// <summary>
		/// 整数配置
		/// Ex1：最小值
		/// Ex2：最大值
		/// </summary>
		Int32 = 1,
		/// <summary>
		/// 小数配置
		/// Ex1：最小值
		/// Ex2：最大值
		/// </summary>
		Double = 2,
		/// <summary>
		/// 枚举列表控制
		/// Ex1：EnumConfigItem对象的数组 json字符串
		/// </summary>
		Enum = 3,
		/// <summary>
		/// 文件路径配置
		/// Value：文件路径
		/// </summary>
		FilePath = 4,
		/// <summary>
		/// 文件配置（base64存储）
		/// Value：文件名和文件大小信息
		/// Data：文件base64字符串
		/// </summary>
		FileBase64 = 5,
		/// <summary>
		/// 目录路径配置
		/// Value：目录路径
		/// </summary>
		FolderPath = 6,
		/// <summary>
		/// 打开page进行配置
		/// Ex1：Page路径
		/// </summary>
		Page = 7,
		/// <summary>
		/// 目录路径配置
		/// Value：目录路径
		/// </summary>
		Time = 8,
		/// <summary>
		/// 打开page进行配置
		/// Ex1：Page路径
		/// </summary>
		DateTime = 9
	}
}
