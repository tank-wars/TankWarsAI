using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GameClient
{
    class DemoGUI
    {
        private const int HorizontalCellCount = 20;
        private const int VerticalCellCount = 20;

        private Graphics graphics;
        private Point origin;
        private int size;

        public DemoGUI(Graphics graphics, Point origin, int size)
        {
            this.graphics = graphics;
            this.origin = origin;
            this.size = size;
        }

        private Size CellSize
        {
            get
            {
                return new Size(size / HorizontalCellCount, size / VerticalCellCount);
            }
        }

        private Point getCellCenter(int row, int column)
        {
            if (row >= VerticalCellCount)
                throw new Exception("row should be less than Vertical Cell Count");
            if (column >= HorizontalCellCount)
                throw new Exception("row should be less than Horizontal Cell Count");

        }

    }
}
