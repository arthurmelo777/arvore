using AVLBinaryTree;

class Program {
    public static void Main(string[] args) {
        AVLTree at = new AVLTree(10);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(20);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(30);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(40);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(50);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(25);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(60);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(70);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(80);
        at.printTree();
        Console.WriteLine("----------------------");
        at.insertAVL(90);
        at.printTree();
        Console.WriteLine("----------------------");
    }
}