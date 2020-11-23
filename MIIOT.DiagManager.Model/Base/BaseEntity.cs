using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    [Serializable]
    public class BaseEntity : BaseVm, IEntity, INotifyPropertyChanged
    {
        /// <summary>
        /// 无效ID
        /// </summary>
        public static readonly int InvalidID = -1;


        private Int64 _id;
        /// <summary>
        /// ID
        /// </summary>
        public Int64 ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged();
            }
        }
    }
}
