using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    public interface Graphic : IDrawElement
    {
        bool PointInShape(int x, int y);

        #region Get bounds
        int getXMiddle();
        int getYMiddle();
        int getLeft();
        int getRight();
        int getTop();
        int getBottom();
        int getWidth();
        int getHeight();
        #endregion

        #region Set bounds
        void setX(int x);
        /// <summary>
        /// Sets the y location
        /// </summary>
        /// <param name="y">The new y value</param>
        void setY(int y);
        /// <summary>
        /// Set the height of the object
        /// </summary>
        /// <param name="height">The height</param>
        void setHeight(int height);
        /// <summary>
        /// Set the width of the object
        /// </summary>
        /// <param name="height">The width</param>
        void setWidth(int width);
        #endregion
    }

    public class Group : Graphic // , IDrawElement
    {
        private List<Graphic> _childGraphics = new List<Graphic>();
        public List<Graphic> getGraphics() { return _childGraphics; }

        public void add(Graphic g)
        { _childGraphics.Add(g); }

        public void remove(Graphic g)
        { _childGraphics.Remove(g); }
        
        public void setList(List<Graphic> graphicList)
        { _childGraphics = graphicList; }

        public void Load(string[] to_load, ref int position)
        {
            // Skip first line (has group)
            while ( position < to_load.Length )
            {   // map to string
                string l = to_load[position++];

                // Skip empty lines :D
                if (l.Equals("")) continue;

                Console.WriteLine("loading: {0}", l);

                // decode string :D remove whitspace
                char[] sep = new char[] { ' '};
                string[] _values = l.Trim().Split(sep, 5);

                if (_values[0] == "group")
                {
                    Console.WriteLine("Adding group");

                    Group _child = new Group();
                    _childGraphics.Add(_child);
                    // Pas as reference, afterwards it will hold a higer position :D
                    _child.Load(to_load, ref position);

                }
                else if (_values[0] == "ellipse")
                {
                    Console.WriteLine("Adding ellipse");

                    // Add an ellipse
                    _childGraphics.Add(
                        new BasicShape(
                            Convert.ToInt16(_values[1]), // x
                            Convert.ToInt16(_values[2]), // y
                            Convert.ToInt16(_values[3]), // width
                            Convert.ToInt16(_values[4]),  // height 
                            Elipse.getShape()
                        )
                    );
                }
                else if (_values[0] == "rectangle")
                {
                    Console.WriteLine("Adding square");
                    // Add and square
                    _childGraphics.Add(
                        new BasicShape(
                            Convert.ToInt16(_values[1]), // x
                            Convert.ToInt16(_values[2]), // y
                            Convert.ToInt16(_values[3]), // width
                            Convert.ToInt16(_values[4]),  // height 
                            Square.getShape()
                        )
                    );
                }
                else if (_values[0] == "ornament")
                {
                    Console.WriteLine("Adding ornament");
                    // TODO 
                }
                else
                    Console.WriteLine("Dont know this string :< {0}", _values[0]);

            } // end for loop
        }


        public bool PointInShape(int x, int y)
        {
            if (getLeft() < x && x < getLeft() + getWidth() &&
                getTop() < y && y < getTop() + getHeight())
                return true;

            return false;
        }

        #region Bounds
        public int getXMiddle() { return (int)(getLeft() + getWidth() / 2); }
        public int getYMiddle() { return (int)(getTop() + getHeight() / 2); }

        public int getLeft()
        {
            // y
            int _value = int.MaxValue;
            foreach (Graphic g in _childGraphics)
                _value = Math.Min(_value, g.getLeft());
            return _value;
        }

        public int getRight()
        {
            int _value = 0;
            foreach (Graphic g in _childGraphics)
                _value = Math.Max(_value, g.getRight());
            return _value;
        }

        public int getTop()
        {
            int _value = int.MaxValue;
            foreach (Graphic g in _childGraphics)
                _value = Math.Min(_value, g.getTop());
            return _value;
        }

        public int getBottom()
        {
            int _value = 0;
            foreach (Graphic g in _childGraphics)
                _value = Math.Max(_value, g.getBottom());
            return _value;
        }

        public int getWidth()
        {
            int _left = getLeft();
            int _right = getRight();

            return _right - _left;
        }

        public int getHeight()
        {
            int _top = getTop();
            int _bottom = getBottom();

            return _bottom - _top;
        }
        #endregion

        #region Set bounds
        public void setX(int x)
        {
            int _change = x - getLeft();
            foreach (Graphic g in _childGraphics)
                g.setX(g.getLeft() + _change);
        }

        public void setY(int y)
        {
            int _change = y - getTop();
            foreach (Graphic g in _childGraphics)
                g.setY(g.getTop() + _change);
        }

        public void setHeight(int height)
        {
            // scaling all childeren
            float _scaled = height / (float)getHeight();
            int top = getTop();

            foreach (Graphic g in _childGraphics)
            {
                g.setHeight((int)(g.getHeight() * _scaled));
                g.setY((int)(top + (g.getTop() - top) * _scaled));
            }
        }

        public void setWidth(int width)
        {
            // scaling all childeren
            float _scaled = width / (float)getWidth();
            int left = getLeft();

            foreach (Graphic g in _childGraphics)
            {
                g.setWidth((int)(g.getWidth() * _scaled));
                g.setX((int)(left + (g.getLeft() - left) * _scaled));
            }
        }
        #endregion

        public void accept(IDrawElementVisitor visitor)
        {
            visitor.visit(this);
            foreach (Graphic g in _childGraphics)
                g.accept(visitor);
                //visitor.visit(g);
            visitor.end_visit(this);
        }
    }

    /// <summary>
    /// Actual shape draw thing implements this
    /// </summary>
    public interface IDrawInterface
    {
        void Draw(Graphics g, Pen p, BasicShape b);
        string toString();
    }

    /// <summary>
    /// Base class for shapes drawn on the screen
    /// </summary>
    public class BasicShape : Graphic//, IDrawElement
    {
        protected int _x, _y, _width, _height;
        private IDrawInterface _interface;

        public BasicShape(int x, int y, int width, int height, IDrawInterface inter)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _interface = inter;
        }

        #region Get bounds
        public int getXMiddle() { return (int)(_x + _width / 2); }
        public int getYMiddle() { return (int)(_y + _height / 2); }
        public int getLeft() { return _x; }
        public int getRight() { return _x + _width; }
        public int getTop() { return _y; }
        public int getBottom() { return _y + _height; }
        public int getWidth() { return _width; }
        public int getHeight() { return _height; }
        #endregion

        /// <summary>
        /// Sets the x location
        /// </summary>
        /// <param name="x">The new x value</param>
        public void setX(int x) { this._x = x; }
        /// <summary>
        /// Sets the y location
        /// </summary>
        /// <param name="y">The new y value</param>
        public void setY(int y) { this._y = y; }
        /// <summary>
        /// Set the height of the object
        /// </summary>
        /// <param name="height">The height</param>
        public void setHeight(int height) { this._height = height; }
        /// <summary>
        /// Set the width of the object
        /// </summary>
        /// <param name="height">The width</param>
        public void setWidth(int width) { this._width = width; }


        /// <summary>
        /// Check if a given x and y is within the shape
        /// </summary>
        /// <param name="x">The x to check</param>
        /// <param name="y">The y to check</param>
        /// <returns>True if x and y are within the bounds of the shape</returns>
        public bool PointInShape(int x, int y)
        {
            if (_x < x && x < _x + _width &&
                _y < y && y < _y + _height)
                return true;

            return false;
        }

        public void Draw(Graphics g, Pen p)
        {
            _interface.Draw(g, p, this);
        }

        public string toString()
        {
            return _interface.toString();
        }

        public void accept(IDrawElementVisitor visitor)
        {
            visitor.visit(this);
        }
    }

    /// <summary>
    /// Implement drawing a square
    /// </summary>
    public class Square : IDrawInterface
    {
        public Square() { }

        private static Square _e = null;
        public static Square getShape()
        {
            if (_e == null)
                _e = new Square();

            return _e;
        }

        public void Draw(Graphics g, Pen p, BasicShape b)
        {
            Rectangle rect = new Rectangle(
                b.getLeft(), b.getTop(), b.getWidth(), b.getHeight()
            );
            g.DrawRectangle(p, rect);
        }

        public string toString()
        {
            return "rectangle";
        }
    }

    /// <summary>
    /// Implment drawing a an elipse
    /// </summary>
    public class Elipse : IDrawInterface
    {
        public Elipse() { }

        private static Elipse _e = null;
        public static Elipse getShape()
        {
            if (_e == null)
                _e = new Elipse();

            return _e;
        }

        public void Draw(Graphics g, Pen p, BasicShape b)
        {
            Rectangle rect = new Rectangle(
                b.getLeft(), b.getTop(), b.getWidth(), b.getHeight()
            );
            g.DrawEllipse(p, rect);
        }

        public string toString()
        {
            return "ellipse";
        }
    }

    public class DrawVisitor : IDrawElementVisitor
    {   
        private Graphics _g;
        private Pen _color;

        public DrawVisitor(Graphics g, Pen color)
        {
            _g = g;
            _color = color;
        }

        public void visit(Group group)
        {
            //foreach (Graphic g in group.getGraphics())
            //    g.accept(this);
        }

        public void visit(BasicShape basic)
        {
            basic.Draw(_g, _color);
        }


        public void end_visit(Group group)
        {
            // Dont do anything
        }
    }

    public class ResizeVisitor : IDrawElementVisitor
    {
        int _x, _y, _width, _height;
        //int _Px, _Py, _Pwidth, _Pheight;
        float _yScaled, _xScaled;
        int _top, _left, _change_x, _change_y;

        public ResizeVisitor(int x, int y, int width, int height, Graphic shape)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;

            // Calculate scalling etc done
            _yScaled = height / (float)shape.getHeight();
            _top = shape.getTop();
            _xScaled = width / (float)shape.getWidth();
            _left = shape.getLeft();
            _change_y = y - shape.getTop();
            _change_x = y - shape.getLeft();

            //_Px = shape.getLeft();
            //_Py = shape.getTop();
            //_Pwidth = shape.getWidth();
            //_Pheight = shape.getHeight();
        }

        public void visit(Group group)
        {
            // Group can't be resized :D
            // Childeren should be resized :-)

            //group.setHeight(_height);
            //group.setWidth(_width);
            //group.setX(_x);
            //group.setY(_y);
        }

        public void end_visit(Group group)
        {
            // Dont do anything
        }
     
        public void visit(BasicShape basic)
        {
            //float _yScale = (float)_height / (float)_Pheight;
            //float _xScale = (float)_width / (float)_Pwidth;

            //Console.WriteLine("x:{0}, y:{1}", _xScale, _yScale);

            //int new_x = (int)(_xScale * (_x - _Px) + _Px);
            //int new_y = (int)(_yScale * (_y - _Py) + _Py);

            //int new_height = (int)(basic.getHeight() * _yScale);
            //int new_width = (int)(basic.getWidth() * _xScale);

            // Calculate translation
            basic.setY(basic.getTop() + _change_y);
            basic.setX(basic.getLeft() + _change_x);

            // Calculate new height
            basic.setHeight((int)(basic.getHeight() * _yScaled));
            basic.setY((int)(_top + (basic.getTop() - _top) * _yScaled));

            // Calculate new width
            basic.setWidth((int)(basic.getWidth() * _xScaled));
            basic.setX((int)(_left + (basic.getLeft() - _left) * _xScaled));

            //basic.setHeight(new_height);
            //basic.setWidth(new_width);
            //basic.setX(new_x);
            //basic.setY(new_y);
        }
    }

    /*
      public void setY(int y)
        {
            int _change = y - getTop();
            foreach (Graphic g in _childGraphics)
                g.setY(g.getTop() + _change);
        }

        public void setHeight(int height)
        {
            // scaling all childeren
            float _scaled = height / (float)getHeight();
            int top = getTop();

            foreach (Graphic g in _childGraphics)
            {
                g.setHeight((int)(g.getHeight() * _scaled));
                g.setY((int)(top + (g.getTop() - top) * _scaled));
            }
        }

     
     */

    public class SaveVisitior : IDrawElementVisitor
    {
        int _depth;
        string _out;

        public SaveVisitior(int depth)
        {
            _depth = depth;
            _out = "";
        }

        public string getString()
        {
            return _out;
        }

        public void visit(Group group)
        {
            _out += String.Format("{0}group {1}" + Environment.NewLine, new String(' ', _depth), group.getGraphics().Count());
            _depth += 1;
            //foreach (Graphic g in group.getGraphics())
            //    g.accept(this);
            //_depth -= 1;
        }

        public void visit(BasicShape basic)
        {
            _out += String.Format(
                "{4}{5} {0} {1} {2} {3}" + Environment.NewLine,
                basic.getLeft(), basic.getTop(), basic.getWidth(), basic.getHeight(), new String(' ', _depth), basic.toString()
            );
        }

        public void end_visit(Group group)
        {
            _depth -= 1;
        }
    }
}
