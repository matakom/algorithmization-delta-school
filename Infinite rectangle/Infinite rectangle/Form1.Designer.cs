using System.Diagnostics;
using System.Numerics;

namespace Infinite_rectangle
{
    partial class Form1
    {
        private void Draw(Graphics g)
        {
            System.Drawing.Pen lineColor = Pens.Black;

            int length = 800;
            int gapWidth = 20;
            
            // Choosing loop or recursion mode
            //DrawLoop(length, gapWidth, lineColor, g);
            DrawRecursion(length, gapWidth, lineColor, g);
        }
        private void DrawLoop(int length, int gapWidth, Pen lineColor, Graphics g)
        {
            // Not starting from edge
            Vector2 currentPosition = new Vector2(100, 100);

            // Counter of lines for subracting line length
            // Starts at -1, because at the start we need 3 same length lines instead of 2
            int counter = -1;
            // Direction for next line
            Vector2 direction = new Vector2(1, 0);

            while (length > 0)
            {

                // Making line shorter
                if (counter == 2)
                {
                    length -= gapWidth;
                    counter = 0;
                }

                Vector2 newPosition = Vector2.Zero;

                // Calculating new position
                newPosition.X = direction.X * length + currentPosition.X;
                newPosition.Y = direction.Y * length + currentPosition.Y;

                // Drawing line
                g.DrawLine(lineColor, currentPosition.X, currentPosition.Y, newPosition.X, newPosition.Y);

                currentPosition = newPosition;
                counter++;

                // Changing direction
                if (direction.X != 0)
                {
                    direction.Y = direction.X;
                    direction.X = 0;
                }
                else if (direction.Y != 0)
                {
                    direction.X = direction.Y * -1;
                    direction.Y = 0;
                }
            }
        }
        private void DrawRecursion(int length, int gapWidth, Pen lineColor, Graphics g)
        {
            // Setting variables for recursion
            Vector2 currentPosition = new Vector2(100, 100);
            Vector2 direction = new Vector2(1, 0);
            int counter = -1;
            Recursion(length, gapWidth, currentPosition, direction, counter, lineColor, g);
        }
        private void Recursion(int length, int gapWidth, Vector2 currentPosition, Vector2 direction, int counter, Pen lineColor, Graphics g)
        {
            // End of recursion
            if(length < gapWidth)
            {
                return;
            }

            // Making line shorter
            if (counter == 2)
            {
                length -= gapWidth;
                counter = 0;
            }

            Vector2 newPosition = Vector2.Zero;

            // Calculating new position
            newPosition.X = direction.X * length + currentPosition.X;
            newPosition.Y = direction.Y * length + currentPosition.Y;

            // Drawing line
            g.DrawLine(lineColor, currentPosition.X, currentPosition.Y, newPosition.X, newPosition.Y);

            // Changing direction of next line
            if (direction.X != 0)
            {
                direction.Y = direction.X;
                direction.X = 0;
            }
            else if (direction.Y != 0)
            {
                direction.X = direction.Y * -1;
                direction.Y = 0;
            }
            currentPosition = newPosition;
            counter++;

            // Calling recursion
            Recursion(length, gapWidth, currentPosition, direction, counter, lineColor, g);
        }
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            Draw(g);

            
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 1000);
            this.Text = "Form1";
        }

        #endregion
    }
}
