using AVLBinaryTree;

class Program {
    public static void Main(string[] args) {
        AVLTree at = new AVLTree(1);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(40);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(50);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(75);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(25);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(60);
        //at.printTree();
        //Console.WriteLine("----------------------");
        at.insertAVL(100);
        at.printTree();
        Console.WriteLine("----------------------");
        // REMOVER
        at.removeAVL(100);
        at.printTree();
        Console.WriteLine("----------------------");
        at.removeAVL(25);
        at.printTree();
        Console.WriteLine("----------------------");
        at.removeAVL(60);
        at.printTree();
        Console.WriteLine("----------------------");
    }
}