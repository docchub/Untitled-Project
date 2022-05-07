using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Untitled_Project
{
    class Button
    {
        // Fields
        private Color color;
        private Rectangle rect;
        private Entity selected;

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Entity Selected { get { return selected; } }

        // Constructor
        public Button(Rectangle rect, Entity selected)
        {
            color = Color.White;
            this.rect = rect;
            this.selected = selected;
        }
    }
}
