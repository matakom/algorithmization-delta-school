using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace spiral
{
    public partial class Spiral : Window
    {
        Canvas canvas;
        int padding;
        public Spiral(bool useLoop, int lineLength, int gapWidth)
        {
            InitializeComponent();
            padding = 50;
            this.Width = lineLength + 2 * padding;
            this.Height = lineLength + 2 * padding + 50;
            canvas = new Canvas();
            Content = canvas;

            Brush lineColor = Brushes.Black;
            int lineWidth = 2;

            if (useLoop)
            {
                DrawLoop(lineLength, gapWidth, lineColor, lineWidth);
            }
            else
            {
                DrawRecursion(lineLength, gapWidth, lineColor, lineWidth);
            }
        }
        private Vector2 RotateVector(Vector2 vector2)
        {
            return new Vector2(-vector2.Y, vector2.X);
        }
        private Vector2 ApplyOffset(Vector2 position, Vector2 lineOffset, int lineWidth)
        {
            return new Vector2(
                position.X + lineWidth / 2 * lineOffset.X, 
                position.Y + ((lineWidth / 2) * lineOffset.Y));
        }
        private void DrawLoop(int lineLength, int gapWidth, Brush lineColor, int lineWidth)
        {
            // Not starting from edge
            Vector2 currentPosition = new Vector2(padding, padding);

            // Counter for shortening line length
            int counter = 0;

            // Setting direction and Offset values
            Vector2 direction = new Vector2(1, 0);
            Vector2 lineOffset = new Vector2(-1, 1);

            int previousLineLength = int.MaxValue;

            while (previousLineLength > gapWidth + 2 * lineWidth)
            {
                // Calculating new position
                Vector2 newPosition = new Vector2(
                    currentPosition.X + direction.X * lineLength,
                    currentPosition.Y + direction.Y * lineLength
                );

                // Drawing line
                Line line = new Line
                {
                    X1 = currentPosition.X,
                    Y1 = currentPosition.Y,
                    X2 = newPosition.X,
                    Y2 = newPosition.Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = lineWidth,
                    Opacity = 1
                };
                canvas.Children.Add(line);

                // Offset of the line width
                lineOffset.X *= (direction.X == 0) ? -1 : 1;
                lineOffset.Y *= (direction.Y == 0) ? -1 : 1;
                newPosition = ApplyOffset(newPosition, lineOffset, lineWidth);

                currentPosition = newPosition;

                // Changing direction
                direction = RotateVector(direction);

                // Saving information for exit condition
                previousLineLength = lineLength;

                // Making the line shorter
                if (counter == 2)
                {
                    lineLength -= gapWidth + lineWidth;
                    counter = 0;
                }
                counter++;
            }
        }
        private void DrawRecursion(int lineLength, int gapWidth, Brush lineColor, int lineWidth)
        {
            // Setting variables for recursion
            Vector2 currentPosition = new Vector2(padding, padding);
            Vector2 direction = new Vector2(1, 0);
            Vector2 lineOffset = new Vector2(-1, 1);
            int previousLineLength = int.MaxValue;
            int counter = 0;
            Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth, lineOffset, previousLineLength);
        }
        private void Recursion(int lineLength, int gapWidth, Vector2 currentPosition, Vector2 direction, int counter, Brush lineColor, int lineWidth, Vector2 lineOffset, int previousLineLength)
        {
            // Calculating new position
            Vector2 newPosition = new Vector2(
                currentPosition.X + direction.X * lineLength,
                currentPosition.Y + direction.Y * lineLength
            );

            // Drawing line
            Line line = new Line
            {
                X1 = currentPosition.X,
                Y1 = currentPosition.Y,
                X2 = newPosition.X,
                Y2 = newPosition.Y,
                Stroke = Brushes.Black,
                StrokeThickness = lineWidth,
                Opacity = 1
            };
            canvas.Children.Add(line);

            // Offset of the line width
            lineOffset.X *= (direction.X == 0) ? -1 : 1;
            lineOffset.Y *= (direction.Y == 0) ? -1 : 1;
            newPosition = ApplyOffset(newPosition, lineOffset, lineWidth);

            currentPosition = newPosition;

            // Changing direction
            direction = RotateVector(direction);

            // Saving information for exit condition
            previousLineLength = lineLength;

            // Making the line shorter
            if (counter == 2)
            {
                lineLength -= gapWidth + lineWidth;
                counter = 0;
            }
            counter++;

            // Calling recursion
            if(previousLineLength > gapWidth + 2 * lineWidth)
            {
                Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth, lineOffset, previousLineLength);
            }
        }
    }
}