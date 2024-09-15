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
    /// <summary>
    /// Interaction logic for Spiral.xaml
    /// </summary>
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
            int lineWidth = 5;

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
            int counter = -1;
            Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth);
        }
        private void Recursion(int lineLength, int gapWidth, Vector2 currentPosition, Vector2 direction, int counter, Brush lineColor, int lineWidth)
        {
            // End of recursion
            if (lineLength < gapWidth)
            {
                return;
            }

            // Making line shorter
            if (counter == 2)
            {
                lineLength -= gapWidth;
                counter = 0;
            }

            Vector2 newPosition = Vector2.Zero;

            // Calculating new position
            newPosition.X = (float)(direction.X * lineLength + currentPosition.X);
            newPosition.Y = (float)(direction.Y * lineLength + currentPosition.Y);

            // Drawing line
            Line line = new Line
            {
                X1 = currentPosition.X,
                Y1 = currentPosition.Y,
                X2 = newPosition.X,
                Y2 = newPosition.Y,
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };
            canvas.Children.Add(line);

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
            Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth);

        }
    }
}


/*
 * using System;
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
    /// <summary>
    /// Interaction logic for Spiral.xaml
    /// </summary>
    public partial class Spiral : Window
    {
        Canvas canvas;
        int padding;
        List<Brush> brushes = new List<Brush> {
            Brushes.Black,
            Brushes.Red,
            Brushes.Green,
            Brushes.Blue,
        };
        int brushIndex = 0;
        public Spiral(bool useLoop, int lineLength, int gapWidth)
        {
            InitializeComponent();
            padding = 50;
            this.Width = lineLength + 2 * padding;
            this.Height = lineLength + 2 * padding + 50;
            canvas = new Canvas();
            Content = canvas;

            Brush lineColor = Brushes.Black;
            int lineWidth = 20;

            if (useLoop)
            {
                DrawLoop(lineLength, gapWidth, lineColor, lineWidth);
            }
            else
            {
                DrawRecursion(lineLength, gapWidth, lineColor, lineWidth);
            }
        }
        private void DrawLoop(int lineLength, int gapWidth, Brush lineColor, int lineWidth)
        {
            // Not starting from edge
            Vector2 currentPosition = new Vector2(padding, padding);

            // Counter of lines for subracting line length
            // Starts at -1, because at the start we need 3 same length lines instead of 2
            int counter = -1;

            // Direction for next line
            Vector2 direction = new Vector2(1, 0);

            while (lineLength >= gapWidth + 2 * lineWidth)
            {
                if (counter == 2)
                {
                    lineLength -= gapWidth + lineWidth;
                    counter = 0;
                }
                if (lineLength < gapWidth)
                {
                    break;
                }
                counter++;

                Vector2 newPosition = Vector2.Zero;

                // Calculating new position
                if (direction.X == 0)
                {
                    if (direction.Y < 0)
                    {
                        currentPosition.Y += lineWidth / 2;
                        currentPosition.X += lineWidth / 2;
                    }
                    else
                    {
                        currentPosition.X -= lineWidth / 2;
                        currentPosition.Y -= lineWidth / 2;
                    }
                    newPosition.Y = (float)(direction.Y * lineLength + currentPosition.Y);
                    newPosition.X = (float)(direction.X * lineLength + currentPosition.X);
                }
                else
                {
                    if (direction.X < 0)
                    {
                        currentPosition.X += lineWidth / 2;
                        currentPosition.Y -= lineWidth / 2;
                    }
                    else
                    {
                        currentPosition.X -= lineWidth / 2;
                        currentPosition.Y += lineWidth / 2;
                    }
                    newPosition.Y = (float)(direction.Y * lineLength + currentPosition.Y);
                    newPosition.X = (float)(direction.X * lineLength + currentPosition.X);
                }

                // Drawing line
                Line line = new Line
                {
                    X1 = currentPosition.X,
                    Y1 = currentPosition.Y,
                    X2 = newPosition.X,
                    Y2 = newPosition.Y,
                    Stroke = brushes[brushIndex],
                    StrokeThickness = lineWidth,
                    Opacity = 0.9
                };
                canvas.Children.Add(line);
                brushIndex++;
                if (brushIndex == 4)
                {
                    brushIndex = 0;
                }


                currentPosition = newPosition;

                // Changing direction
                direction = new Vector2(-direction.Y, direction.X);
            }
        }
        private void DrawRecursion(int lineLength, int gapWidth, Brush lineColor, int lineWidth)
        {
            // Setting variables for recursion
            Vector2 currentPosition = new Vector2(padding, padding);
            Vector2 direction = new Vector2(1, 0);
            int counter = -1;
            Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth);
        }
        private void Recursion(int lineLength, int gapWidth, Vector2 currentPosition, Vector2 direction, int counter, Brush lineColor, int lineWidth)
        {
            // End of recursion
            if (lineLength < gapWidth)
            {
                return;
            }

            // Making line shorter
            if (counter == 2)
            {
                lineLength -= gapWidth;
                counter = 0;
            }

            Vector2 newPosition = Vector2.Zero;

            // Calculating new position
            newPosition.X = (float)(direction.X * lineLength + currentPosition.X);
            newPosition.Y = (float)(direction.Y * lineLength + currentPosition.Y);

            // Drawing line
            Line line = new Line
            {
                X1 = currentPosition.X,
                Y1 = currentPosition.Y,
                X2 = newPosition.X,
                Y2 = newPosition.Y,
                Stroke = lineColor,
                StrokeThickness = lineWidth
            };
            canvas.Children.Add(line);

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
            Recursion(lineLength, gapWidth, currentPosition, direction, counter, lineColor, lineWidth);

        }
    }
}
*/