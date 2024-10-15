class Node : INode {
    private int value;
    private Node? parent, leftChild, rightChild;

    public int Value {
        get {return value;}
        set {this.value = value;}
    }

    public Node Parent {
        get {return parent;}
        set {parent = value;}
    }

    public Node LeftChild {
        get {return leftChild;}
        set {leftChild = value;}
    }

    public Node RightChild {
        get {return rightChild;}
        set {rightChild = value;}
    }

    public Node (Node p, int v) {
        value = v;
        parent = p;
        leftChild = null;
        rightChild = null;
    }
}

class Tree : ITree {
    private int size = 0;
    private Node root;

    public int Size {
        get {return size;}
    }

    public bool isRoot (Node n) {
        return root == n;
    }

    public bool isExternal (Node n) {
        return n.LeftChild == null && n.RightChild == null;
    }

    public bool isInternal (Node n) {
        return !isExternal(n);
    }

    //public int depth (Node<T> n) { }

    //public int height (Node<T> n) { }

    public bool search (int v) {
        return search(root, v);
    }

    public bool search (Node n, int v) {
        if (n == null) {return false;}
        if (v == n.Value) {return true;}
        if (v < n.Value) {return search(n.LeftChild, v);}
        if (v > n.Value) {return search(n.RightChild, v);}
        return false;
    }
    
    //public void insert (int v) { }
    //public int remove (int v) { }
    //public int replace (int v) { }
}