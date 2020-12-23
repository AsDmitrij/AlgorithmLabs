using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SystAnalys_lr1
{
    public partial class Form1 : Form
    {
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        int[,] AMatrix;
        int[,] IMatrix;

        public int[,] DrawMatrix = { {0,0,1,1},
                            { 1,0,1,0},
                            { 1,1,0,0},
                             { 0,0,0,0} };
        public static int numberOfVertices = 0;

        public Form1()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            sheet.Image = G.GetBitmap();
        }
        private void DrawFromMatrix_Click(object sender, EventArgs e)
        {
            numberOfVertices = Convert.ToInt32(textBox1.Text);
            int[,] DrawMatrix = new int[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    DrawMatrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            int x = 50;
            int y = 50;
            Random randomX = new Random();
            Random randomY = new Random();
            V.Clear();
            E.Clear();
            G.clearSheet();
            for (int i = 0; i < numberOfVertices; i++)
            {
                x = randomX.Next(20, 500);
                y = randomX.Next(20, 500);
                V.Add(new Vertex(x, y));
                G.drawVertex(x, y, V.Count.ToString());
                sheet.Image = G.GetBitmap();
            }
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (DrawMatrix[i, j] != 0)
                    {
                        E.Add(new Edge(i, j));
                        G.drawEdge(V[i], V[j], E[E.Count - 1], E.Count - 1);
                        sheet.Image = G.GetBitmap();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBoxMatrix.Items.Clear();
            int numberOfVertices = Convert.ToInt32(textBox1.Text);
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (DrawMatrix[i, j] != 0)
                    {

                        String str = (i + 1).ToString() + " → " + (j + 1).ToString();
                        listBoxMatrix.Items.Add(str);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBoxMatrix.Items.Clear();
            int numberOfVertices = Convert.ToInt32(textBox1.Text);
            int posOfVertex = 0;
            for (int i = numberOfVertices - 1; i >= 0; i--)
            {
                for (int j = numberOfVertices - 1; j >= 0; j--)
                {
                    if (DrawMatrix[i, j] != 0)
                    {

                        String str = (char)('a' + posOfVertex) + "   " + (i + 1).ToString() + " → " + (j + 1).ToString();
                        listBoxMatrix.Items.Add(str);
                        posOfVertex++;
                    }
                }
            }
        }
        private void depthFirstWalk_Click(object sender, EventArgs e)
        {
            listBoxMatrix.Items.Clear();
            listBoxMatrix.Items.Add("Обход графа в глубину");

            PrintWidth(DrawMatrix);
        }
        private void getRandomMaitrix_Click(object sender, EventArgs e)
        {
            int verticesCount = Convert.ToInt32(textBox1.Text);
            int[,] RandomMatrix = new int[verticesCount, verticesCount];
            Random rnd = new Random();
            for (int row = 0; row < verticesCount - 1; row++)
            {
                for (int col = row + 1; col < verticesCount; col++)
                    if (rnd.Next(3) < 1)
                    {
                        RandomMatrix[row, col] = 1;
                        RandomMatrix[col, row] = 1;
                    }
            }
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = RandomMatrix[i, j];
                }
            }
            DrawMatrix = RandomMatrix;
        }
        static bool IsVerifyGraf(int[,] adjacency)
        {
            int verticesCount = adjacency.GetLength(0);
            if (verticesCount != adjacency.GetLength(1))
            {
                Console.WriteLine("Ошибка! Матрица смежности неверной размерности!");
                return false;
            }
            bool error = false;
            for (int row = 0; row < verticesCount; row++)
            {
                if (adjacency[row, row] != 0)
                    error = true;
                for (int col = row + 1; col < verticesCount; col++)
                    if (adjacency[row, col] != adjacency[col, row])
                    {
                        error = true;
                        break;
                    }
                if (error)
                    break;
            }
            if (error)
            {
                Console.WriteLine("Ошибка! Матрица смежности ошибочна!");
                return false;
            }
            return true;
        }
        private void PrintWidth(int[,] adjacency)
        {
            if (!IsVerifyGraf(adjacency))
                return;

            int verticesCount = adjacency.GetLength(0);

            List<int> vertList = new List<int>();
            Queue<int> vertQueue = new Queue<int>();

            for (int vert = 0; vert < verticesCount; vert++)
            {

                int vertCurr = vert;
                while (true)
                {
                    if (vertList.IndexOf(vertCurr) < 0)
                    {
                        PrintVert(vertCurr, adjacency);
                        listBoxMatrix.Items.Add('\n');
                        vertList.Add(vertCurr);

                        for (int col = 0; col < verticesCount; col++)
                            if (adjacency[vertCurr, col] != 0 && vertList.IndexOf(col) < 0)
                                vertQueue.Enqueue(col);
                    }

                    if (vertQueue.Count == 0)
                        break;

                    vertCurr = vertQueue.Dequeue();
                }

            }

        }

        private void PrintVert(int Vert, int[,] adjacency)
        {
            if (!IsVerifyGraf(adjacency))
                return;
            listBoxMatrix.Items.Add($"Вершина {Vert+1} ->:");
            int verticesCount = adjacency.GetLength(0);
            for (int col = 0; col < verticesCount; col++)
                if (adjacency[Vert, col] != 0)
                    listBoxMatrix.Items.Add($"  {col+1}");
        }

        private void PrintDeep(int[,] adjacency)
        {
            if (!IsVerifyGraf(adjacency))
                return;

            int verticesCount = adjacency.GetLength(0);

            List<int> vertList = new List<int>();
            Stack<int> vertStack = new Stack<int>();

            for (int vert = 0; vert < verticesCount; vert++)
            {
                int vertCurr = vert;
                while (true)
                {
                    if (vertList.IndexOf(vertCurr) < 0)
                    {
                        PrintVert(vertCurr, adjacency);
                        listBoxMatrix.Items.Add('\n');
                        vertList.Add(vertCurr);

                        for (int col = 0; col < verticesCount; col++)
                            if (adjacency[vertCurr, col] != 0 && vertList.IndexOf(col) < 0)
                                vertStack.Push(col);
                    }

                    if (vertStack.Count == 0)
                        break;

                    vertCurr = vertStack.Pop();
                }

            }

        }
        int[] FindShortWay(int numberOfVertices, int[,] MainMatrix)
        {
            int[] answer = new int[0];
            int[,] HelpMatrix = new int[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (MainMatrix[i, j]==0)
                    {
                        MainMatrix[i, j] = int.MaxValue;
                    }
                    HelpMatrix[i, j] = j + 1;
                }
            }
            bool flag = false;
            for (int stage = 0; stage < numberOfVertices; stage++)
            {
                for (int i = 0; i < numberOfVertices; i++)
                {
                    for (int j = 0; j < numberOfVertices; j++)
                    {
                        if (i != stage && j != stage && MainMatrix[i, stage] < 10000 && MainMatrix[stage, j] < 10000)
                        {
                            int sum = MainMatrix[i, stage] + MainMatrix[stage, j];
                            if (MainMatrix[i, j] > sum && sum < 10000 && sum > -10000)
                            {
                                MainMatrix[i, j] = sum;
                                HelpMatrix[i, j] = HelpMatrix[i, stage];
                                if (i == j && sum < 0)
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    goto answer;
                }
            }
            if (MainMatrix[0, numberOfVertices - 1] < 10000)
            {
                int startVertex = Convert.ToInt32(textBox3.Text);
                int last_i = Convert.ToInt32(textBox3.Text)-1;
                int last_j = Convert.ToInt32(textBox4.Text)-1;
                answer = new int[1] { startVertex };
                while (Convert.ToInt32(textBox4.Text)!= HelpMatrix[last_i, last_j])
                {
                    Array.Resize(ref answer, answer.Length + 1);
                    answer[answer.Length - 1] = HelpMatrix[last_i, last_j];
                    last_i = HelpMatrix[last_i, last_j] - 1;
                }
                Array.Resize(ref answer, answer.Length + 1);
                answer[answer.Length - 1] = HelpMatrix[last_i, last_j];
            }
        answer:;
            return answer;
        }
        void DrawPoints()
        {
            textBox2.Text = "";
            int[,] tempMatrix = new int[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    tempMatrix[i, j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            int[] DrawArray = FindShortWay(numberOfVertices, tempMatrix);
            for (int i = 0; i < DrawArray.Length; i++)
            {
                textBox2.Text += DrawArray[i].ToString() + " -> ";
            }
            string resultText = textBox2.Text.Remove(textBox2.Text.Length - 3); ;
            textBox2.Text = resultText;


        }
        private void FindLoop_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = "";
            nodes = new int[Convert.ToInt32(textBox1.Text)];
            for (int i = 0; i < numberOfVertices; i++) 
            nodes[i] = 0;
            search(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
            DrawPoints();
        }
        int[] nodes= new int[8]; 
        void search(int st, int n)
        {
            int r;
            textBox2.Text += (st + 1).ToString() + " -> ";
            nodes[st] = 1;
            for (r = 0; r < n; r++)
                if ((DrawMatrix[st,r] != 0) && (nodes[r] == 0))
                    search(r, n);
        }


    }
}
