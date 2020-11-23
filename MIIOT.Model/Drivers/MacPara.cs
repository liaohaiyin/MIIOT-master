using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class MacPara : BaseNotify
    {
        private string _macType;

        public string MacType
        {
            get { return _macType; }
            set
            {
                _macType = value;
                OnPropertyChanged("MacPara");
            }
        }
        private bool _active;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }
        private string _MacKey;

        public string MacKey
        {
            get { return _MacKey; }
            set
            {
                _MacKey = value;
                OnPropertyChanged("MacKey");
            }
        }
        private string _cabinet;

        public string Cabinet
        {
            get { return _cabinet; }
            set
            {
                _cabinet = value;
                OnPropertyChanged("Cabinet");
            }
        }
        private string _com;

        public string COM
        {
            get { return _com; }
            set
            {
                _com = value;
                OnPropertyChanged("COM");
            }
        }
        private string _baudRate;

        public string BaudRate
        {
            get { return _baudRate; }
            set
            {
                _baudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        private double _timeOut;

        public double TimeOut
        {
            get { return _timeOut; }
            set
            {
                _timeOut = value;
                OnPropertyChanged("TimeOut");
            }
        }
        private string _para1;

        public string Para1
        {
            get { return _para1; }
            set { _para1 = value;
                OnPropertyChanged("Para1");
            }
        }

        private string _para2;

        public string Para2
        {
            get { return _para2; }
            set
            {
                _para2 = value;
                OnPropertyChanged("Para2");
            }
        }
        public int Id { get; set; }
        public bool isConnect { get; set; }
    }
}
