﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystAnalys_lr1
{
    class Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Edge
    {
        public int v1, v2;

        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    class DrawGraph
    {
        #region
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 14; 

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Green);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.Blue);
            darkGoldPen.Width = 2;
            fo = new Font("Times New Roman", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }
        #endregion
        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 7, y - 7);
            gr.DrawString(number, fo, br, point);
        }
        public void drawColorVertex(int x, int y, string number,int color)
        {
            Brush brush = Brushes.Green;
            switch (color)
            {
                case 0:
                    brush = Brushes.Green;
                    break;
                case 1:
                    brush = Brushes.Red;
                    break;
                case 2:
                    brush = Brushes.Blue;
                    break;
                case 3:
                    brush = Brushes.Brown;
                    break;
                case 4:
                    brush = Brushes.Green;
                    break;
                case 5:
                    brush = Brushes.Indigo;
                    break;
                default:
                    break;
            }
            Pen pen = new Pen(brush);
            gr.FillEllipse(brush, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(pen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 7, y - 7);
            gr.DrawString(number, fo, br, point);
        }


        public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }
        public void drawColorEdge(Vertex V1, Vertex V2, Edge E, int numberE, int color,int type)
        {
            Brush brush = Brushes.Green;
            switch (color)
            {
                case 0:
                    brush = Brushes.Green;
                    break;
                case 1:
                    brush = Brushes.Red;
                    break;
                case 2:
                    brush = Brushes.Blue;
                    break;
                case 3:
                    brush = Brushes.Green;
                    break;
                case 4:
                    brush = Brushes.Indigo;
                    break;
                case 5:
                    brush = Brushes.Indigo;
                    break;
                default:
                    break;
            }
            Pen darkGoldPens = new Pen(brush);
            if (E.v1 == E.v2)
            {
                if (type == 0)
                {
                    gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                    drawColorVertex(V1.x, V1.y, (E.v1 + 1).ToString(), color);
                }
                else {
                    gr.DrawArc(darkGoldPens, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                    drawVertex(V1.x, V1.y, (E.v1 + 1).ToString()); 
                }
            }
            else
            {
                if (type == 0)
                {
                    gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                    point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                    drawColorVertex(V1.x, V1.y, (E.v1 + 1).ToString(), color);
                    drawColorVertex(V2.x, V2.y, (E.v2 + 1).ToString(), color);
                }
                else
                {
                    gr.DrawLine(darkGoldPens, V1.x, V1.y, V2.x, V2.y);
                    point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                    drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                    drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
                }
            }
        }

        public void drawALLGraph(List<Vertex> V, List<Edge> E)
        {
            for (int i = 0; i < E.Count; i++)
            {
                if (E[i].v1 == E[i].v2)
                {
                    gr.DrawArc(darkGoldPen, (V[E[i].v1].x - 2 * R), (V[E[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                    point = new PointF(V[E[i].v1].x - (int)(2.75 * R), V[E[i].v1].y - (int)(2.75 * R));
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
                else
                {
                    gr.DrawLine(darkGoldPen, V[E[i].v1].x, V[E[i].v1].y, V[E[i].v2].x, V[E[i].v2].y);
                    point = new PointF((V[E[i].v1].x + V[E[i].v2].x) / 2, (V[E[i].v1].y + V[E[i].v2].y) / 2);
                    gr.DrawString(((char)('a' + i)).ToString(), fo, br, point);
                }
            }
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, E[i].v2] = 1;
                matrix[E[i].v2, E[i].v1] = 1;
            }
        }

        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, i] = 1;
                matrix[E[i].v2, i] = 1;
            }
        }

    }
}