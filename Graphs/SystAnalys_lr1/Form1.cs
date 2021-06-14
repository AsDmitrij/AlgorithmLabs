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

        public static int[,] DrawMatrix = { {0,0,1,1},
                            { 1,0,1,0},
                            { 1,1,0,0},
                             { 0,0,0,0} };
        public static int numberOfVertices = 0;

        public static int[][] finalPathArray;
        public int[] colorsOfVertex;

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
            DrawFromMatrixNoColor();
        }
        private void DrawFromMatrixNoColor()
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
            listBoxMatrix.Items.Add($"Вершина {Vert + 1} ->:");
            int verticesCount = adjacency.GetLength(0);
            for (int col = 0; col < verticesCount; col++)
                if (adjacency[Vert, col] != 0)
                    listBoxMatrix.Items.Add($"  {col + 1}");
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
                    if (MainMatrix[i, j] == 0)
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
                int last_i = Convert.ToInt32(textBox3.Text) - 1;
                int last_j = Convert.ToInt32(textBox4.Text) - 1;
                answer = new int[1] { startVertex };
                while (Convert.ToInt32(textBox4.Text) != HelpMatrix[last_i, last_j])
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
        int[] nodes = new int[8];
        void search(int st, int n)
        {
            int r;
            textBox2.Text += (st + 1).ToString() + " -> ";
            nodes[st] = 1;
            for (r = 0; r < n; r++)
                if ((DrawMatrix[st, r] != 0) && (nodes[r] == 0))
                    search(r, n);
        }

        private void GetMatrix_Click(object sender, EventArgs e)
        {
            numberOfVertices = Convert.ToInt32(textBox1.Text);
            int demention = Convert.ToInt32(textBox1.Text);
            dataGridView1.RowCount = demention;
            dataGridView1.ColumnCount = demention;
            for (int i = 0; i < demention; i++)
                dataGridView1.Columns[i].Width = 20;

        }

        private void TaskValues_Click(object sender, EventArgs e)
        {
            int verticesCount = Convert.ToInt32(textBox1.Text);
            int[,] RandomMatrix = { { 0,12, 4, 5,10, 8},
                                    {12, 0, 5, 4,10, 2},
                                    { 4, 5, 0,10,15, 3},
                                    { 5, 4,10, 0, 5, 6},
                                    {10,0,0, 5, 0, 8},
                                    { 8, 2, 3, 6, 8, 0}};
            DrawMatrix = RandomMatrix;
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = DrawMatrix[i, j];
                }
            }
            LittleAlgoritm();
        }

        #region Seleman problem
        public static void LittleAlgoritm()
        {
            numberOfVertices = 6;
            int n = numberOfVertices;
            int[][] indexes = new int[2][];
            Array.Resize(ref indexes[0], n);
            Array.Resize(ref indexes[1], n);
            int[,] HelpMatrix = new int[numberOfVertices, numberOfVertices];
            for (int i = 0; i < n; i++)
            {
                indexes[0][i] = i;
                indexes[1][i] = i;
                for (int j = 0; j < n; j++)
                {
                    if (DrawMatrix[i, j] == 0)
                    {
                        HelpMatrix[i, j] = 100;
                    }
                    else
                    {
                        HelpMatrix[i, j] = DrawMatrix[i, j];
                    }
                }
            }
            int[][] pathArray = new int[1][];
            int[][] costArray = new int[1][];
            for (int tp = 0; tp < numberOfVertices; tp++)
            {
                int sumMinE = getZeroidMatrix(HelpMatrix, n);
                int[,] FirstFineMatrix = new int[n, n];
                int maxFine = getFineOfMatrix(HelpMatrix, FirstFineMatrix, n);
                int[] vertexes = getFineElement(FirstFineMatrix, n, maxFine, indexes);
                Array.Resize(ref pathArray[0], (numberOfVertices - n + 1) * 4);
                Array.Resize(ref costArray[0], (numberOfVertices - n + 1) * 4);

                pathArray[0][(numberOfVertices - n) * 2] = vertexes[0];
                pathArray[0][(numberOfVertices - n) * 2 + 1] = vertexes[1];
                costArray[0][numberOfVertices - n] = maxFine + sumMinE;

                int[,] MatrixC1 = HelpMatrix;

                costArray[0][0] = getZeroidMatrix(MatrixC1, n) + sumMinE;
                deleteIndexes(indexes, vertexes, n);

                n = Convert.ToInt32(Math.Sqrt(MatrixC1.Length)) - 1;
                int[,] contractionMatrix = new int[n, n];
                if (n == 2)
                {
                    contractMatrix(contractionMatrix, MatrixC1, vertexes, n, indexes);
                    getLastPath(indexes, contractionMatrix, pathArray, costArray, n);
                    break;
                }
                contractMatrix(contractionMatrix, MatrixC1, vertexes, n, indexes);
                HelpMatrix = contractionMatrix;
                sumMinE = getZeroidMatrix(HelpMatrix, n);
                becomeInfinity(contractionMatrix, indexes, vertexes);
                HelpMatrix = contractionMatrix;
            }
            Array.Resize(ref pathArray, numberOfVertices * 2);
            finalPathArray = pathArray;
        }

        public static void getLastPath(int[][] indexes, int[,] contractionMatrix, int[][] pathArray, int[][] costArray, int n)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (contractionMatrix[i, j] != 100)
                    {
                        Array.Resize(ref pathArray[0], (numberOfVertices - n + 1) * 4);
                        pathArray[0][(numberOfVertices - n) * 2] = indexes[0][i];
                        pathArray[0][(numberOfVertices - n) * 2 + 1] = indexes[1][j];
                        n--;
                    }
                }
            }
        }
        public static int getZeroidMatrix(int[,] HelpMatrix, int n)
        {
            int sumMinE = 0;
            for (int j = 0; j < n; j++)
            {
                int minE = 100;
                for (int i = 0; i < n; i++)
                {
                    if (HelpMatrix[i, j] < minE) { minE = HelpMatrix[i, j]; }
                }
                for (int i = 0; i < n; i++)
                {
                    if (HelpMatrix[i, j] != 100) { HelpMatrix[i, j] -= minE; }
                }
                sumMinE += minE;
            }

            for (int i = 0; i < n; i++)
            {
                int minE = 100;
                for (int j = 0; j < n; j++)
                {
                    if (HelpMatrix[i, j] < minE) { minE = HelpMatrix[i, j]; }
                }
                for (int j = 0; j < n; j++)
                {
                    if (HelpMatrix[i, j] != 100) { HelpMatrix[i, j] -= minE; }
                }
                sumMinE += minE;
            }
            return sumMinE;
        }
        public static int getFineOfMatrix(int[,] HelpMatrix, int[,] FirstFineMatrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (HelpMatrix[i, j] == 0)
                    {
                        int firstFine = 100;
                        for (int l = 0; l < n; l++)
                        {
                            if (l != i && firstFine > HelpMatrix[l, j])
                            {
                                firstFine = HelpMatrix[l, j];
                            }
                        }
                        int secondFine = 100;
                        for (int w = 0; w < n; w++)
                        {
                            if (w != j && secondFine > HelpMatrix[i, w])
                            {
                                secondFine = HelpMatrix[i, w];
                            }
                        }
                        FirstFineMatrix[i, j] = firstFine + secondFine;
                    }
                }
            }
            int maxFine = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (FirstFineMatrix[i, j] > maxFine)
                    {
                        maxFine = FirstFineMatrix[i, j];
                    }
                }
            }
            return maxFine;

        }
        public static int[] getFineElement(int[,] FirstFineMatrix, int n, int maxFine, int[][] indexes)
        {
            int[] a = new int[2];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (FirstFineMatrix[i, j] == maxFine)
                    {
                        a[0] = indexes[0][i];
                        a[1] = indexes[1][j];
                        return a;
                    }
                }
            }
            return a;
        }
        public static void deleteIndexes(int[][] indexes, int[] vertexes, int n)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (indexes[i][j] == vertexes[i])
                    {
                        int[] temp = indexes[i];
                        Array.Resize(ref indexes[i], indexes[i].Length - 1);
                        int m = 0;
                        for (int l = 0; l < temp.Length; l++)
                        {
                            if (l != j)
                            {
                                indexes[i][m] = temp[l];
                                m++;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static void contractMatrix(int[,] contractionMatrix, int[,] MatrixC1, int[] vertexes, int n, int[][] indexes)
        {
            int l = 0, m = 0;
            int[] a = new int[2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < indexes[i].Length; j++)
                {
                    if (indexes[i][j] == vertexes[i])
                    {
                        a[i] = j;
                    }
                }
            }
            for (int i = 0; i < n + 1; i++)
            {
                if (i != a[0])
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        if (j != a[1])
                        {
                            contractionMatrix[l, m] = MatrixC1[i, j];
                            m++;
                        }

                    }
                    m = 0;
                    l++;
                }
            }
        }

        public static bool becomeInfinity(int[,] contractionMatrix, int[][] indexes, int[] vertexes)
        {
            for (int i = 0; i < indexes[0].Length; i++)
            {
                if (indexes[0][i] == vertexes[0])
                {
                    for (int j = 0; j < indexes[1].Length; j++)
                    {
                        if (indexes[1][j] == vertexes[1])
                        {
                            contractionMatrix[i, j] = 100;
                            return true;
                        }
                    }
                }
            }
            for (int i = 0; i < indexes[0].Length; i++)
            {
                if (indexes[1][i] == vertexes[0])
                {
                    for (int j = 0; j < indexes[1].Length; j++)
                    {
                        if (indexes[0][j] == vertexes[1])
                        {
                            contractionMatrix[j, i] = 100;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void Salesman_Click(object sender, EventArgs e)
        {
            outputResult();
        }
        public void outputResult()
        {
            int cost = 0;
            listBoxMatrix.Items.Clear();
            for (int i = 0; i < numberOfVertices * 2; i++)
            {
                cost += DrawMatrix[finalPathArray[0][i], finalPathArray[0][i + 1]];
                String str = (finalPathArray[0][i] + 1).ToString() + " → " + (finalPathArray[0][i + 1] + 1).ToString() + " Cost:" + cost.ToString();
                listBoxMatrix.Items.Add(str);
                i++;
            }
            String str1 = "Total cost: " + cost.ToString();
            listBoxMatrix.Items.Add(str1);

        }
        #endregion

        #region Graph drawing

        private void greedy_algorithm_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[][] vertexArray = new int[2][];
            vertexArray[0] = new int[10] { 1, 1, 2, 2, 2, 3, 3, 4, 5, 6 };
            vertexArray[1] = new int[10] { 4, 5, 5, 6, 7, 6, 7, 5, 6, 7 };
            //vertexArray[0] = new int[9] { 1,1,1,2,3,3,3,4,5 };
           // vertexArray[1] = new int[9] {2,3,6,4,4,5,6,5,6 };
            int[][] degreeTable = new int[2][];
            degreeTable[0] = new int[n];
            degreeTable[1] = new int[n];
            int[,] incziMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                incziMatrix[i, i] = 1;
            }
            for (int j = 0; j < vertexArray[0].Length; j++)
            {
                incziMatrix[vertexArray[0][j] - 1, vertexArray[1][j] - 1] = 1;
                incziMatrix[vertexArray[1][j] - 1, vertexArray[0][j] - 1] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = incziMatrix[i, j];
                }
            }

            colorsOfVertex = greedyalgorithm(vertexArray);
            DrawFromMatrixColor(0);
        }
        private int[]  greedyalgorithm(int[][] vertexArray)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[][] degreeTable = new int[2][];
            degreeTable[0] = new int[n];
            degreeTable[1] = new int[n];
            int[,] incziMatrix = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                incziMatrix[i, i] = 1;
            }
            for (int j = 0; j < vertexArray[0].Length; j++)
            {
                incziMatrix[vertexArray[0][j] - 1, vertexArray[1][j] - 1] = 1;
                incziMatrix[vertexArray[1][j] - 1, vertexArray[0][j] - 1] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = incziMatrix[i, j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < vertexArray[0].Length; j++)
                {
                    if ((i + 1) == vertexArray[0][j])
                    {
                        degreeTable[0][i]++;
                    }
                    if ((i + 1) == vertexArray[1][j])
                    {
                        degreeTable[0][i]++;
                    }
                }
            }
            for (int iter = 0; iter < n; iter++)
            {

                int maxIndex = degreeTable[0].ToList().IndexOf(degreeTable[0].Max());
                degreeTable[0][maxIndex] = 0;
                if (degreeTable[0].Sum() == 0)
                {
                    break;
                }
                int point = 1;
                for (int depth = 2; depth < degreeTable.Length; depth++)
                {
                    if (degreeTable[depth][maxIndex] == point)
                    {
                        point++;
                        depth = 1;
                    }
                }
                degreeTable[1][maxIndex] = point;
                Array.Resize(ref degreeTable, degreeTable.Length + 1);
                int ord = degreeTable.Length - 1;
                degreeTable[ord] = new int[n];
                for (int j = 0; j < vertexArray[0].Length; j++)
                {
                    if ((maxIndex + 1) == vertexArray[0][j])
                    {
                        if (degreeTable[0][vertexArray[1][j] - 1] != 0)
                        {
                            degreeTable[0][vertexArray[1][j] - 1] -= 1;
                        }
                        degreeTable[ord][vertexArray[1][j] - 1] = point;
                    }

                    if ((maxIndex + 1) == vertexArray[1][j])
                    {
                        if (degreeTable[0][vertexArray[0][j] - 1] != 0)
                        {
                            degreeTable[0][vertexArray[0][j] - 1] -= 1;
                        }

                        degreeTable[ord][vertexArray[0][j] - 1] = point;
                    }
                }

            }
            for (int iter = 0; iter < n; iter++)
            {
                if (degreeTable[1][iter] == 0)
                {
                    int point = 1;
                    for (int depth = 2; depth < degreeTable.Length; depth++)
                    {
                        if (degreeTable[depth][iter] == point)
                        {
                            point++;
                            depth = 1;
                        }
                    }
                    degreeTable[1][iter] = point;
                }
            }
            return degreeTable[1];
        }

        private void Draw_string_algorithm_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[][] vertexArray = new int[2][];
            vertexArray[0] = new int[10] { 1, 1, 2, 2, 2, 3, 3, 4, 5, 6 };
            vertexArray[1] = new int[10] { 4, 5, 5, 6, 7, 6, 7, 5, 6, 7 };
            //vertexArray[0] = new int[9] { 1,1,1,2,3,3,3,4,5 };
           // vertexArray[1] = new int[9] {2,3,6,4,4,5,6,5,6 };
            int[] colors = new int[n];
            int[,] incziMatrix = new int[n,n];
            for (int i = 0; i < n; i++)
            {
                incziMatrix[i, i] = 1;
            }
            for (int j = 0; j < vertexArray[0].Length; j++)
            {
                incziMatrix[vertexArray[0][j] - 1, vertexArray[1][j] - 1] = 1;
                incziMatrix[vertexArray[1][j] - 1, vertexArray[0][j] - 1] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = incziMatrix[i, j];
                }
            }
            int color = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(incziMatrix[i, j] == 0)
                    {
                        colors[i] = color;
                        if (incziMatrix[j, 0] == 2)
                        {
                            color += 1;
                            break;
                        }
                        for (int l = 0; l < n; l++)
                        {
                            incziMatrix[i, l] += incziMatrix[j, l];
                            if(incziMatrix[i, l] == 2)
                            {
                                incziMatrix[i, l] = 1;
                            }
                        }
                        colors[j] = color;
                        color += 1;
                        incziMatrix[i, 0] = 2;
                        incziMatrix[j, 0] = 2;
                        break;
                    }
                    if(incziMatrix[i, j] == 2)
                    {
                        break;
                    }
                }
            }
            DrawFromMatrixColor(0);
        }

        private void DrawFromMatrixColor(int type)
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
            int c = 0;
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    if (DrawMatrix[i, j] != 0)
                    {
                        E.Add(new Edge(i, j));
                        if(type==1)
                        {
                            G.drawColorEdge(V[i], V[j], E[E.Count - 1], E.Count - 1, colorsOfVertex[c], type);
                            c++;
                        }
                        else
                        {
                            G.drawColorEdge(V[i], V[j], E[E.Count - 1], E.Count - 1, colorsOfVertex[j], type);
                        }
                        sheet.Image = G.GetBitmap();
                    }
                }
            }
        }

        private void EdgeColoring_Click(object sender, EventArgs e)
        {
            numberOfVertices = Convert.ToInt32(textBox1.Text);
            int[][] vertexArray = new int[2][];
            vertexArray[0] = new int[5] { 1, 1, 2, 2, 3 };
            vertexArray[1] = new int[5] { 3, 2, 3, 4, 4 };
            int[,] srawMatrix =  {{0,1,1,0},
                                 { 0,0,1,0},
                                 { 0,0,0,1},
                                 { 0,0,0,0}};

            colorsOfVertex = new int[4];
            colorsOfVertex = greedyalgorithm(vertexArray);
            for (int i = 0; i < numberOfVertices; i++)
            {
                for (int j = 0; j < numberOfVertices; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = srawMatrix[i, j];
                }
            }
            DrawFromMatrixColor(1);
        }
        #endregion

        private void depthOriWalk_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[] indexes = new int[n];
            int[,] inziMatrix = {   { 0,0,0,1,1,0,0},
                                    { 0,0,0,0,0,1,1},
                                    { 0,0,0,0,0,0,0},
                                    { 0,0,0,0,1,0,0},
                                    { 0,1,0,0,0,0,0},
                                    { 0,1,1,0,1,0,1},
                                    { 0,0,1,0,0,0,0}  };
            
            int[] walkArray = new int[n];
            for (int j = 0; j < n; j++)
            {
                walkArray[j] = j;
            }
            int point = 0;
            int block = -1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (isMinusArray(n, walkArray))
                    {
                        indexes[point] = i;
                        goto end;
                    }
                    if (inziMatrix[i,j]==1&&j!= block)
                    {
                        if (walkArray[j] == -1)
                        {
                            continue;
                        }
                        indexes[point] = i;
                        walkArray[i] = -1;
                        point++;
                        i = j - 1;
                        block = -1;
                        
                        break;
                    }
                    if(inziMatrix[i, j]!= 1&&(j+1)==n)
                    {
                        block = i;
                        i = indexes[point - 1] - 1;
                        point--;
                        indexes[point] = 0;
                        walkArray[block] = i;

                        break;
                    }
                }
            }
        end:;
            int[,] treeMatrix = new int[n, n];

            for (int i = 0; i < n-1; i++)
            {
                treeMatrix[indexes[i], indexes[i + 1]] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = treeMatrix[i, j];
                }
            }
            DrawFromMatrixNoColor();
        }
        public bool isMinusArray(int n,int[] array)
        {
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                if (array[i] == -1)
                {
                    counter++;
                }
            }
            if (counter == (n-1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void drawBeforeDepth_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            int[,] inziMatrix = {   { 0,0,0,1,1,0,0},
                                    { 0,0,0,0,0,1,1},
                                    { 0,0,0,0,0,0,0},
                                    { 0,0,0,0,1,0,0},
                                    { 0,1,0,0,0,0,0},
                                    { 0,1,1,0,1,0,1},
                                    { 0,0,1,0,0,0,0}  };
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = inziMatrix[i, j];
                }
            }
            DrawFromMatrixNoColor();
        }

        private void widthWalk_Click(object sender, EventArgs e)
        {

            int n = Convert.ToInt32(textBox1.Text);
            int[] indexes = new int[n];
            int[] indexesW = new int[n];
            int[,] inziMatrix = {   { 0,0,0,1,1,0,0},
                                    { 0,0,0,0,0,1,1},
                                    { 0,0,0,0,0,0,0},
                                    { 0,0,0,0,1,0,0},
                                    { 0,1,0,0,0,0,0},
                                    { 0,1,1,0,1,0,1},
                                    { 0,0,1,0,0,0,0}  };
            int[][] vertexArray = new int[2][];
            vertexArray[0] = new int[10];
            vertexArray[1] = new int[10];
            for (int i = 0; i < n; i++)
            {
                indexes[i] = 1;
                indexesW[i] = 1;
            }
            Array.Resize(ref vertexArray[0],1);
            Array.Resize(ref vertexArray[1],1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(indexesW[j]==1 && inziMatrix[i,j]==1)
                    {
                        vertexArray[0][vertexArray[0].Length - 1] = i;
                        vertexArray[1][vertexArray[1].Length - 1] = j;
                        indexes[i] -= 1;
                        indexesW[j] -= 1;
                        Array.Resize(ref vertexArray[0], vertexArray[0].Length+1);
                        Array.Resize(ref vertexArray[1], vertexArray[1].Length+1);
                        if (indexes[i] >= 0)
                        {
                            i-=1;
                        }
                        break;
                    }
                }
            }
            int[,] nInziMatrix = new int[n, n];
            for (int j = 0; j < vertexArray[0].Length-1; j++)
            {
                nInziMatrix[vertexArray[0][j], vertexArray[1][j]] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = nInziMatrix[i, j];
                }
            }
            DrawFromMatrixNoColor();

        }
    }

}
