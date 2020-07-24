using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    public class WindowWidth
    {
        private int m_width;

        public WindowWidth(int windowWidth)
        {
            m_width = windowWidth;
        }

        public int Val
        {
            get { return m_width; }
        }
    }
}
