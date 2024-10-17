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

    public Node remove (int v) { 
        Node w = search(v);

        if (w.Value != v) throw new Exception("Valor nao encontrado");

        int type = caseType(w);

        switch (type) {
            case 0:
                removeRoot();
            break;
            case 1:
                removeLeaf(w);
            break;
            case 2:
                removeTwoChilds(w);
            break;
            case 3:
                removeOneChild(w);
            break;
        }

        return w;
     }

    private int caseType (Node n) {
        //Caso 0 - No raiz
        if (n == root && isExternal(n)) return 0;
        //Caso 1 - No folha
        if (isExternal(n)) return 1;
        //Caso 2 - No com dois filhos
        if (n.LeftChild != null && n.RightChild != null) return 2;
        //Caso 3 - No com um filho
        if (n.LeftChild != null || n.RightChild != null) return 3;
        return -1;
    }

    private void removeRoot () {
        root = null;
        size--;
    }

    private void removeLeaf (Node n) {
        Node w = n.Parent;
        //Verifica de que lado n é filho e seta como nulo
        if (n == w.LeftChild) w.LeftChild = null;
        else w.RightChild = null;
        size--;
    }

    private void removeOneChild (Node n) {
        Node w = n.Parent;
        Node z;

        //Verifica de que lado está o filho de n
        if (n.LeftChild != null) z = n.LeftChild;
        else z = n.RightChild;

        //Verifica de que lado n é filho e seta como z
        if (n == w.LeftChild) {
            w.LeftChild = z;
            z.Parent = w;
        }
        else {
            w.RightChild = z;
            z.Parent = w;
        }
        size--;
    }

    private void removeTwoChilds (Node n) {
        Node s = findSuccessor(n.RightChild);
        Node rcs = null;// RightChildOfSuccessor;

        //Seta, se houver, o filho direito do sucessor
        if (isInternal(s)) rcs = s.RightChild; 

        //No raiz
        if (n == root) {
            //Modifica os filhos do no removido
            n.LeftChild.Parent = s;
            n.RightChild.Parent = s;
            //Modifica o filho esquerdo do pai do sucessor
            s.Parent.LeftChild = rcs;
            //Modifica o filho direito do pai do sucessor
            if (rcs is not null) rcs.Parent = s.Parent;
            //Modifica o sucessor
            s.LeftChild = n.LeftChild;
            s.RightChild = n.RightChild;
        }
        else {
            //Modifica os filhos do no removido
            n.LeftChild.Parent = s;
            n.RightChild.Parent = s;
            //Modifica o filho esquerdo do pai do sucessor
            s.Parent.LeftChild = rcs;
            //Modifica o filho direito do pai do sucessor
            if (rcs is not null) rcs.Parent = s.Parent;
            //Modifica o sucessor
            s.LeftChild = n.LeftChild;
            s.RightChild = n.RightChild;
            s.Parent = n.Parent;
        }
        size--;
    }

    public Node findSuccessor (Node n) {
        if (n.LeftChild != null) return findSuccessor(n.LeftChild);
        return n;
    }

    public void printElements () {
        if (size == 0) throw new Exception("Arvore Vazia");
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
        if (size == 0) throw new Exception("Arvore Vazia");
        Node[] elements = setElements();
        
        int [,] matrix = new int[height(root)+1,size];
        setMatriz(matrix, elements);
        printMatriz(matrix, height(root)+1, size);
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
        for (int i = 0; i < height(root); i++) {
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