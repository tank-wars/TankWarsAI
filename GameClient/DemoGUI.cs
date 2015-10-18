using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GameClient
{
    /*
    This is a temporary class written to demonstrate GUI at Evaluation
    */
    class DemoGUI
    {
        /*Section containing base parameters*/
        private const int ColumnCount = 10;
        private const int RowCount = 10;

        //Pen used to draw the grid
        private Pen GridPen
        {
            get
            {
                return new Pen(Color.DarkGray, 2);
            }
        }

        private Brush[] TankBrushes
        {
            get
            {
                return new Brush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Yellow, Brushes.Purple };

            }
        }

        private GraphicsPath TankShape
        {
            get
            {
                GraphicsPath shape = new GraphicsPath();
                shape.AddPolygon(new PointF[] { new PointF(0, -0.5f), new PointF(0.5f, 0), new PointF(0.5f, 0.5f), new PointF(-0.5f, 0.5f), new PointF(-0.5f, 0) });
                return shape;
            }
        }

        /* End of section */

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
                return new Size(size / ColumnCount, size /RowCount);
            }
        }

        private Size GridSize
        {
            get
            {
                return new Size(RowCount * CellSize.Width, ColumnCount * CellSize.Height);
            }
        }
        private Point getCellCenter(int row, int column)
        {
            if (row >= RowCount)
                throw new Exception("row should be less than Vertical Cell Count");
            if (column >= ColumnCount)
                throw new Exception("row should be less than Horizontal Cell Count");
            int x = column * CellSize.Width + (CellSize.Width / 2);
            int y = row * CellSize.Height + (CellSize.Height / 2);
            x += origin.X;
            y += origin.Y;
            return new Point(x, y);
        }

       

        public void DrawGrid()
        {
            for(int r = 0; r <= RowCount; r++)
            {
                graphics.DrawLine(GridPen, origin.X, r * CellSize.Height + origin.Y, origin.X + GridSize.Width, r * CellSize.Height + origin.Y);
            }
            for (int c = 0; c <= ColumnCount; c++)
            {
                graphics.DrawLine(GridPen, origin.X + c*CellSize.Width, origin.Y, origin.X + c * CellSize.Width, GridSize.Height+ origin.Y);
            }
        }

        private static float DirectionToDegrees(Direction direction)
        {
            switch(direction)
            {
                case Direction.North:
                    return 0;
                case Direction.East:
                    return 90;
                case Direction.South:
                    return 180;
                case Direction.West:
                    return 270;
            }
            throw new Exception("Unidentified Direction");
        }

        public void DrawTank(int colorIndex, Coordinate location, Direction heading)
        {
            //obtain brush
            Brush[] tankBrushes = TankBrushes;
            Brush brush = tankBrushes[colorIndex % tankBrushes.Length];

            //obtain shape
            GraphicsPath shape = TankShape;

            Matrix shapeTransform = new Matrix();
            //obtain location
            Point loc = getCellCenter(location.Y, location.X);
            
            //transform the shape 
            shapeTransform.Translate(loc.X, loc.Y);
            shapeTransform.Rotate(DirectionToDegrees(heading));
            shapeTransform.Scale(CellSize.Width, CellSize.Height);
            

      
            
            shape.Transform(shapeTransform);
            //Draw the tank
            graphics.FillPath(brush, shape);

        }

    }
}
