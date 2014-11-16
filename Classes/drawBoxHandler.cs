using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Classes
{
    class drawBoxHandler
    {
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
        private List<Shape> _shapes;
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
            _shapes = new List<Shape>();

            // Redraw for background settings!
            Redraw();
        }

        public void Redraw()
        {
            using (Graphics g = Graphics.FromImage(_draw_on.Image))
            {
                g.Clear(_background_color);
                foreach (Shape s in _shapes)
                    s.Draw(g, _draw_color);
            }
            _draw_on.Invalidate();
        }

        public void Clear()
        {
            _shapes = new List<Shape>();
            Redraw();
        }

        public void viewClicked(int first_x, int first_y, int second_x, int second_y, Shape selected_shape)
        {

            // TODO: write select things for it
            if (first_x != second_x || first_y != second_y)
                applyMove(first_x, first_y, second_x, second_y, ref selected_shape);

            _shapes = new List<Shape>(5);
            
            // Top center
            _shapes.Add(new Square(selected_shape.getXMiddle() - 5, selected_shape.getTop() - 5, 10, 10));
            _shapes.Add(new Square(selected_shape.getRight() - 5, selected_shape.getYMiddle() - 5, 10, 10));
            _shapes.Add(new Square(selected_shape.getXMiddle() - 5, selected_shape.getBottom() - 5, 10, 10));
            _shapes.Add(new Square(selected_shape.getLeft() - 5, selected_shape.getYMiddle() - 5, 10, 10));
            _shapes.Add(new Square(selected_shape.getLeft(), selected_shape.getTop(), selected_shape.getWidth(), selected_shape.getHeight()));

            Redraw();
        }

        public void applyMove(int first_x, int first_y, int second_x, int second_y, ref Shape selected_shape)
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
                return;

            moved_box move = getMovedBox(_shapes[i], selected_shape);

            int change_x = first_x - second_x;
            int change_y = first_y - second_y;

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
        }   

        private moved_box getMovedBox(Shape box, Shape selected)
        {
            if (box.getTop() < selected.getTop() && box.getBottom() > selected.getTop())
                return moved_box.TOP;

            if (box.getLeft() < selected.getLeft())
                return moved_box.LEFT;

            if (box.getRight() > selected.getRight())
                return moved_box.RIGHT;

            if (box.getBottom() > selected.getBottom())
                return moved_box.BOTTOM;

            // Wont get here in current implementation
            return moved_box.MOVE;
        }

        public void viewClicked(int first_x, int first_y, int second_x, int second_y, selected_tool tool)
        {
            // Calculate draw positions first
            int size_x = Math.Abs(first_x - second_x);
            int size_y = Math.Abs(first_y - second_y);

            int x = Math.Min(first_x, second_x);
            int y = Math.Min(first_y, second_y);

            Shape new_shape;
            switch (tool)
            {
                case selected_tool.SQUARE:
                    new_shape = new Square(x, y, size_x, size_y);
                    break;
                case selected_tool.ELIPSE:
                    new_shape = new Elipse(x, y, size_x, size_y);
                    break;
                case selected_tool.SELECT:
                    selectTool(x, y);
                    return;
                default:
                    return;
            }

            // Dont allow stacked vars? Clicking creates shapes but doesn't work nice :<
            // Check this here since we have other tools aswell!
            if (first_x == second_x || first_y == second_y)
                return;

            _shapes.Add(new_shape);

            Redraw();
            countToolTip();
        }

        private void selectTool(int x, int y)
        {
            for(int i = 0; i < _shapes.Count; i++)
            {
                if (_shapes[i].PointInShape(x, y))
                    _form.item_selected(_shapes[i]);
            }
        }

        private void countToolTip()
        {
            if (_count_label == null)
                return;

            _form.UIThread(() => _count_label.Text = "Objects: " + _shapes.Count);
        }
    }
}
