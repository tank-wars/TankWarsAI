using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using GameClient.Foundation;
using GameClient.Network;
using GameClient.Network.Communicator;
using GameClient.GameDomain;
using System.Windows.Forms;

namespace GameClient.GUI
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

        private Brush BulletBrush
        {
            get
            {
                return Brushes.Gold;
            }

        }
        //Set of brushes that will be chosen in sequence to pain each players tank
        private Brush[] TankBrushes
        {
            get
            {
                return new Brush[] { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Orange, Brushes.Yellow, Brushes.Purple };
            }
        }

        private Brush WaterBrush
        {
            get
            {
                return Brushes.CornflowerBlue;
            }
        }
        private Brush BrickBrush
        {
            get
            {
                return Brushes.SaddleBrown;
            }
        }

        private Brush CoinBrush
        {
            get { return Brushes.Yellow; }
        }

        private Brush MediBrush
        {
            get { return Brushes.Red; }
        }

       
        private Brush StoneBrush
        {
            get
            {
                return Brushes.Silver;
            }
        }
        private GraphicsPath BulletShape
        {
            get
            {
                GraphicsPath shape = new GraphicsPath();
                shape.AddEllipse(-0.125f, -0.625f, 0.25f, 0.25f);
                return shape;
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

       
        //Draw the Grid in Background
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


        //Fills the cell at location using given brush
        //Cells are half filled based on health condition
        private void FillCell(Coordinate location, Brush brush, int health)
        {
            
            //obtain shape
            GraphicsPath shape = new GraphicsPath();
            switch (health)
            {
                case 0:
                    shape.AddRectangle(new RectangleF(-0.5f, -0.5f, 1, 1));
                    break;
                case 1:
                    shape.AddRectangle(new RectangleF(-0.5f, -0.25f, 1, 0.75f));
                    break;
                case 2:
                    shape.AddRectangle(new RectangleF(-0.5f, 0, 1, 0.5f));
                    break;
                case 3:
                    shape.AddRectangle(new RectangleF(-0.5f, 0.25f, 1, 0.25f));
                    break;
               
            }
            

            Matrix shapeTransform = new Matrix();
            //obtain location
            Point loc = getCellCenter(location.Y, location.X);

            //transform the shape 
            shapeTransform.Translate(loc.X, loc.Y);
            shapeTransform.Scale(CellSize.Width, CellSize.Height);


            shape.Transform(shapeTransform);
            //fill the cell
            graphics.FillPath(brush, shape);
        }

        private void FillCell(Coordinate location, Brush brush)
        {
            FillCell(location, brush, 0);
        }
        public void DrawCoin(Coordinate location)
        {
            Brush brush = CoinBrush;

            FillCell(location, brush);

        }

        public void DrawMedi(Coordinate location)
        {
            Brush brush = MediBrush;

            FillCell(location, brush);

        }

        public void DrawBrick(Coordinate location, int health)
        {
            Brush brush = BrickBrush;
            FillCell(location, brush,health);

        }
        public void DrawStone(Coordinate location)
        {
            Brush brush = StoneBrush;
            FillCell(location, brush);

        }

        public void DrawWater(Coordinate location)
        {
            Brush brush = WaterBrush;
            FillCell(location, brush);

        }

        public void DrawTank(int colorIndex, Coordinate location, Direction heading, bool isShooting)
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

            if(isShooting)
            {
                GraphicsPath bullet = BulletShape;
                bullet.Transform(shapeTransform);
                graphics.FillPath(BulletBrush, bullet);
            }

        }

       
    }
}
