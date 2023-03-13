using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Xml.Linq;

namespace lb5
{
    internal static class Line
    {
        public static int[] DFS(int startVertex, int[,] a)
        {
            bool[] visited = new bool[a.GetLength(0)];
            int[] path = new int[a.GetLength(0)];
            Stack<int> queue = new Stack<int>();
            Stack<int> parent = new Stack<int>();
            path[startVertex] = 0;
            visited[startVertex] = true;
            queue.Push(startVertex);
            parent.Push(startVertex);
            int prevVertex = 0;
            while (queue.Count > 0)
            {
                int vertex = queue.Pop();
                prevVertex = parent.Pop();
                if (visited[vertex] == false)
                {
                    path[vertex] = prevVertex;
                    Console.WriteLine($"{prevVertex + 1} -> {vertex + 1}");
                }
                visited[vertex] = true;
                for (int i = a.GetLength(1) - 1; i > -1; i--)
                {
                    if (a[vertex, i] == 1 && visited[i] == false)
                    {
                        queue.Push(i);
                        parent.Push(vertex);
                    }
                }
            }
            path[startVertex] = -1;
            foreach (int i in path)
                Console.Write(i + 1 + " ");
            Path(path, startVertex);
            return path;
        }
        static void Path(int[] path, int startVertex)
        {
            List<List<int>> list = new List<List<int>>(new List<int>[path.Length]);
            int wait;
            list[startVertex] = new List<int>() { -1 };
            int prev = startVertex;
            int count = 1;
            while (count < path.Length)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    if (path[i] == prev)
                    {
                        list[i] = new List<int>();
                        list[i].Add(prev);
                        list[i].AddRange(list[prev]);
                        prev = i;
                        count++;
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write($"\"{i + 1}\" <- ");
                foreach (var item in list[i])
                {
                    if (item > 0)
                        Console.Write(item + 1 + " <- ");
                }
                Console.WriteLine();
            }
        }
        public static void Draw(List<List<int>> list, List<Figura> objects)
        {
            int[,] a = new int[list.Count, list.Count];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list[i].Count; j++)
                    a[i, list[i][j]] = 1;
            DrawCmej(a, objects);
            foreach (var obj in objects)
                obj.Draw();
        }
        public static List<Figura> CreateList(int[,] a)
        {
            List<Figura> figuras= new List<Figura>();
            for (int i = 0; i < a.GetLength(0); i++)
                figuras.Add(new Circle(-0.5f + (float)i / a.GetLength(0), -0.5f + (float)i / a.GetLength(0), new Vector3((float)i / 5, (float)i / 10, (float)i / 2)));
            return figuras;
        }
        public static List<Figura> CreateList(List<List<int>> list)
        {
            List<Figura> figuras = new List<Figura>();
            int[,] a = new int[list.Count, list.Count];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < list[i].Count; j++)
                    a[i, list[i][j]] = 1;
            for (int i = 0; i < a.GetLength(0); i++)
                figuras.Add(new Circle(-0.5f + (float)i / a.GetLength(0), -0.5f + (float)i / a.GetLength(0), new Vector3((float)i / 5, (float)i / 10, (float)i / 2)));
            return figuras;
        }
        public static void Draw(int[,] a, List<Figura> objects)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (a[i,j] < 0)
                    {
                        a = ConvertToCmej(a);
                        break;
                    }
           DrawCmej(a, objects);
            foreach (var obj in objects)
                obj.Draw();
        }
        static void DrawCmej(int[,] a, List<Figura> objects)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == 1)
                    {
                        GL.LineWidth(10);
                        GL.Color3(Color.Black);
                        GL.Begin(PrimitiveType.Lines);
                        if (a[j, i] != 1)
                            GL.Color3(Color.Blue);
                        GL.Vertex2(objects[i].Position);
                        if (a[j, i] != 1)
                            GL.Color3(Color.Red);
                        GL.Vertex2(objects[j].Position);
                        GL.End();
                    }
                }
            }
        }
        static int[,] ConvertToCmej(int[,] a)
        {
            int count = 0;
            int[,] B = new int[a.GetLength(0), a.GetLength(0)];
            for (int j = 0; j < a.GetLength(1); j++)
                for (int i = 0; i < a.GetLength(0); i++)
                    if (a[i, j] == -1)
                        for (int k = 0; k < a.GetLength(0); k++)
                            if (a[k, j] == 1)
                                B[i, k] = 1;
            return B;
        }
    }
}
