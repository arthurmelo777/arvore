using RedBlackBinaryTree;

class Program {
    public static void Main(string[] args) {
        RBTree at = new RBTree(1);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(40);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(50);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(75);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(25);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(60);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insert(100);
        at.printTree();
        Console.WriteLine("----------------------");
        // REMOVER
        /*at.removeAVL(100);
        at.printTree();
        Console.WriteLine("----------------------");
        at.removeAVL(25);
        at.printTree();
        Console.WriteLine("----------------------");
        at.removeAVL(60);
        at.printTree();
        Console.WriteLine("----------------------");*/
    }
}