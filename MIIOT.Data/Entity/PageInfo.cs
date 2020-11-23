using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    public class PageInfo
    {
        public int Page { get; }
        public int Size { get; }

        public PageInfo(int page, int size)
        {
            this.Page = page;
            this.Size = size;
        }
    }
}
