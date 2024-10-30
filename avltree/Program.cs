using BinaryTree;
using AVLBinaryTree;

class Program {
    public static void Main(string[] args) {
        Node bn = new Node (null, 5);
        AVLNode an = new AVLNode(null, 9);
        Console.WriteLine(bn);
        Console.WriteLine(an);

        AVLTree at = new AVLTree(15);
        at.printTree();
        at.insert(20);
        at.printTree();
        at.insert(10);
        at.printTree();
        at.insert(22);
        at.printTree();
    }
}