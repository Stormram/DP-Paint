﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    public interface Graphic
    {
        void Draw(Graphics g, Pen color);
        string Save(int depth);
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

    public class Group : Graphic
    {
        private List<Graphic> _childGraphics = new List<Graphic>();
        public List<Graphic> getGraphics() { return _childGraphics; }

        public void add(Graphic g)
        { _childGraphics.Add(g); }

        public void remove(Graphic g)
        { _childGraphics.Remove(g); }

        public void Draw(Graphics g, Pen color)
        {
            // Draw all childeren, a group doesn't need to be drawn :D
            foreach (Graphic _g in _childGraphics)
                _g.Draw(g, color);
        }

        public string Save(int depth)
        {
            string _out = String.Format("{0}group {1}" + Environment.NewLine, new String(' ', depth), _childGraphics.Count());
            foreach (Graphic g in _childGraphics)
                _out += g.Save(depth+1);
            return _out;
        }

        public void Load(string[] to_load, ref int position)
        {
            // This holds the amount of items this group has!
            int items = Convert.ToInt16(to_load[position].Trim().Split(' ')[1]);

            // Skip first line (has group)
            for (int i = position+1; i < to_load.Length; i++ )
            {   // map to string
                string l = to_load[i];

                // Skip empty lines :D
                if (l == "") continue;

                // decode string :D remove whitspace
                string[] _values = l.Trim().Split(' ');

                if (_values[0] == "group")
                {
                    Group _child = new Group();
                    _childGraphics.Add(_child);
                    // Pas as reference, afterwards it will hold a higer position :D
                    _child.Load(to_load, ref position);

                }
                else if (_values[0] == "ellipse")
                    // Add an ellipse
                    _childGraphics.Add(
                        new Elipse(
                            Convert.ToInt16(_values[1]), // x
                            Convert.ToInt16(_values[2]), // y
                            Convert.ToInt16(_values[3]), // width
                            Convert.ToInt16(_values[4])  // height 
                        )
                    );
                else if (_values[0] == "square")
                    // Add and square
                    _childGraphics.Add(
                        new Square(
                            Convert.ToInt16(_values[1]), // x
                            Convert.ToInt16(_values[2]), // y
                            Convert.ToInt16(_values[3]), // width
                            Convert.ToInt16(_values[4])  // height 
                        )
                    );
                else if (_values[0] == "ornament")
                {
                    // TODO 
                    continue;
                }
                else
                    Console.WriteLine("Dont know this string :< {0}", _values[0]);

                // ornament part wont get here :D
                items--;
                if (items == 0) return;
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
            float _scaled = height / getHeight();
            foreach (Graphic g in _childGraphics)
                g.setHeight((int)(g.getHeight() * _scaled));
        }

        public void setWidth(int width)
        {
            // scaling all childeren
            float _scaled = width / getWidth();
            foreach (Graphic g in _childGraphics)
                g.setWidth((int)(g.getWidth() * _scaled));

        }
        #endregion
    }

    /// <summary>
    /// Base class for shapes drawn on the screen
    /// </summary>
    public abstract class Shape : Graphic
    {
        protected int _x, _y, _width, _height;

        public Shape(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
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
            //Console.WriteLine("{0} {1} -> {2}+{3}, {4}+{5}", x, y, _x, _width, _y, _height);

            if (_x < x && x < _x + _width &&
                _y < y && y < _y + _height)
                return true;

            return false;
        }

        public abstract void Draw(Graphics g, Pen color);
        public abstract string Save(int depth);
    }

    /// <summary>
    /// Implement drawing a square
    /// </summary>
    class Square : Shape
    {
        public Square(int x, int y, int width, int height) :
            base(x, y, width, height) { }

        public override void Draw(Graphics g, Pen color)
        {
            Rectangle rect = new Rectangle(_x, _y, _width, _height);
            g.DrawRectangle(color, rect);
        }

        /// <summary>
        /// Return this as thingy
        /// </summary>
        /// <returns>The thingy as string :D</returns>
        public override string Save(int depth)
        {
            return String.Format(
                "{4}rectangle {0} {1} {2} {3}" + Environment.NewLine, 
                    this.getLeft(), this.getTop(), this.getWidth(), this.getHeight(), new String(' ', depth)
            );
        }
    }

    /// <summary>
    /// Implment drawing a an elipse
    /// </summary>
    class Elipse : Shape
    {
        public Elipse(int x, int y, int width, int height) :
            base(x, y, width, height) { }

        public override void Draw(Graphics g, Pen color)
        {
            Rectangle rect = new Rectangle(_x, _y, _width, _height);
            g.DrawEllipse(color, rect);
        }

        public override string Save(int depth)
        {
            return String.Format(
                "{4}ellipse {0} {1} {2} {3}" + Environment.NewLine, 
                    this.getLeft(), this.getTop(), this.getWidth(), this.getHeight(), new String(' ', depth)
            );
        }
    }
}
