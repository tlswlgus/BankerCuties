public class Node
{
    public int Data { get; set; }
    public Node RightChild { get; set; }
    public Node LeftChild { get; set; }

    public Node(int data)
    {
        Data = data;
        RightChild = null;
        LeftChild = null;
    }
}
