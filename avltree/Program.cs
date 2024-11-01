using AVLBinaryTree;

class Program {
    public static void Main(string[] args) {
        AVLTree at = new AVLTree(15);
        at.printTree();
        at.insert(20);
    }
}