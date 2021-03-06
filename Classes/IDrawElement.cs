﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    /// <summary>
    /// Visitor for elements interface
    /// </summary>
    public interface IDrawElementVisitor
    {
        void visit(Group group);
        void visit(Decorator decorator);
        void end_visit(Group group);
        void visit(BasicShape elipse);
        //void visit(Graphic g);
    }

    /// <summary>
    /// Elements must implement this so the IDrawElementVisitor can visit them
    /// </summary>
    public interface IDrawElement
    {
        void accept(IDrawElementVisitor visitor);
    }
}
