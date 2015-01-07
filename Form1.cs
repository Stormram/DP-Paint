using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;
using WindowsFormsApplication1.Classes;
using System.IO;

namespace WindowsFormsApplication1
{
    public enum selected_tool {
        SQUARE,
        ELIPSE,
        GROUP,
        CAPTION,
        SELECT
    };

    public partial class Form1 : Form
    {
        private int X;
        private int Y;
        private selected_tool _cur_tool;
        private drawBoxHandler _draw_handler;
        private drawBoxHandler _draw_handler_hidden;
        private CommandHandler _commandHandler;

        // Move/resize stuff
        private Graphic _selected = null;
        private ChangeCommand _change = null;

        public Form1()
        {
            InitializeComponent();
            init_draw_box();
            set_tool(selected_tool.SQUARE);

            draw_box_hidden.BackColor = Color.Transparent;
            draw_box_hidden.Parent = draw_box;
            draw_box_hidden.Location = new Point(0, 0);

            _draw_handler = new drawBoxHandler(this, this.draw_box, this.object_count_text);
            _draw_handler_hidden = new drawBoxHandler(this, this.draw_box_hidden, null, Color.Transparent, Pens.Aqua);
            _commandHandler = new CommandHandler();
        }

        private void init_draw_box()
        {
            draw_box.Image = new Bitmap(draw_box.Width, draw_box.Height);
            draw_box_hidden.Image = new Bitmap(draw_box.Width, draw_box.Height);
        }

        private void draw_box_MouseDown(object sender, MouseEventArgs e)
        {
            this.X = e.X;
            this.Y = e.Y;
        }

        private void draw_box_MouseUp(object sender, MouseEventArgs e)
        {
            if (_selected == null)
                if (_cur_tool != selected_tool.SELECT)
                {
                    Shape _s = _draw_handler.createShape(X, Y, e.X, e.Y, _cur_tool);
                    if (_s != null)
                        _commandHandler.Add(new CreateCommand(_draw_handler, _s));
                }
                else
                    _draw_handler.viewClicked(X, Y, e.X, e.Y, _cur_tool);
            else
                _draw_handler_hidden.viewClicked(X, Y, e.X, e.Y, _selected);
        }

        private void set_tool(selected_tool new_tool)
        {
            applyToolStripMenuItem.Enabled = false;
            _cur_tool = new_tool;
            this.selected_tool_text.Text = "Selected tool: " + _cur_tool.ToString().ToLower();
        }

        public void item_selected(Graphic item)
        {
            applyToolStripMenuItem.Enabled = true;
            _selected = item;
            _change = new ChangeCommand(_draw_handler, _selected);

            toolsToolStripMenuItem.Enabled = false;

            _draw_handler_hidden.viewClicked(0, 0, 0, 0, _selected);
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.SQUARE); }
        private void elipseToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.ELIPSE); }
        private void groupToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.GROUP); }
        private void captionToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.CAPTION); }
        private void selectToolStripMenuItem_Click(object sender, EventArgs e) { set_tool(selected_tool.SELECT); }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _commandHandler.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _commandHandler.Redo();
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _change.finished(_selected.getLeft(), _selected.getTop(), _selected.getWidth(), _selected.getHeight());
            _commandHandler.Add(_change);

            _change = null;
            _selected = null;
            _draw_handler_hidden.Clear();
            //_draw_handler.Redraw();
            applyToolStripMenuItem.Enabled = true;
            toolsToolStripMenuItem.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // save clicked!
            string data = _draw_handler.SaveAsString();

            // Create save window
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Paint file|*.pnt";
            sfd.FileName = "painting";
            sfd.Title = "Save painting";

            // If ok write to file :D
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamWriter bw = new StreamWriter(File.Create(sfd.FileName)))
                {
                    bw.Write(data);
                    bw.Close();
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // load clicked
            // Create load window
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "Paint file|*.pnt";
            sfd.FileName = "painting";
            sfd.Title = "Load painting";

            string data = "";

            // Read the file
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (StreamReader bw = new StreamReader(File.Open(sfd.FileName, FileMode.Open)))
                {
                    data = bw.ReadToEnd();
                    bw.Close();
                }
            }
            
            // import it!
            _draw_handler.LoadString(data);
        }
    }
}
