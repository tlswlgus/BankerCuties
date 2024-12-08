using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class Tree
{
    public Node RootNode { get; set; }

    public Tree()
    {
        RootNode = null;
    }

    public void Insert(int data)
    {
        Node node = new Node(data);
        if (RootNode == null)
        {
            RootNode = node;
            return;
        }

        Node current = RootNode;
        Node parent = null;
        while (true)
        {
            parent = current;
            if (node.Data < parent.Data)
            {
                current = current.LeftChild;
                if (current == null)
                {
                    parent.LeftChild = node;
                    return;
                }
            }
            else
            {
                current = current.RightChild;
                if (current == null)
                {
                    parent.RightChild = node;
                    return;
                }
            }
        }
    }

    public void BuildBalancedTree(int[] sortedList)
    {
        RootNode = BuildBalancedTreeRecursive(sortedList);
    }

    private Node BuildBalancedTreeRecursive(int[] sortedList)
    {
        if (sortedList.Length == 0)
        {
            return null;
        }

        int mid = sortedList.Length / 2;
        Node root = new Node(sortedList[mid]);
        root.LeftChild = BuildBalancedTreeRecursive(sortedList[..mid]);
        root.RightChild = BuildBalancedTreeRecursive(sortedList[(mid + 1)..]);
        return root;
    }
}

public class BankersAlgorithm
{
    private int[,] Allocation;
    private int[,] Maximum;
    private int[,] Need;
    private int[] Available;
    private int[] Work;
    private bool[] Finish;
    private List<int> SafeSequence;

    public BankersAlgorithm(int[,] allocation, int[,] maximum, int[] available)
    {
        Allocation = allocation;
        Maximum = maximum;
        Available = available;
        int processCount = allocation.GetLength(0);
        int resourceCount = allocation.GetLength(1);

        Need = new int[processCount, resourceCount];
        Work = new int[resourceCount];
        Finish = new bool[processCount];
        SafeSequence = new List<int>();

        CalculateNeed();
    }

    private void CalculateNeed()
    {
        for (int i = 0; i < Allocation.GetLength(0); i++)
        {
            for (int j = 0; j < Allocation.GetLength(1); j++)
            {
                Need[i, j] = Maximum[i, j] - Allocation[i, j];
            }
        }
    }

    public bool IsSafe()
    {
        Array.Copy(Available, Work, Available.Length);
        Array.Clear(Finish, 0, Finish.Length);
        SafeSequence.Clear();

        while (true)
        {
            bool foundProcess = false;

            for (int i = 0; i < Allocation.GetLength(0); i++)
            {
                if (!Finish[i] && CanAllocate(i))
                {
                    foundProcess = true;
                    SafeSequence.Add(i);
                    Finish[i] = true;

                    for (int j = 0; j < Work.Length; j++)
                    {
                        Work[j] += Allocation[i, j];
                    }
                }
            }

            if (!foundProcess)
            {
                break;
            }
        }

        foreach (bool finished in Finish)
        {
            if (!finished)
            {
                return false;
            }
        }

        return true;
    }

    private bool CanAllocate(int process)
    {
        for (int j = 0; j < Work.Length; j++)
        {
            if (Need[process, j] > Work[j])
            {
                return false;
            }
        }
        return true;
    }

    public void ShowStepByStepMatrix()
    {
        Console.WriteLine("Allocation Matrix:");
        PrintMatrix(Allocation);

        Console.WriteLine("Maximum Matrix:");
        PrintMatrix(Maximum);

        Console.WriteLine("Need Matrix:");
        PrintMatrix(Need);

        Console.WriteLine("Available Vector:");
        PrintVector(Available);

        Console.WriteLine("Work Vector:");
        PrintVector(Work);

        Console.WriteLine("Finish Vector:");
        PrintFinishVector();
    }

    private void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    private void PrintVector(int[] vector)
    {
        foreach (int value in vector)
        {
            Console.Write(value + " ");
        }
        Console.WriteLine();
    }

    private void PrintFinishVector()
    {
        foreach (bool value in Finish)
        {
            Console.Write((value ? "true" : "false") + " ");
        }
        Console.WriteLine();
    }

    public void PrintSafeSequence()
    {
        if (SafeSequence.Count > 0)
        {
            Console.WriteLine("Safe Sequence: " + string.Join(" -> ", SafeSequence));
        }
        else
        {
            Console.WriteLine("No Safe Sequence found.");
        }
    }
}
