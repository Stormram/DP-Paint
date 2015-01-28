using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    /// <summary>
    /// Visitor pattern stuff
    /// </summary>

    public interface IDrawElementVisitor
    {
        void visit(Group group);
        void visit(BasicShape elipse);
        void visit(Graphic g);
    }

    public interface IDrawElement
    {
        void accept(IDrawElementVisitor visitor);
    }
}
