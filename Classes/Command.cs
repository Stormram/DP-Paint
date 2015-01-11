using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    interface Command
    {
        void Execute();
        void UnExecute();
        String toString();

    }

    class CreateCommand : Command
    {
        drawBoxHandler _handler;
        Graphic _shape;

        public CreateCommand(drawBoxHandler handler, Graphic shape)
        {
            _handler = handler;
            _shape = shape;
        }

        public void Execute()
        {
            _handler.addShape(_shape);
        }

        public void UnExecute()
        {
            _handler.remove(_shape);
        }

        public String toString()
        {
            return "CreateCommand";
        }
    }

    class GroupCommand : Command
    {
        private drawBoxHandler _handler;
        private List<Graphic> _toGroup;
        private Group _groupGraphic;

        public GroupCommand(drawBoxHandler handler, List<Graphic> toGroup)
        {
            _handler = handler;
            _toGroup = toGroup;

            _groupGraphic = new Group();
            _groupGraphic.setList(toGroup);
        }

        public void Execute()
        {
            foreach (Graphic g in _toGroup)
                _handler.remove(g);
            _handler.addShape(_groupGraphic);
            _handler.Redraw();
        }

        public void UnExecute()
        {
            foreach (Graphic g in _toGroup)
                _handler.addShape(g);
            _handler.remove(_groupGraphic);
            _handler.Redraw();
        }

        public string toString()
        {
            return "Group";
        }
    }

    class ChangeCommand : Command
    {
        drawBoxHandler _handler;
        int _new_x, _new_y, _new_width, _new_height;
        int _old_x, _old_y, _old_width, _old_height;
        Graphic _shape;

        public ChangeCommand(drawBoxHandler handler, Graphic shape)
       {
            _handler = handler;
            _shape = shape;

            // Fill old values!
            _old_x = shape.getLeft();
            _old_y = shape.getTop();
            _old_width = shape.getWidth();
            _old_height = shape.getHeight();
        }

        public void finished( int x, int y, int width, int height)
        {
            _new_x = x;
            _new_y = y;
            _new_width = width;
            _new_height = height;
        }

        public void Execute()
        {
            _shape.setX(_new_x);
            _shape.setY(_new_y);
            _shape.setWidth(_new_width);
            _shape.setHeight(_new_height);
                
            // Redraw the view 
            _handler.Redraw();
        }

        public void UnExecute()
        {
            _shape.setX(_old_x);
            _shape.setY(_old_y);
            _shape.setWidth(_old_width);
            _shape.setHeight(_old_height);

            // Redraw the view 
            _handler.Redraw();
        }


        public string toString()
        {
            return String.Format("Change");
        }
    }

    class CommandHandler
    {
        Stack<Command> _undoStack = new Stack<Command>();
        Stack<Command> _redoStack = new Stack<Command>();

        public CommandHandler()
        { }

        public void Add(Command command)
        {
            command.Execute();
            _undoStack.Push(command);
            // Clear redo stack, we are going on a new branch?
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count() == 0) return;

            Command c = _undoStack.Pop();
            c.UnExecute();

            Console.WriteLine("undo " + c.toString());

            _redoStack.Push(c);
        }

        public void Redo()
        {
            if (_redoStack.Count() == 0) return;

            Command c = _redoStack.Pop();
            c.Execute();

            Console.WriteLine("redo " + c.toString());

            _undoStack.Push(c);
        }

    }
}
