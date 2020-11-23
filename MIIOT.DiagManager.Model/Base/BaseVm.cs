using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    [Serializable]
    public class BaseVm : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性值改变事件
        /// </summary>
        /// 

   
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 触发属性值改变事件
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
