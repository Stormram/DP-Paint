namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.object_count_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.selected_tool_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.draw_box = new System.Windows.Forms.PictureBox();
            this.draw_box_hidden = new System.Windows.Forms.PictureBox();
            this.topMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.draw_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.draw_box_hidden)).BeginInit();
            this.SuspendLayout();
            // 
            // topMenu
            // 
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.iOToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.applyToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(1247, 24);
            this.topMenu.TabIndex = 0;
            this.topMenu.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.squareToolStripMenuItem,
            this.elipseToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.captionToolStripMenuItem,
            this.moveToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // squareToolStripMenuItem
            // 
            this.squareToolStripMenuItem.Name = "squareToolStripMenuItem";
            this.squareToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.squareToolStripMenuItem.Text = "Square";
            this.squareToolStripMenuItem.Click += new System.EventHandler(this.squareToolStripMenuItem_Click);
            // 
            // elipseToolStripMenuItem
            // 
            this.elipseToolStripMenuItem.Name = "elipseToolStripMenuItem";
            this.elipseToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.elipseToolStripMenuItem.Text = "Elipse";
            this.elipseToolStripMenuItem.Click += new System.EventHandler(this.elipseToolStripMenuItem_Click);
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.groupToolStripMenuItem.Text = "Group";
            this.groupToolStripMenuItem.Click += new System.EventHandler(this.groupToolStripMenuItem_Click);
            // 
            // captionToolStripMenuItem
            // 
            this.captionToolStripMenuItem.Name = "captionToolStripMenuItem";
            this.captionToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.captionToolStripMenuItem.Text = "Caption";
            this.captionToolStripMenuItem.Click += new System.EventHandler(this.captionToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.moveToolStripMenuItem.Text = "Select";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // iOToolStripMenuItem
            // 
            this.iOToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.iOToolStripMenuItem.Name = "iOToolStripMenuItem";
            this.iOToolStripMenuItem.Size = new System.Drawing.Size(31, 20);
            this.iOToolStripMenuItem.Text = "IO";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // applyToolStripMenuItem
            // 
            this.applyToolStripMenuItem.Enabled = false;
            this.applyToolStripMenuItem.Name = "applyToolStripMenuItem";
            this.applyToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.applyToolStripMenuItem.Text = "Apply";
            this.applyToolStripMenuItem.Click += new System.EventHandler(this.applyToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.object_count_text,
            this.selected_tool_text});
            this.statusStrip1.Location = new System.Drawing.Point(0, 527);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1247, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // object_count_text
            // 
            this.object_count_text.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.object_count_text.Margin = new System.Windows.Forms.Padding(0, 3, 20, 2);
            this.object_count_text.Name = "object_count_text";
            this.object_count_text.Size = new System.Drawing.Size(59, 17);
            this.object_count_text.Text = "Objects: 0";
            // 
            // selected_tool_text
            // 
            this.selected_tool_text.Name = "selected_tool_text";
            this.selected_tool_text.Size = new System.Drawing.Size(117, 17);
            this.selected_tool_text.Text = "Selected tool: Square";
            // 
            // draw_box
            // 
            this.draw_box.Location = new System.Drawing.Point(0, 27);
            this.draw_box.Name = "draw_box";
            this.draw_box.Size = new System.Drawing.Size(1247, 497);
            this.draw_box.TabIndex = 2;
            this.draw_box.TabStop = false;
            this.draw_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draw_box_MouseDown);
            this.draw_box.MouseUp += new System.Windows.Forms.MouseEventHandler(this.draw_box_MouseUp);
            // 
            // draw_box_hidden
            // 
            this.draw_box_hidden.Location = new System.Drawing.Point(0, 27);
            this.draw_box_hidden.Name = "draw_box_hidden";
            this.draw_box_hidden.Size = new System.Drawing.Size(1247, 497);
            this.draw_box_hidden.TabIndex = 2;
            this.draw_box_hidden.TabStop = false;
            this.draw_box_hidden.MouseDown += new System.Windows.Forms.MouseEventHandler(this.draw_box_MouseDown);
            this.draw_box_hidden.MouseUp += new System.Windows.Forms.MouseEventHandler(this.draw_box_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 549);
            this.Controls.Add(this.draw_box);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.topMenu);
            this.MainMenuStrip = this.topMenu;
            this.Name = "Form1";
            this.Text = "Paint";
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.draw_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.draw_box_hidden)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elipseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel object_count_text;
        private System.Windows.Forms.ToolStripStatusLabel selected_tool_text;
        private System.Windows.Forms.PictureBox draw_box;
        private System.Windows.Forms.PictureBox draw_box_hidden;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem;

    }
}

