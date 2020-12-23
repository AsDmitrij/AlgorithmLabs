using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using System.IO;

namespace Lab1
{
    public partial class Drawing : Form
    {
        public int sprocessors = 0;
        public int nprocessors = 0;
        public int[,] Tmatrix = new int[7,7];
        public int[] TimeSequence = { 3, 5, 2, 5, 7, 6, 4, 8, 6, 5 };

        public Drawing()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Drawing_MouseDown);
            sprocessors = Convert.ToInt32(textBox1.Text);
            nprocessors = Convert.ToInt32(textBox2.Text);
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region Лабораторные 1-2 параллельные процессы
        private void Draw_Click(object sender, EventArgs e)
        {
            int[,] Tmatrix = new int[nprocessors, sprocessors];
            for (int i = 0; i < nprocessors; i++)
            {
                for (int j = 0; j < sprocessors; j++)
                {
                    Tmatrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            zedGraphControl1.Visible = true;

            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title.Text = "Асинхронный режим распределенных конкурирующих процессов";
            pane.XAxis.Title.Text = "T (p,n,s,e)";
            pane.YAxis.Title.Text = "P";
            
            
            pane.CurveList.Clear();

            int[] last_koef_array = new int[nprocessors];

            int last_dot = 0;
            LineItem drawing = new LineItem("0");
            for (int j=0;j< nprocessors; j++)
            {
                int x= 0;
                int x1 = last_koef_array[0];
                for (int y = 0; y < sprocessors; y++)
                {
                    PointPairList temp_list_of_process = new PointPairList();
                    for (int i = x1; i <= Tmatrix[j, y] + x1; i++)
                    {
                        temp_list_of_process.Add(i, y + 1);
                        x = i;
                    }
                    last_koef_array[y] = x;
                    if (y != (sprocessors - 1))
                    {
                        if (last_koef_array[y + 1] < x)
                        {
                            x1 = x;
                        }
                        else
                        {
                            x1 = last_koef_array[y + 1];
                        }
                    }
                    else
                    { 
                        last_dot = x; 
                    }
                    string name = "t" + (j+1)+","+(y+1);
                    if (y == 0)
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.Diamond); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.Diamond); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.Diamond); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.Diamond); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.Diamond); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.Diamond); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.Diamond); break;
                            default: break;
                        }
                    }
                    if (y == 1)
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.TriangleDown); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.TriangleDown); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.TriangleDown); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.TriangleDown); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.TriangleDown); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.TriangleDown); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.TriangleDown); break;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.Triangle); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.Triangle); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.Triangle); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.Triangle); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.Triangle); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.Triangle); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.Triangle); break;
                            default: break;
                        }
                    }
                    
                }
            
            }
            PointPairList end_list_of_process = new PointPairList();
            end_list_of_process.Add(last_dot, 0);
            string end_text = "T end:" + last_dot.ToString();
            textBox3.Text = last_dot.ToString();
            drawing = pane.AddCurve(end_text, end_list_of_process, Color.Red, SymbolType.None); 
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void Clean_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
        }

        private void Drawing_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
            Win32.SendMessage(this.Handle, Win32.WM_NCLBUTTONDOWN, Win32.HTCAPTION, 0);
        }

        private void GetMatrix_Click(object sender, EventArgs e)
        {
            sprocessors = Convert.ToInt32(textBox1.Text);
            nprocessors = Convert.ToInt32(textBox2.Text);
            DrawTableForMatrix(sprocessors, nprocessors);
        }
        private void DrawTableForMatrix(int s,int n)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = s;
            for (int i = 0; i < s; i++)
            {
                dataGridView1.Columns[i].Width = 15;
            }
            dataGridView1.RowCount = n;
        }
        private void DefaultValue_Click(object sender, EventArgs e)
        {
            DrawTableForMatrix(7, 7);
            int[,] DefaultMatrix = {
                { 1,3,4,5,3,2,1 },
            { 3,4,5,3,2,1,1 },
            { 4,5,3,2,1,1,3 },
            { 5,3,2,1,1,3,4 },
            { 3,2,1,1,3,4,5 },
            { 2,1,1,3,4,5,3 },
            { 1,1,3,4,5,3,2 },
            };
            for (int i = 0; i < nprocessors; i++)
            {
                for (int j = 0; j < sprocessors; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = DefaultMatrix[i, j];
                }
            }
            Tmatrix = DefaultMatrix;
        }
        private void Draw2_Click(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = true;

            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title.Text = "Первый синхронный режим \n распределенных конкурирующих процессов";
            pane.XAxis.Title.Text = "T (p,n,s,e)";
            pane.YAxis.Title.Text = "P";


            pane.CurveList.Clear();

            int[] last_koef_array = new int[sprocessors];

            int last_dot = 0;
            LineItem drawing = new LineItem("0");
            for (int j = 0; j < nprocessors; j++)
            {
                int x = 0;
                
                for (int y = 0; y < sprocessors-1; y++)
                {
                    if(last_koef_array[y] + Tmatrix[j, y]< last_koef_array[y+1])
                    {
                        for(int y1=0;y1<=y;y1++)
                        {
                            last_koef_array[y1] += last_koef_array[y + 1] - Tmatrix[j, y]- last_koef_array[y];
                        }
                    }
                }
                int x1 = last_koef_array[0];

                for (int y = 0; y < sprocessors; y++)
                {
                    PointPairList temp_list_of_process = new PointPairList();
                    for (int i = x1; i <= Tmatrix[j, y] + x1; i++)
                    {
                        temp_list_of_process.Add(i, y + 1);
                        x = i;
                    }
                    last_koef_array[y] = x;
                    if (y != (sprocessors - 1))
                    {
                        if (last_koef_array[y + 1] < x)
                        {
                            x1 = x;
                        }
                        else
                        {
                            x1 = last_koef_array[y + 1];
                        }
                    }
                    else
                    {
                        last_dot = x;
                    }

                    string name = "t" + (j + 1) + "," + (y + 1);

                    if (y == 0)
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.Diamond); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.Diamond); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.Diamond); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.Diamond); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.Diamond); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.Diamond); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.Diamond); break;
                            default: break;
                        }
                    }
                    if (y == 1)
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.TriangleDown); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.TriangleDown); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.TriangleDown); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.TriangleDown); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.TriangleDown); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.TriangleDown); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.TriangleDown); break;
                            default: break;
                        }
                    }
                    else
                    {
                        switch (j)
                        {
                            case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.Triangle); break;
                            case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.Triangle); break;
                            case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.Triangle); break;
                            case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.Triangle); break;
                            case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.Triangle); break;
                            case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.Triangle); break;
                            case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.Triangle); break;
                            default: break;
                        }
                    }
                }

            }
            PointPairList end_list_of_process = new PointPairList();
            end_list_of_process.Add(last_dot, 0);
            string end_text = "T end:" + last_dot.ToString();
            textBox4.Text = last_dot.ToString();
            drawing = pane.AddCurve(end_text, end_list_of_process, Color.Red, SymbolType.None);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void Draw3_Click(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = true;

            GraphPane pane = zedGraphControl1.GraphPane;

            pane.Title.Text = "Второй синхронный режим \n распределенных конкурирующих процессов";
            pane.XAxis.Title.Text = "T (p,n,s,e)";
            pane.YAxis.Title.Text = "P";


            pane.CurveList.Clear();

            int[] last_koef_array = new int[nprocessors];

            int last_dot = 0;
            LineItem drawing = new LineItem("0");
            for (int y = 0; y < sprocessors; y++)
            {
                int x = 0;
                for (int j = 0; j < nprocessors-1; j++)
                {
                    if(last_koef_array[j]+Tmatrix[j, y]< last_koef_array[j+1])
                    {
                        for (int j1 = 0; j1 <= j; j1++)
                        {
                            last_koef_array[j1] += last_koef_array[j + 1] - last_koef_array[j] - Tmatrix[j, y];
                        }
                    }
                }
                int x1 = last_koef_array[0];
                for (int j = 0; j < nprocessors; j++)
                {
                    PointPairList temp_list_of_process = new PointPairList();
                    for (int i = x1; i <= Tmatrix[j, y] + x1; i++)
                    {
                        temp_list_of_process.Add(i, y + 1);
                        x = i;
                    }
                    x1 = x;
                    last_koef_array[j] = x1;
                    last_dot = x;
                    string name = "t" + (j + 1) + "," + (y + 1);
                    switch (j)
                    {
                        case 0: drawing = pane.AddCurve(name, temp_list_of_process, Color.Blue, SymbolType.TriangleDown); break;
                        case 1: drawing = pane.AddCurve(name, temp_list_of_process, Color.Red, SymbolType.TriangleDown); break;
                        case 2: drawing = pane.AddCurve(name, temp_list_of_process, Color.LimeGreen, SymbolType.TriangleDown); break;
                        case 3: drawing = pane.AddCurve(name, temp_list_of_process, Color.Green, SymbolType.TriangleDown); break;
                        case 4: drawing = pane.AddCurve(name, temp_list_of_process, Color.Black, SymbolType.TriangleDown); break;
                        case 5: drawing = pane.AddCurve(name, temp_list_of_process, Color.Orange, SymbolType.TriangleDown); break;
                        case 6: drawing = pane.AddCurve(name, temp_list_of_process, Color.Purple, SymbolType.TriangleDown); break;
                        default: break;
                    }
                }
            }
            PointPairList end_list_of_process = new PointPairList();
            end_list_of_process.Add(last_dot, 0);
            string end_text = "T end:" + last_dot.ToString();
            textBox5.Text = last_dot.ToString();
            drawing = pane.AddCurve(end_text, end_list_of_process, Color.Red, SymbolType.None);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void NewValue_Click(object sender, EventArgs e)
        {
            Tmatrix = new int[nprocessors,sprocessors];
            for(int i=0;i<nprocessors;i++)
            {
                for(int j=0;j<sprocessors;j++)
                {
                    Tmatrix[i,j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
        }
        private void SecondDefaultValue_Click(object sender, EventArgs e)
        {
            DrawTableForMatrix(3, 4);
            int[,] DefaultMatrix = {
                { 4,2,3 },
            { 1,4,1 },
            { 3,3,2 },
            { 3,1,2 },
            };
            for (int i = 0; i < nprocessors; i++)
            {
                for (int j = 0; j < sprocessors; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = DefaultMatrix[i, j];
                }
            }
            Tmatrix = DefaultMatrix;
        }

        private void FindCriticalWay_Click(object sender, EventArgs e)
        {
           int [,] TempMatrix = Tmatrix;
            int[][] OldPoints = new int [2][];
            OldPoints[0] = new int[] { 0 };
            OldPoints[1] = new int[] { 0 };
            int counter = 100000;
            for (int times = 0; times < 2; times++)
            {
                int[][] TempPoints = new int[2][];
                TempPoints[0] = new int[] { 0 };
                TempPoints[1] = new int[] { 0 };
                int right = 0;
                int down = 0;
                int right_way = 0;
                int down_way = 0;
                int temp_counter = 0;
                do
                {
                    right_way = 0;
                    down_way = 0;
                    if (right < nprocessors - 1)
                    {
                        right_way = TempMatrix[right + 1, down];
                        for (int i = 0; i < OldPoints[0].Length - 2; i++)
                        {
                            if (OldPoints[0][i] == right + 1 && OldPoints[1][i] == down)
                            {
                                right_way = 100000;
                            }
                        }
                    }
                    else
                    {
                        right_way = 100000;
                    }
                    if (down < sprocessors - 1)
                    {
                        down_way = TempMatrix[right, down + 1];
                        for (int i = 0; i < OldPoints[0].Length - 2; i++)
                        {
                            if (OldPoints[0][i] == right && OldPoints[1][i] == down + 1)
                            {
                                down_way = 100000;
                            }
                        }
                    }
                    else
                    {
                        down_way = 100000;
                    }


                    TempPoints[0][TempPoints[0].Length - 1] = right;
                    TempPoints[1][TempPoints[1].Length - 1] = down;
                    Array.Resize(ref TempPoints[0], TempPoints[0].Length + 1);
                    Array.Resize(ref TempPoints[1], TempPoints[1].Length + 1);
                    temp_counter += TempMatrix[right, down];
                    if (right_way < down_way )//&& !check_entity_right)
                    {
                        right++;
                    }
                    else //if (check_entity_down)
                    {
                        down++;
                    }
                   
                }
                while (right_way != 100000 || down_way != 100000);
                if(temp_counter<=counter)
                {
                    OldPoints = TempPoints;
                    counter = temp_counter;
                }
            }
            for (int i = 0; i < OldPoints[0].Length; i++)
            {
                 dataGridView1.Rows[OldPoints[0][i]].Cells[OldPoints[1][i]].Style.BackColor = Color.Green;
            }
            textBox6.Text = counter.ToString();
            /* for (int right=0; right < nprocessors; right++)
            {
                 for (int down = 0; down < sprocessors; down++)
                 {
                     int right_way = 0;
                     int down_way = 0;
                     if(right< nprocessors-1)
                     {
                         right_way = TempMatrix[right + 1, down];
                     }
                     else
                     {
                         right_way = 100000;
                     }
                     if(down < sprocessors-1)
                     {
                         down_way = TempMatrix[right, down + 1];
                     }
                     else
                     {
                         down_way = 100000;
                     }
                     if (right_way== 100000 && down_way == 100000)
                     {
                         goto end;
                     }

                     OldPoints[0][OldPoints[0].Length - 1] = right;
                     OldPoints[1][OldPoints[1].Length - 1] = down;
                     Array.Resize(ref OldPoints[0], OldPoints[0].Length + 1);
                     Array.Resize(ref OldPoints[1], OldPoints[1].Length + 1);

                     if (right_way < down_way)
                     {
                         right++;
                     }
                     else 
                     {
                         down++;
                     }
                 }
             }
            end:*/
            ;
        }
        private void CheckDotEnter(int [,] array,int i,int j)
        {
            //for (int i)
        }

        private void UniStructuting_Click(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "График \n равномерного структурирования";
            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "f(x)";
            pane.CurveList.Clear(); 

            LineItem drawing = new LineItem("0");

            int[] NewTimeSequence = new int[TimeSequence.Length];
            NewTimeSequence = TimeSequence;
            Array.Sort(NewTimeSequence);
            int counter = 0;
            for(int i=0;i< NewTimeSequence.Length;i++)
            {
                NewTimeSequence[counter] = TimeSequence[i];
                if (i != NewTimeSequence.Length-1)
                { NewTimeSequence[NewTimeSequence.Length - 1 - counter] = TimeSequence[i + 1]; }
                i++;
                counter++;
            }
            PointPairList temp_list_of_process = new PointPairList();
            PointPairList temp_list_of_process_2 = new PointPairList();
            bool flag = true;
            for (int i = 0; i < NewTimeSequence.Length; i++)
            {
                int koef = 0;
                if (flag)
                {
                    koef = i;
                }
                else
                {
                    koef = NewTimeSequence.Length - i;
                }
                double x = koef + Math.Pow(Convert.ToDouble(NewTimeSequence[i]), 1.0 / 2.0);
                temp_list_of_process.Add(i, x);
                temp_list_of_process_2.Add(i, 1 + Math.Sqrt(10));
                if (flag)
                {
                    if(NewTimeSequence[i]> NewTimeSequence[i+1])
                    {
                        flag = false;
                    }
                }
            }
            double tau = 1 + Math.Sqrt(10);

            textBox14.Text = "τ = "+Convert.ToInt16(tau);
            drawing = pane.AddCurve("Delta", temp_list_of_process, Color.Blue);
            drawing = pane.AddCurve("Tau", temp_list_of_process_2, Color.Red);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
  
        private void Evaluate_Click(object sender, EventArgs e)
        {
            double sum_tj = TimeSequence.Sum();
            double n_ar = TimeSequence.Length;
            double p_ar = Convert.ToInt32(textBox10.Text);
            textBox8.Text = sum_tj +"+"+n_ar + " * ε >";
            textBox9.Text = p_ar+"*("+n_ar+ "+ ε)";
            textBox12.Text = "ε = " + p_ar / sum_tj;
            textBox15.Text = "s0 = " + Convert.ToInt32(n_ar-p_ar / sum_tj*20);
            textBox16.Text = "s0 = " + Convert.ToInt32(p_ar / sum_tj * 10);
        }
        private void Findp_Click(object sender, EventArgs e)
        {
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "График \n равномерного структурирования";
            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "f(x)";
            pane.CurveList.Clear();

            LineItem drawing = new LineItem("0");

            int[] NewTimeSequence = new int[TimeSequence.Length];
            NewTimeSequence = TimeSequence;
            Array.Sort(NewTimeSequence);
            PointPairList temp_list_of_process = new PointPairList();
            PointPairList temp_list_of_process_2 = new PointPairList();
            int p = NewTimeSequence.Length - 1;
            for (int i = 0; i < NewTimeSequence.Length; i++)
            {
                double x = (NewTimeSequence.Length-i + Math.Pow(Convert.ToDouble(NewTimeSequence[i]),1.0/2.0))/i;
                temp_list_of_process.Add(i, x);
                if(i== NewTimeSequence.Length-1)
                {
                    temp_list_of_process.Add(i+1, x);
                    for (int j = 0; j < NewTimeSequence.Length+ Convert.ToInt32(textBox10.Text)/4; j++)
                    {
                        temp_list_of_process_2.Add(j, x);
                    }
                    
                    textBox13.Text = "p*= "+ p;
                }
              
                
            }

            drawing = pane.AddCurve("Delta", temp_list_of_process, Color.Blue);
            drawing = pane.AddCurve("Tau", temp_list_of_process_2, Color.Red);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
        #endregion
        #region Lab choise
        private void ChangeVisible(int page)
        {
            contentPanel.Visible = false;
            Lab1panel.Visible = false;
            Lab2panel.Visible = false;
            Lab3panel.Visible = false;
            Lab4panel.Visible = false;
            Lab5panel.Visible = false;
            Lab6panel.Visible = false;
            //Lab7panel.Visible = false;
            switch(page)
                {
                case 0: contentPanel.Visible = true; break;
                case 1: Lab1panel.Visible = true; break;
                case 2: Lab2panel.Visible = true; break;
                case 3: Lab3panel.Visible = true; break;
                case 4: Lab4panel.Visible = true; break;
                case 5: Lab5panel.Visible = true; break;
                case 6: Lab6panel.Visible = true; break;
                case 7: //Lab7panel.Visible = true; 
                    break;
                default:break;

            }
        }
        private void contentPage_Click(object sender, EventArgs e)
        {
            ChangeVisible(0);
        }
        private void Lab1_Click(object sender, EventArgs e)
        {
            ChangeVisible(1);
        }

        private void Lab2_Click(object sender, EventArgs e)
        {
            ChangeVisible(2);
        }
        private void Lab3_Click(object sender, EventArgs e)
        {
            ChangeVisible(3);
        }
        private void Lab4_Click(object sender, EventArgs e)
        {
            ChangeVisible(4);
        }

        private void Lab5_Click(object sender, EventArgs e)
        {
            ChangeVisible(5);
        }

        private void Lab6_Click(object sender, EventArgs e)
        {
            ChangeVisible(6);
        }

        private void Lab7_Click(object sender, EventArgs e)
        {
            ChangeVisible(7);
        }
        #endregion
        #region Лабораторная 3
        //Составить программу, которая формирует матрицу из n*n
        //случайных чисел.Определить произведение всех чисел в матрице.
        //Значение nменяется в пределах от 5 до 10 тысяч.
        private void CalculateLab3_Click(object sender, EventArgs e)
        {
            textBox19.Text = "---";
            Random rand = new Random();
            int matrixSize = rand.Next(5, 10000);
            if (!userandomnum.Checked)
            { 
                matrixSize = Convert.ToInt32(textBox17.Text);
                textBox17.Text = matrixSize.ToString();
            }
            long tempellapledTicks = 0;
            for (int k = 0; k < Convert.ToInt32(textBox18.Text); k++)
            {
                long ellapledTicks = DateTime.Now.Ticks;
                int[,] matrix = new int[matrixSize, matrixSize];
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        Random randNum = new Random();
                        matrix[i, j] = randNum.Next(5, 10000);
                    }
                }
                long multiply = 0;
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        multiply *= matrix[i, j];
                    }
                }
                ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
                tempellapledTicks += ellapledTicks;
            }
            tempellapledTicks = tempellapledTicks / Convert.ToInt32(textBox18.Text);
            tempellapledTicks= tempellapledTicks / TimeSpan.TicksPerMillisecond;
            textBox19.Text = tempellapledTicks.ToString();
            
        }

        private void useingoldvalues_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            string path = @"c:\Users\user\Documents\logFileLab3.txt";
            StreamReader sr = new StreamReader(path);
            string numbers = sr.ReadLine();
            int[] timeNeed = new int[0];
            int[] numberofNumbers = new int[0];
            int writeNum = 0;
            numbers=numbers.Remove(0, 1);
            foreach (var number in numbers.Split())
            {
                if ((writeNum % 2) == 0)
                {
                    Array.Resize(ref timeNeed, timeNeed.Length + 1);
                    timeNeed[timeNeed.Length - 1] = Convert.ToInt32(number);
                }
                else
                {
                    Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
                    numberofNumbers[numberofNumbers.Length - 1] = Convert.ToInt32(number);
                }
                writeNum++;
            }
            sr.Close();            
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Лабораторная работа №3";
            pane.YAxis.Title.Text = "Время выполнения (мс)";
            pane.XAxis.Title.Text = "Количество элементов массива ";
            pane.CurveList.Clear(); 
            LineItem drawing = new LineItem("0");
            PointPairList temp_list_data = new PointPairList();
            for (int i = 0; i < timeNeed.Length; i++)
            {
                temp_list_data.Add(numberofNumbers[i],timeNeed[i]);
            }
            drawing = pane.AddCurve("Архивные данные", temp_list_data, Color.Blue, SymbolType.TriangleDown);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void addcountpoint_Click(object sender, EventArgs e)
        {

            string path = @"c:\Users\user\Documents\logFileLab3.txt";
            StreamReader sr = new StreamReader(path);
            string numbers = sr.ReadLine();
            string numbers1 = sr.ReadToEnd();
            int[] timeNeed = new int[0];
            int[] numberofNumbers = new int[0];
            int writeNum = 0;
            numbers = numbers.Remove(0, 1);
            foreach (var number in numbers.Split())
            {
                if ((writeNum % 2) == 0)
                {
                    Array.Resize(ref timeNeed, timeNeed.Length + 1);
                    timeNeed[timeNeed.Length - 1] = Convert.ToInt32(number);
                }
                else
                {
                    Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
                    numberofNumbers[numberofNumbers.Length - 1] = Convert.ToInt32(number);
                }
                writeNum++;
            }
            sr.Close();
            Array.Resize(ref timeNeed, timeNeed.Length + 1);
            timeNeed[timeNeed.Length - 1] = Convert.ToInt32(textBox19.Text);
            Array.Sort(timeNeed);
            Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
            numberofNumbers[numberofNumbers.Length - 1] = Convert.ToInt32(textBox17.Text);
            Array.Sort(numberofNumbers);
            StreamWriter sw = new StreamWriter(path);
            for(int i=0;i< numberofNumbers.Length;i++)
            {
                sw.Write(" ");
                sw.Write(timeNeed[i]);
                sw.Write(" ");
                sw.Write(numberofNumbers[i]);
            }
            sw.Close();
        }
        #endregion
        #region Лабораторная 4
        //Разработать алгоритм и программу простого линейного поиска
        //с циклом For.В качестве исходных данных использовать строку
        //текста, из которой необходимо выделить слова.Аргумент поиска –слово.

        private void CalculateLab4_Click(object sender, EventArgs e)
        {
            for (int mainLoop = 51000; mainLoop <= Convert.ToInt32(textBox21.Text); mainLoop++)
            {
                if (checkBox1.Checked)
                { mainLoop = Convert.ToInt32(textBox21.Text); }
                long endTicks = 0;
                for (int t = 0; t < Convert.ToInt32(textBox20.Text); t++)
                {
                    var r = new Random();
                    var r1 = new Random();
                    int wordLenght = Convert.ToInt32(textBox22.Text);
                    int numberOfWords = mainLoop;
                    int numberOfFindWord = r1.Next(1, numberOfWords - 1);
                    string endText = "";
                    string findWord = "";
                    for (int i = 0; i < numberOfWords; i++)
                    {
                        string addString = new String(Enumerable.Range(0, wordLenght).Select(n => (Char)(r.Next(32, 127))).ToArray());
                        endText += addString;
                        endText += " ";
                        if (numberOfFindWord == i)
                        {
                            findWord = addString;
                        }
                    }

                    long ellapledTicks = DateTime.Now.Ticks;
                    foreach (var finFindWord in endText.Split())
                    {

                        if (finFindWord == findWord)
                        {
                            break;
                        }
                    }
                    ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
                    endTicks += ellapledTicks;
                }
                endTicks = endTicks / (10 * Convert.ToInt32(textBox20.Text));
                textBox23.Text = endTicks.ToString();


                MakeLogFileLab4(Convert.ToInt32(textBox22.Text), mainLoop, (int)endTicks);
                mainLoop += 999;
            }
        }

        private void UseOldData_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Лабораторная работа №4";
            pane.XAxis.Title.Text = "Время выполнения (микросекунд)";
            pane.YAxis.Title.Text = "Количество слов в строке ";
            pane.CurveList.Clear();
            LineItem drawing = new LineItem("0");
            string path = @"c:\Users\user\Documents\logFileLab4.txt";
            StreamReader sr = new StreamReader(path);
            
            string numbers1 = sr.ReadToEnd();
            string[] item2 = System.Text.RegularExpressions.Regex.Split(numbers1, "\n");
            
            for(int i =0;i< item2.Length;i++)
            {
                int writeNum = 0;
                int[] timeNeed = new int[0];
                int[] numberofWords = new int[0];
                string[] itemNumbers = item2[i].Split();
                for (int j = 1; j < itemNumbers.Length; j++)
                {
                    if (itemNumbers[j] != null)
                    {
                        if ((writeNum % 2) != 0)
                        {
                            Array.Resize(ref timeNeed, timeNeed.Length + 1);
                            timeNeed[timeNeed.Length - 1] = Convert.ToInt32(itemNumbers[j]);
                        }
                        else
                        {
                            Array.Resize(ref numberofWords, numberofWords.Length + 1);
                            numberofWords[numberofWords.Length - 1] = Convert.ToInt32(itemNumbers[j]);
                        }
                    }
                    writeNum++;
                }
                PointPairList temp_list_data = new PointPairList();
                for (int j = 0; j < timeNeed.Length; j++)
                {
                     temp_list_data.Add(timeNeed[j], numberofWords[j]);
                }
                if(i==0)
                {
                    drawing = pane.AddCurve(itemNumbers[0] + " words ", temp_list_data, Color.Blue, SymbolType.TriangleDown);
                }
                if (i == 1)
                {
                    drawing = pane.AddCurve(itemNumbers[0] + " words ", temp_list_data, Color.Red, SymbolType.TriangleDown);
                }
                if (i == 2)
                {
                    drawing = pane.AddCurve(itemNumbers[0] + " words ", temp_list_data, Color.Green, SymbolType.TriangleDown);
                }
            }

            sr.Close();
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
            
        }

        private void Addpointlab3_Click(object sender, EventArgs e)
        {
            string path = @"c:\Users\user\Documents\logFileLab4.txt";
            StreamReader sr = new StreamReader(path);
            string numbers1 = sr.ReadToEnd();
            string[] item2 = System.Text.RegularExpressions.Regex.Split(numbers1, @"\n");
            int lengthOfWord = Convert.ToInt32(textBox22.Text);
            int numberOfElements = Convert.ToInt32(textBox21.Text);
            int averageTime = Convert.ToInt32(textBox23.Text);

            bool findFlag = false;
            for (int i = 0; i < item2.Length; i++)
            {
                
                int[] itemNumbers = item2[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                if (itemNumbers[0] == lengthOfWord)
                {
                    findFlag = true;
                    int[] temp1 = new int [0];
                    int[] temp2 = new int [0];
                    for (int j = 1; j < itemNumbers.Length; j++)
                    {
                        
                            if (j % 2 == 1)
                            {
                                Array.Resize(ref temp1, temp1.Length + 1);
                                temp1[temp1.Length - 1] = itemNumbers[j];
                            }
                            else
                            {
                                Array.Resize(ref temp2, temp2.Length + 1);
                                temp2[temp2.Length - 1] = itemNumbers[j];
                            }
                        
                        
                    }
                    Array.Resize(ref temp1, temp1.Length + 1);
                    temp1[temp1.Length - 1] = numberOfElements;
                    Array.Resize(ref temp2, temp2.Length + 1);
                    temp2[temp2.Length - 1] = averageTime;
                    Array.Sort(temp1);
                    Array.Sort(temp2);
                    item2[i] = lengthOfWord.ToString();
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        item2[i] += " " + temp1[j].ToString() +" "+ temp2[j].ToString();
                    }

                    break;
                }
                else
                {
                    continue;
                }
            }
            if(!findFlag)
            {
                Array.Resize(ref item2, item2.Length + 1);
                item2[item2.Length - 1] = lengthOfWord.ToString();
                item2[item2.Length - 1] += " " + numberOfElements.ToString()+ " " + averageTime.ToString();
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < item2.Length; i++)
            {
                sw.Write(item2[i]);
                if(i!= item2.Length-1)
                {
                    sw.Write('\n');
                }  
            }
            sw.Close();
        }
        
        private void MakeLogFileLab4(int l, int n , int a)
        {
            string path = @"c:\Users\user\Documents\logFileLab4.txt";
            StreamReader sr = new StreamReader(path);
            string numbers1 = sr.ReadToEnd();
            string[] item2 = System.Text.RegularExpressions.Regex.Split(numbers1, @"\n");
            int lengthOfWord = l;
            int numberOfElements = n;
            int averageTime = a;

            bool findFlag = false;
            for (int i = 0; i < item2.Length; i++)
            {

                int[] itemNumbers = item2[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                if (itemNumbers[0] == lengthOfWord)
                {
                    findFlag = true;
                    int[] temp1 = new int[0];
                    int[] temp2 = new int[0];
                    for (int j = 1; j < itemNumbers.Length; j++)
                    {

                        if (j % 2 == 1)
                        {
                            Array.Resize(ref temp1, temp1.Length + 1);
                            temp1[temp1.Length - 1] = itemNumbers[j];
                        }
                        else
                        {
                            Array.Resize(ref temp2, temp2.Length + 1);
                            temp2[temp2.Length - 1] = itemNumbers[j];
                        }


                    }
                    Array.Resize(ref temp1, temp1.Length + 1);
                    temp1[temp1.Length - 1] = numberOfElements;
                    Array.Resize(ref temp2, temp2.Length + 1);
                    temp2[temp2.Length - 1] = averageTime;
                    Array.Sort(temp1);
                    Array.Sort(temp2);
                    item2[i] = lengthOfWord.ToString();
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        item2[i] += " " + temp1[j].ToString() + " " + temp2[j].ToString();
                    }

                    break;
                }
                else
                {
                    continue;
                }
            }
            if (!findFlag)
            {
                Array.Resize(ref item2, item2.Length + 1);
                item2[item2.Length - 1] = lengthOfWord.ToString();
                item2[item2.Length - 1] += " " + numberOfElements.ToString() + " " + averageTime.ToString();
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < item2.Length; i++)
            {
                sw.Write(item2[i]);
                if (i != item2.Length - 1)
                {
                    sw.Write('\n');
                }
            }
            sw.Close();
        }
        #endregion
        #region Лабораторная 5
        //Разработать следующие алгоритмы и программы с
        // использованием рекурсии.
        //Ввода одномерного массива и линейного поиска
        //целочисленного значения ключа в нем
        private void CalculateLab5_Click(object sender, EventArgs e)
        {
            long tempellapledTicks = 0;
            for (int k = 0; k < Convert.ToInt32(textBox26.Text); k++)
            {
                long ellapledTicks = DateTime.Now.Ticks;
                int sizeArray = Convert.ToInt32(textBox27.Text);
                maxNumber = sizeArray;
                int[] array = new int[sizeArray];
                makeArray(array, sizeArray - 1);
                var r = new Random();
                key = r.Next(1, maxNumber);
                int value = lineSearch(array, sizeArray - 1);
                ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
                tempellapledTicks += ellapledTicks;
            }
            tempellapledTicks = tempellapledTicks / Convert.ToInt32(textBox26.Text);
            tempellapledTicks = tempellapledTicks / 100;
            textBox24.Text = tempellapledTicks.ToString();
            AddDataToLog5((int)tempellapledTicks, Convert.ToInt32(textBox27.Text));
        }
        public int key = 0;
        public int maxNumber = 0;
        void makeArray(int[] arr, int size)
        { 
            for(int i =0;i < size;i++)
            {
                var r = new Random(i);
                arr[i] = r.Next(1, maxNumber);
            }
        }
        int lineSearch(int[] arr, int size)
        {

            if (size == -1)
                return 0;

            if (arr[size] == key)
                return size;

            return lineSearch(arr, size - 1);
        }
        private void AddDataToLog5(int time,int wnumbers)
        {

            string path = @"c:\Users\user\Documents\logFileLab5.txt";
            StreamReader sr = new StreamReader(path);
            string numbers = sr.ReadLine();
            int[] timeNeed = new int[0];
            int[] numberofNumbers = new int[0];
            int writeNum = 0;
            numbers = numbers.Remove(0, 1);
            foreach (var number in numbers.Split())
            {
                if ((writeNum % 2) == 0)
                {
                    Array.Resize(ref timeNeed, timeNeed.Length + 1);
                    timeNeed[timeNeed.Length - 1] = Convert.ToInt32(number);
                }
                else
                {
                    Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
                    numberofNumbers[numberofNumbers.Length - 1] = Convert.ToInt32(number);
                }
                writeNum++;
            }
            sr.Close();
            Array.Resize(ref timeNeed, timeNeed.Length + 1);
            timeNeed[timeNeed.Length - 1] = time;
            Array.Sort(timeNeed);
            Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
            numberofNumbers[numberofNumbers.Length - 1] = wnumbers;
            Array.Sort(numberofNumbers);
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < numberofNumbers.Length; i++)
            {
                sw.Write(" ");
                sw.Write(timeNeed[i]);
                sw.Write(" ");
                sw.Write(numberofNumbers[i]);
            }
            sw.Close();
        }
        private void DrawLab5_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            string path = @"c:\Users\user\Documents\logFileLab5.txt";
            StreamReader sr = new StreamReader(path);
            string numbers = sr.ReadLine();
            int[] timeNeed = new int[0];
            int[] numberofNumbers = new int[0];
            int writeNum = 0;
            numbers = numbers.Remove(0, 1);
            foreach (var number in numbers.Split())
            {
                if ((writeNum % 2) == 0)
                {
                    Array.Resize(ref timeNeed, timeNeed.Length + 1);
                    timeNeed[timeNeed.Length - 1] = Convert.ToInt32(number);
                }
                else
                {
                    Array.Resize(ref numberofNumbers, numberofNumbers.Length + 1);
                    numberofNumbers[numberofNumbers.Length - 1] = Convert.ToInt32(number);
                }
                writeNum++;
            }
            sr.Close();
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Лабораторная работа №5";
            pane.YAxis.Title.Text = "Время выполнения (мкс)";
            pane.XAxis.Title.Text = "Количество элементов массива ";
            pane.CurveList.Clear();
            LineItem drawing = new LineItem("0");
            PointPairList temp_list_data = new PointPairList();
            for (int i = 0; i < timeNeed.Length; i++)
            {
                temp_list_data.Add(numberofNumbers[i], timeNeed[i]);
            }
            drawing = pane.AddCurve("Архивные данные", temp_list_data, Color.Blue, SymbolType.TriangleDown);
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
        #endregion

        #region Лабораторная 6
        //Составить две программы, которые реализуют алгоритмы
        // ускоренной сортировки «пузырьком» и выбором.Исходные данные
        //задавать с помощью датчика случайных чисел.

       

        private void CalculateLab6_Click(object sender, EventArgs e)
        {
            for (int i = 100; i <= 10000; i++)
            {
                long BubTempEllapledTicks = 0;
                long ChoTempEllapledTicks = 0;
                long QuickTempEllapledTicks = 0;
                for (int k = 0; k < Convert.ToInt32(textBox28.Text); k++)
                {

                    int sizeArray = i;// Convert.ToInt32(textBox29.Text);
                    maxNumber = sizeArray;
                    int[] array = new int[sizeArray];
                    makeArray(array, sizeArray - 1);

                    int[] BubArray = array;
                    int[] SelArray = array;
                    int[] SoArray = array;

                    long BubEllapledTicks = DateTime.Now.Ticks;
                    fastBubbleSort(BubArray);
                    BubEllapledTicks = DateTime.Now.Ticks - BubEllapledTicks;
                    BubTempEllapledTicks += BubEllapledTicks;
                    long ChoEllapledTicks = DateTime.Now.Ticks;
                    SelectionSort(SelArray);
                    ChoEllapledTicks = DateTime.Now.Ticks - ChoEllapledTicks;
                    ChoTempEllapledTicks += ChoEllapledTicks;

                    long QuickEllapledTicks = DateTime.Now.Ticks;
                    Array.Sort(SoArray);
                    QuickEllapledTicks = DateTime.Now.Ticks - QuickEllapledTicks;
                    QuickTempEllapledTicks += QuickEllapledTicks;

                }
                BubTempEllapledTicks = BubTempEllapledTicks / Convert.ToInt32(textBox28.Text);
                BubTempEllapledTicks = BubTempEllapledTicks / 100;
                ChoTempEllapledTicks = ChoTempEllapledTicks / Convert.ToInt32(textBox28.Text);
                ChoTempEllapledTicks = ChoTempEllapledTicks / 100;
                QuickTempEllapledTicks = QuickTempEllapledTicks / Convert.ToInt32(textBox28.Text);
                QuickTempEllapledTicks = QuickTempEllapledTicks / 100;
                textBox25.Text = BubTempEllapledTicks.ToString();
                textBox30.Text = ChoTempEllapledTicks.ToString();
                MakeLogFileLab6(1, i, (int)BubTempEllapledTicks);
                MakeLogFileLab6(2, i, (int)ChoTempEllapledTicks);
                MakeLogFileLab6(3, i, (int)QuickTempEllapledTicks);

                i += 99;
            }
        }
        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
        void fastBubbleSort(int[] arr)
        {
            var len = arr.Length;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
        }
        int IndexOfMin(int[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }
        //сортировка выбором
        int[] SelectionSort(int[] array, int currentIndex = 0)
        {
            if (currentIndex == array.Length)
                return array;

            var index = IndexOfMin(array, currentIndex);
            if (index != currentIndex)
            {
                Swap(ref array[index], ref array[currentIndex]);
            }

            return SelectionSort(array, currentIndex + 1);
        }
        private void DrawLab6_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.Visible = true;
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.Title.Text = "Лабораторная работа №6";
            pane.YAxis.Title.Text = "Время выполнения (мкс)";
            pane.XAxis.Title.Text = "Количество элементов в массиве ";
            pane.CurveList.Clear();
            LineItem drawing = new LineItem("0");
            string path = @"c:\Users\user\Documents\logFileLab6.txt";
            StreamReader sr = new StreamReader(path);

            string numbers1 = sr.ReadToEnd();
            string[] item2 = System.Text.RegularExpressions.Regex.Split(numbers1, "\n");

            for (int i = 0; i < item2.Length; i++)
            {
                int writeNum = 0;
                int[] timeNeed = new int[0];
                int[] numberofWords = new int[0];
                string[] itemNumbers = item2[i].Split();
                for (int j = 1; j < itemNumbers.Length; j++)
                {
                    if (itemNumbers[j] != null)
                    {
                        if ((writeNum % 2) != 0)
                        {
                            Array.Resize(ref timeNeed, timeNeed.Length + 1);
                            timeNeed[timeNeed.Length - 1] = Convert.ToInt32(itemNumbers[j]);
                        }
                        else
                        {
                            Array.Resize(ref numberofWords, numberofWords.Length + 1);
                            numberofWords[numberofWords.Length - 1] = Convert.ToInt32(itemNumbers[j]);
                        }
                    }
                    writeNum++;
                }
                PointPairList temp_list_data = new PointPairList();
                for (int j = 0; j < timeNeed.Length; j++)
                {
                    temp_list_data.Add(numberofWords[j],timeNeed[j]);
                }
                if (i == 0)
                {
                    drawing = pane.AddCurve(" Сортировка пузырьком ", temp_list_data, Color.Blue, SymbolType.TriangleDown);
                }
                if (i == 1)
                {
                    drawing = pane.AddCurve(" Сортировка вставками ", temp_list_data, Color.Red, SymbolType.TriangleDown);
                }
                if (i == 2)
                {
                    drawing = pane.AddCurve(" Встроенная сортировка ", temp_list_data, Color.Green, SymbolType.TriangleDown);
                }

            }

            sr.Close();
            zedGraphControl1.GraphPane.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void MakeLogFileLab6(int l, int n, int a)
        {
            string path = @"c:\Users\user\Documents\logFileLab6.txt";
            StreamReader sr = new StreamReader(path);
            string numbers1 = sr.ReadToEnd();
            string[] item2 = System.Text.RegularExpressions.Regex.Split(numbers1, @"\n");
            int lengthOfWord = l;
            int numberOfElements = n;
            int averageTime = a;

            
            for (int i = 0; i < item2.Length; i++)
            {

                int[] itemNumbers = item2[i].Split(' ').Select(x => int.Parse(x)).ToArray();
                if (itemNumbers[0] == l)
                {
                   
                    int[] temp1 = new int[0];
                    int[] temp2 = new int[0];
                    for (int j = 1; j < itemNumbers.Length; j++)
                    {

                        if (j % 2 == 1)
                        {
                            Array.Resize(ref temp1, temp1.Length + 1);
                            temp1[temp1.Length - 1] = itemNumbers[j];
                        }
                        else
                        {
                            Array.Resize(ref temp2, temp2.Length + 1);
                            temp2[temp2.Length - 1] = itemNumbers[j];
                        }


                    }
                    Array.Resize(ref temp1, temp1.Length + 1);
                    temp1[temp1.Length - 1] = numberOfElements;
                    Array.Resize(ref temp2, temp2.Length + 1);
                    temp2[temp2.Length - 1] = averageTime;
                    Array.Sort(temp1);
                    Array.Sort(temp2);
                    item2[i] = lengthOfWord.ToString();
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        item2[i] += " " + temp1[j].ToString() + " " + temp2[j].ToString();
                    }

                    break;
                }
                else if(itemNumbers[0] == l)
                {
                    
                    int[] temp1 = new int[0];
                    int[] temp2 = new int[0];
                    for (int j = 1; j < itemNumbers.Length; j++)
                    {

                        if (j % 2 == 1)
                        {
                            Array.Resize(ref temp1, temp1.Length + 1);
                            temp1[temp1.Length - 1] = itemNumbers[j];
                        }
                        else
                        {
                            Array.Resize(ref temp2, temp2.Length + 1);
                            temp2[temp2.Length - 1] = itemNumbers[j];
                        }


                    }
                    Array.Resize(ref temp1, temp1.Length + 1);
                    temp1[temp1.Length - 1] = numberOfElements;
                    Array.Resize(ref temp2, temp2.Length + 1);
                    temp2[temp2.Length - 1] = averageTime;
                    Array.Sort(temp1);
                    Array.Sort(temp2);
                    item2[i] = lengthOfWord.ToString();
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        item2[i] += " " + temp1[j].ToString() + " " + temp2[j].ToString();
                    }

                    break;
                }
                else if(itemNumbers[0] == l)
                {
                    int[] temp1 = new int[0];
                    int[] temp2 = new int[0];
                    for (int j = 1; j < itemNumbers.Length; j++)
                    {

                        if (j % 2 == 1)
                        {
                            Array.Resize(ref temp1, temp1.Length + 1);
                            temp1[temp1.Length - 1] = itemNumbers[j];
                        }
                        else
                        {
                            Array.Resize(ref temp2, temp2.Length + 1);
                            temp2[temp2.Length - 1] = itemNumbers[j];
                        }


                    }
                    Array.Resize(ref temp1, temp1.Length + 1);
                    temp1[temp1.Length - 1] = numberOfElements;
                    Array.Resize(ref temp2, temp2.Length + 1);
                    temp2[temp2.Length - 1] = averageTime;
                    Array.Sort(temp1);
                    Array.Sort(temp2);
                    item2[i] = lengthOfWord.ToString();
                    for (int j = 0; j < temp1.Length; j++)
                    {
                        item2[i] += " " + temp1[j].ToString() + " " + temp2[j].ToString();
                    }

                    break;
                }
            }
            sr.Close();
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < item2.Length; i++)
            {
                sw.Write(item2[i]);
                if (i != item2.Length - 1)
                {
                    sw.Write('\n');
                }
            }
            sw.Close();
        }
        #endregion
    }
}
