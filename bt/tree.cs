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

    public Node Root {
        get {return root;}
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

    public int depth (Node n) {
        if (isRoot(n)) return 0;
        else return 1 + depth(n.Parent);
    }

    public int height (Node n) {
        if (n is null || isExternal(n)) return 0;
        else {
            return 1 + Math.Max(height(n.LeftChild), height(n.RightChild));
        }
    }

    public Node search (int v) {
        return search(root, v);
    }

    private Node search (Node n, int v) {
        // Verifica se é externo
        if (isExternal(n)) { return n; }
        // Valor menor -> Caminha pra esquerda
        if (v < n.Value) {
            if (n.LeftChild != null) return search(n.LeftChild, v);
        }
        // Valor maior -> Caminha pra direita
        if (v > n.Value) {
            if (n.RightChild != null) return search(n.RightChild, v);
        }
        // Valor igual -> Retorna o nó
        return n;
    }
    
    public Node insert (int v) { 
        // Arvore vazia
        if (root == null) {
            root = new Node(null, v);
            return root;
        }
        
        Node w = search(v);
        Node n = new Node(w, v);
        if (v < w.Value) { w.LeftChild = n; }
        if (v > w.Value) { w.RightChild = n; }        
        size++;

        return n;
    }

    //public int remove (int v) { }

    public void printElements () {
        Console.Write("(");
        printElement(root);
        Console.WriteLine(")");
    }

    private void printElement (Node n) {
        if (n is null) return;
        if (isInternal(n)) { printElement(n.LeftChild); }
        Console.Write(" " + n.Value + " ");
        if (isInternal(n)) { printElement(n.RightChild); }
    }

    public void printTree () {
        Node[] elements = new Node[size];
        elements = setElements();
        
        int [,] matrix = new int[depth(elements[0])+1,size];
        setMatriz(matrix, elements);
        printMatriz(matrix, depth(elements[0])+1, size);
    }

    private void printMatriz (int[,] m, int x, int y) {
        for (int i = 0; i < x; i++) {
            for (int j = 0; j < y; j++) {
                if (m[i,j] == 0) Console.Write("    ");
                else Console.Write(" " + m[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    private void setMatriz (int[,] m, Node[] elements) {
        //Inicia matriz
        for (int i = 0; i < depth(elements[0]); i++) {
            for (int j = 0; j < size; j++) {
                m[i, j] = 0;
            }
        }

        //Atribui elementos
        for (int i = 0; i < size; i++) {
            m[depth(elements[i]), i] = elements[i].Value;
        }
    }

    private Node[] setElements () {
        Node[] elements = new Node[size];

        setElement(root, elements, 0);

        return elements;
    }

    private int setElement (Node n, Node[] elements, int i) {
        if (n is null) return i;
        
        i = setElement(n.LeftChild, elements, i);
        elements[i++] = n;
        i = setElement(n.RightChild, elements, i);

        return i;
    }
    
    public Tree (int v) {
        Node r = new Node(null, v);
        root = r;
        size++;
    }
}