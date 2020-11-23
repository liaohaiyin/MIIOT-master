using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/28 9:44:02
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_room:BaseRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomno { get; set; }
        /// <summary>
        /// 手术间1
        /// </summary>
        public string roomname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string status { get; set; }
        private ObservableCollection<spd_surgeryinfo> _pub_Surgeries = new ObservableCollection<spd_surgeryinfo>();
        [Description("External")]
        public ObservableCollection<spd_surgeryinfo> pub_Surgeries
        {
            get { return _pub_Surgeries; }
            set
            {
                _pub_Surgeries = value;
                OnPropertyChanged("pub_Surgeries");
            }
        }
    }
}
