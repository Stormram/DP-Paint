using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    /// <summary>
    /// Base class for shapes drawn on the screen
    /// </summary>
    public abstract class Shape
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
    }
}
