﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Node = AdventOfCode.Tree.Node<string>;

namespace day06
{
    class Day06
    {
        static Dictionary<string, Node> ReadInput()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\input.txt");
            StreamReader reader = File.OpenText(path);
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            Node GetNode(string s)
            {
                if (nodes.ContainsKey(s))
                    return nodes[s];
                Node n = new Node(s);
                nodes[s] = n;
                return n;
            }
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] v = line.Split(')').ToArray();
                Node p = GetNode(v[0]);
                Node c = GetNode(v[1]);
                p.children.Add(c);
                c.parent = p;
            }
            return nodes;
        }

        static int CountOrbits(Node n, int depth)
        {
            int sum = depth;
            foreach (Node c in n.children)
            {
                sum += CountOrbits(c, depth + 1);
            }
            return sum;
        }

        static void PartA()
        {
            Dictionary<string, Node> nodes = ReadInput();
            Node top = nodes.First().Value;
            while (top.parent != null)
                top = top.parent;
            int sum = CountOrbits(top, 0);
            Console.WriteLine("Part A: Result is {0}.", sum);
        }

        static void PartB()
        {
            Dictionary<string, Node> nodes = ReadInput();
            Dictionary<Node, int> steps = new Dictionary<Node, int>();
            Node start = nodes["YOU"];
            Node end = nodes["SAN"];
            Node n = start.parent;
            int a = 0;
            do
            {
                steps[n] = a;
                n = n.parent;
                a++;
            }
            while (n != null);
            a = 0;
            n = end.parent;
            while (!steps.ContainsKey(n))
            {
                n = n.parent;
                a++;
            }
            int sum = steps[n] + a;
            Console.WriteLine("Part B: Result is {0}.", sum);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("AoC 2019 - " + typeof(Day06).Namespace + ":");
            PartA();
            PartB();
        }
    }
}
