using System;

class Program {
    public static void Main (string[] args) {
        Node<int> n1 = new Node<int>(null, 2);
        n1.Parent = null;
        n1.LeftChild = null;
        n1.RightChild = null;
        Console.WriteLine(n1.Value);
    }
}