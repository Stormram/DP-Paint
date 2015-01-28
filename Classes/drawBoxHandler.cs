using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Classes
{
    class drawBoxHandler
    {
        /// <summary>
        /// Enum for the different adjustmenst of the shape
        /// </summary>
        private enum moved_box
        {
            TOP,
            LEFT,
            RIGHT,
            BOTTOM,
            MOVE
        };

        private PictureBox _draw_on;
        private ToolStripStatusLabel _count_label;
        private Color _background_color;
        private Pen _draw_color;
        private List<Graphic> _shapes;
        private Form1 _form;

        public drawBoxHandler(Form1 form, PictureBox drawOn, ToolStripStatusLabel count_label) :
            this(form, drawOn, count_label, Color.DimGray, Pens.AliceBlue) { }

        public drawBoxHandler(Form1 form, PictureBox drawOn, ToolStripStatusLabel count_label, Color background_color, Pen draw_color)
        {
            // Add link to where we draw!
            _form = form;
            _draw_on = drawOn;
            _count_label = count_label;
            _background_color = background_color;
            _draw_color = draw_color;
            _shapes = new List<Graphic>();

            // Redraw for background settings!
            Redraw();
        }

        /// <summary>
        /// Redraw all shapes on the screen
        /// </summary>
        public void Redraw()
        {
            using (Graphics g = Graphics.FromImage(_draw_on.Image))
            {
                g.Clear(_background_color);

                DrawVisitor _v = new DrawVisitor(g, _draw_color);

                foreach (Graphic s in _shapes)
                    s.accept(_v);
            }
            _draw_on.Invalidate();
        }

        /// <summary>
        /// Remove all shapes and redraw the screen
        /// </summary>
        public void Clear()
        {
            _shapes = new List<Graphic>();
            Redraw();
        }

        /// <summary>
        /// Called when a shape is moved
        /// </summary>
        /// <param name="first_x">x pos of first click</param>
        /// <param name="first_y">y pos of first click</param>
        /// <param name="second_x">x pos of second click</param>
        /// <param name="second_y">y pos of second click</param>
        /// <param name="selected_shape">the affected shape</param>
        public void viewClicked(int first_x, int first_y, int second_x, int second_y, Graphic selected_shape)
        {
            if (first_x != second_x || first_y != second_y)
                applyMove(first_x, first_y, second_x, second_y, ref selected_shape);

            _shapes = new List<Graphic>(5);
            
            // Top center
            _shapes.Add(new BasicShape(selected_shape.getXMiddle() - 5, selected_shape.getTop() - 5, 10, 10, Square.getShape()));
            _shapes.Add(new BasicShape(selected_shape.getRight() - 5, selected_shape.getYMiddle() - 5, 10, 10, Square.getShape()));
            _shapes.Add(new BasicShape(selected_shape.getXMiddle() - 5, selected_shape.getBottom() - 5, 10, 10, Square.getShape()));
            _shapes.Add(new BasicShape(selected_shape.getLeft() - 5, selected_shape.getYMiddle() - 5, 10, 10, Square.getShape()));
            _shapes.Add(new BasicShape(selected_shape.getLeft(), selected_shape.getTop(), selected_shape.getWidth(), selected_shape.getHeight(), Square.getShape()));

            Redraw();
        }

        /// <summary>
        /// Calculate the movement and apply is to the shape
        /// </summary>
        /// <param name="first_x">x pos of first click</param>
        /// <param name="first_y">y pos of first click</param>
        /// <param name="second_x">x pos of second click</param>
        /// <param name="second_y">y pos of second click</param>
        /// <param name="selected_shape">the affected shape</param>
        public void applyMove(int first_x, int first_y, int second_x, int second_y, ref Graphic selected_shape)
        {
            // Calculate draw positions first
            int size_x = Math.Abs(first_x - second_x);
            int size_y = Math.Abs(first_y - second_y);

            int x = Math.Min(first_x, second_x);
            int y = Math.Min(first_y, second_y);
            
            // Find selected / dragged shape
            int i;
            for (i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i].PointInShape(first_x, first_y) &&
                    x != selected_shape.getLeft() && y != selected_shape.getTop())
                    break;
            }

            // We dragged nothing
            if (i == _shapes.Count)
            {
                Console.WriteLine("Nothing got clicked");
                return;
            }

            moved_box move = getMovedBox(_shapes[i], selected_shape);

            int change_x = first_x - second_x;
            int change_y = first_y - second_y;

            Console.WriteLine("{0} ({1},{2})", move, change_x, change_y);

            switch (move)
            {
                case moved_box.BOTTOM:
                    selected_shape.setHeight(selected_shape.getHeight() - change_y);
                    break;
                case moved_box.LEFT:
                    selected_shape.setX(selected_shape.getLeft() - change_x);
                    selected_shape.setWidth(selected_shape.getWidth() + change_x);
                    break;
                case moved_box.RIGHT:
                    selected_shape.setWidth(selected_shape.getWidth() - change_x);
                    break;
                case moved_box.TOP:
                    selected_shape.setY(selected_shape.getTop() - change_y);
                    selected_shape.setHeight(selected_shape.getHeight() + change_y);
                    break;
                case moved_box.MOVE:
                    selected_shape.setX(selected_shape.getLeft() - change_x);
                    selected_shape.setY(selected_shape.getTop() - change_y);
                    break;
            }
            Redraw();
        }   

        /// <summary>
        /// Find out which square got clicked for a movement
        /// </summary>
        /// <param name="box">A box for dragging/resizing</param>
        /// <param name="selected">A shape which is selected</param>
        /// <returns></returns>
        private moved_box getMovedBox(Graphic box, Graphic selected)
        {
            if (box.getTop() < selected.getTop() && box.getBottom() > selected.getTop())
                return moved_box.TOP;

            if (box.getLeft() < selected.getLeft())
                return moved_box.LEFT;

            if (box.getRight() > selected.getRight())
                return moved_box.RIGHT;

            if (box.getBottom() > selected.getBottom())
                return moved_box.BOTTOM;

            return moved_box.MOVE;
        }

        /// <summary>
        /// Create a new shape on the screen with a size
        /// </summary>
        /// <param name="first_x">x pos of first click</param>
        /// <param name="first_y">y pos of first click</param>
        /// <param name="second_x">x pos of second click</param>
        /// <param name="second_y">y pos of second click</param>
        /// <param name="tool">What tool is used</param>
        public void viewClicked(int first_x, int first_y, int second_x, int second_y, selected_tool tool)
        {
            if (tool != selected_tool.SELECT && tool != selected_tool.GROUP)
                return;

            // Calculate draw positions first
            int size_x = Math.Abs(first_x - second_x);
            int size_y = Math.Abs(first_y - second_y);

            int x = Math.Min(first_x, second_x);
            int y = Math.Min(first_y, second_y);

            selectTool(x, y);
        }

        public void addShape(Graphic shape)
        {
            _shapes.Add(shape);

            Redraw();
            countToolTip();
        }

        public BasicShape createShape(int first_x, int first_y, int second_x, int second_y, selected_tool tool)
        {
            // Calculate draw positions first
            int size_x = Math.Abs(first_x - second_x);
            int size_y = Math.Abs(first_y - second_y);

            int x = Math.Min(first_x, second_x);
            int y = Math.Min(first_y, second_y);

            BasicShape new_shape;
            switch (tool)
            {
                case selected_tool.SQUARE:
                    new_shape = new BasicShape(x, y, size_x, size_y, Square.getShape());
                    break;
                case selected_tool.ELIPSE:
                    new_shape = new BasicShape(x, y, size_x, size_y, Elipse.getShape());
                    break;
                default:
                    return null;
            }

            // Dont allow stacked vars? Clicking creates shapes but doesn't work nice :<
            // Check this here since we have other tools aswell!
            if (first_x == second_x || first_y == second_y)
                return null;

            return new_shape; 
        }

        public void remove(Graphic shape)
        {
            _shapes.Remove(shape);

            Redraw();
            countToolTip();
        }

        /// <summary>
        /// When the select tool is used find out on which shape
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void selectTool(int x, int y)
        {
            for(int i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i].PointInShape(x, y))
                    _form.item_selected(_shapes[i]);
            }
        }

        /// <summary>
        /// Tooltip showing all the present objects
        /// </summary>
        private void countToolTip()
        {
            if (_count_label == null)
                return;

            _form.UIThread(() => _count_label.Text = "Objects: " + _shapes.Count);
        }

        /// <summary>
        /// Create a string to write to file using defined language
        /// </summary>
        /// <returns>The drawing as string</returns>
        public string SaveAsString()
        {
            //string result = "";
            SaveVisitior _v = new SaveVisitior(0);
            foreach (Graphic _shape in _shapes)
                _shape.accept(_v);

            return _v.getString();
        }

        /// <summary>
        /// Loads a string drawing as the current painting
        /// </summary>
        /// <param name="to_load"></param>
        public void LoadString(string to_load)
        {
            int _val = 0;
            Group _tempGroup = new Group();
            _tempGroup.Load(to_load.Split(Environment.NewLine.ToCharArray()), ref _val);
            
            // The first group can be dropped since it's added by us :D
            _shapes = _tempGroup.getGraphics();
            
            Redraw();
        }
    }
}
