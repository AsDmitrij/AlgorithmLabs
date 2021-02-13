using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int numberOfCells = 10;
        int lengthOfDrawedField, widthOfDrawedField, stepNumber;

        byte[,] calculatedField; 
        byte[,] helpField;

        Rectangle[,] drawedField;

        private void lifeTimerTick(object sender, EventArgs e)
        {
            stepIteration();
            this.Invalidate();
        }

        private void refreshCurrentPositions()
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                { 
                    helpField[i, j] = getCellStatus(i, j);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Paint += drawField;
            this.MouseClick += Form1_MouseClick;
            prepareField();
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bool b = true;
            for (int i = 0; b & (i < numberOfCells); i++)
            { 
                for (int j = 0; b & (j < numberOfCells); j++)
                    if (drawedField[i, j].Contains(e.X, e.Y))
                    {
                        calculatedField[i, j] = 2;
                        b = false;
                    }
            }
            refreshCurrentPositions();
            this.Invalidate();
        }

        private void prepareField()
        {
            stepNumber = 0;
            lengthOfDrawedField = (this.Width - this.Height) / 2;
            widthOfDrawedField = (this.Height - 120) / numberOfCells;
            drawedField = new Rectangle[numberOfCells, numberOfCells];

            calculatedField = new byte[numberOfCells, numberOfCells];
            helpField = new byte[numberOfCells, numberOfCells];
            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                {
                    helpField[i, j] = 0;
                    calculatedField[i, j] = 0;

                    drawedField[i, j] = new Rectangle(55 + lengthOfDrawedField + i * widthOfDrawedField, 10 + j * widthOfDrawedField, widthOfDrawedField, widthOfDrawedField);
                }
            }
        }

        private void putRandomPoints()
        {
            Random random = new Random();
            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                {
                    calculatedField[i, j] = (byte)random.Next(0, 3);
                    if (calculatedField[i, j] == 1)
                    {
                        calculatedField[i, j] = 0;
                    }
                }
            }
            refreshCurrentPositions();
        }

        private byte getCellStatus(int i,int j)
        {
            byte a = 0;
            if (j > 0)
            {
                if (calculatedField[i, j - 1] == 2)
                    a++;
                if (i > 0)
                    if (calculatedField[i - 1, j - 1] == 2)
                        a++;
                if (i < numberOfCells - 1)
                    if (calculatedField[i + 1, j - 1] == 2)
                        a++;
            }

            if (j < numberOfCells - 1)
            {
                if (calculatedField[i, j + 1] == 2)
                    a++;
                if (i < numberOfCells - 1)
                    if (calculatedField[i + 1, j + 1] == 2)
                        a++;
                if (i > 0)
                    if (calculatedField[i - 1, j + 1] == 2)
                        a++;
            }
            if (i < numberOfCells - 1)
                if (calculatedField[i + 1, j] == 2)
                    a++;
            if (i > 0)
                if (calculatedField[i - 1, j] == 2)
                    a++;
            return a;
        }

        private void stepIteration()
        {

            refreshCurrentPositions();

            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                {
                    if ((calculatedField[i, j] == 0) & (helpField[i, j] == 3))
                    {
                        calculatedField[i, j] = 2;
                    }
                    else if ((calculatedField[i, j] == 2) & ((helpField[i, j] != 3) & (helpField[i, j] != 2)))
                    { 
                        calculatedField[i, j] = 0; 
                    }

                }
            }
            stepNumber++;
            stepLabel.Text = "Step: " + stepNumber.ToString();
        }

        void drawField(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < numberOfCells; i++)
            {
                for (int j = 0; j < numberOfCells; j++)
                {
                    if (calculatedField[i, j] == 2)
                    { 
                        e.Graphics.FillEllipse(Brushes.Gray, drawedField[i, j]); 
                    }
                    
                    e.Graphics.DrawRectangle(Pens.Black, drawedField[i, j]);
                }
            }
        }

        private void clearField()
        {
            for (int i = 0; i < numberOfCells; i++)
                for (int j = 0; j < numberOfCells; j++)
                {
                    calculatedField[i, j] = 0;
                    helpField[i, j] = 0;
                }
        }

        private void numericGenerationSpeed_ValueChanged(object sender, EventArgs e)
        {
            lifeTimer.Interval = 1000 / Convert.ToInt32(numericGenerationSpeed.Value);
        }
        private void stepButton_Click(object sender, EventArgs e)
        {
            stepIteration();
            this.Invalidate();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearField();
            this.Invalidate();
        }

        private void randomizeButton_Click(object sender, EventArgs e)
        {
            putRandomPoints();
            this.Invalidate();
        }

        private void numericSizeOfField_ValueChanged(object sender, EventArgs e)
        {
            lifeTimer.Enabled = false;
            startStopButton.Text = "Start";
            numberOfCells = Convert.ToInt32(numericSizeOfField.Value);
            prepareField();
            this.Invalidate();
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (!lifeTimer.Enabled)
            {
                lifeTimer.Enabled = true;
                startStopButton.Text = "Stop";
            }
            else
            {
                lifeTimer.Enabled = false;
                startStopButton.Text = "Start";
            }
        }
    }
}
