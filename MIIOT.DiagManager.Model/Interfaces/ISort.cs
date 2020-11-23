using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 排序支持
    /// </summary>
    public interface ISort
    {
        /// <summary>
        /// 排序序号
        /// </summary>
        int SortNo { get; set; }
    }

    /// <summary>
    /// 排序支持
    /// </summary>
    public interface IGroupSort
    {
        /// <summary>
        /// 分组排序序号
        /// </summary>
        int GroupSortNo { get; set; }
    }

    /// <summary>
    /// 升序
    /// </summary>
    public class SortAscComparer : IComparer<ISort>
    {
        public int Compare(ISort x, ISort y)
        {
            return (x.SortNo.CompareTo(y.SortNo));
        }
    }
    /// <summary>
    /// 降序
    /// </summary>
    public class SortDescComparer : IComparer<ISort>
    {
        public int Compare(ISort x, ISort y)
        {
            return (y.SortNo.CompareTo(x.SortNo));
        }
    }

    /// <summary>
    /// 升序
    /// </summary>
    public class GroupSortAscComparer : IComparer<IGroupSort>
    {
        public int Compare(IGroupSort x, IGroupSort y)
        {
            return (x.GroupSortNo.CompareTo(y.GroupSortNo));
        }
    }
    /// <summary>
    /// 降序
    /// </summary>
    public class GroupSortDescComparer : IComparer<IGroupSort>
    {
        public int Compare(IGroupSort x, IGroupSort y)
        {
            return (y.GroupSortNo.CompareTo(x.GroupSortNo));
        }
    }
}
