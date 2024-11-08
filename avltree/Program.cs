using AVLBinaryTree;

class Program {
    public static void Main(string[] args) {
        AVLTree at = new AVLTree(15);
        //at.insertAVL(20);
        at.insertAVL(10);
        //at.insertAVL(72);
        at.insertAVL(5);
        at.printTree();
    }
}