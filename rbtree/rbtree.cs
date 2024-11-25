using System.Collections;

namespace RedBlackBinaryTree {
    public class RBNode {
        private int value;
        private char color;
        private RBNode? parent, leftChild, rightChild;

        public int Value {
            get {return value;}
            set {this.value = value;}
        }

        public char Color {
            get {return color;}
            set {color = value;}
        }

        public RBNode Parent {
            get {return parent;}
            set {parent = value;}
        }

        public RBNode LeftChild {
            get {return leftChild;}
            set {leftChild = value;}
        }

        public RBNode RightChild {
            get {return rightChild;}
            set {rightChild = value;}
        }

        public RBNode (RBNode p, int v, char? c = 'R') {
            value = v;
            parent = p;
            leftChild = null;
            rightChild = null;
            color = c;
        }
        
        public override string ToString () {
            return $"Elemento = {value}, Cor = {color}";
        }
    }

    public class RBTree {
        private int size = 0;
        private RBNode root;
        private ArrayList elements = new ArrayList();

        // Metodos Arvore Binaria
        public int Size {
            get {return size;}
        }

        public RBNode Root {
            get {return root;}
        }

        public bool isRoot (RBNode n) {
            return root == n;
        }

        public bool isExternal (RBNode n) {
            return n.LeftChild == null && n.RightChild == null;
        }

        public bool isInternal (RBNode n) {
            return !isExternal(n);
        }

        public int depth (RBNode n) {
            if (isRoot(n)) return 0;
            else return 1 + depth(n.Parent);
        }

        public int height (RBNode n) {
            if (n is null || isExternal(n)) return 0;
            else {
                return 1 + Math.Max(height(n.LeftChild), height(n.RightChild));
            }
        }

        public RBNode search (int v) {
            return search(root, v);
        }

        private RBNode search (RBNode n, int v) {
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
        
        private RBNode insert (int v) { 
            // Arvore vazia
            if (root == null) {
                root = new RBNode(null, v);
                return root;
            }
            
            RBNode w = search(v);
            RBNode n = new RBNode(w, v);
            if (v < w.Value) { w.LeftChild = n; }
            if (v > w.Value) { w.RightChild = n; }        
            size++;

            return n;
        }
        
        private RBNode remove (int v) {
            RBNode w = removeNo(v);
            size--;
            return w;
        }

        public RBNode removeNo (int v) { 
            RBNode w = search(v);

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

        private int caseType (RBNode n) {
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
        }

        private void removeLeaf (RBNode n) {
            RBNode w = n.Parent;
            //Verifica de que lado n é filho e seta como nulo
            if (n == w.LeftChild) w.LeftChild = null;
            else w.RightChild = null;
        }

        private void removeOneChild (RBNode n) {
            RBNode w = n.Parent;
            RBNode z;

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
        }

        private void removeTwoChilds (RBNode n) {
            RBNode s = findSuccessor(n.RightChild);

            if (s is not null) {
                int t = s.Value;
                removeNo(t);
                n.Value = t;
            }
        }

        public RBNode findSuccessor (RBNode n) {
            if (n.LeftChild != null) return findSuccessor(n.LeftChild);
            return n;
        }

        // Metodos Rubro Negra
        public RBNode insertRB (int v) {
            RBNode n = insert(v);
            n.LeftChild = new RBNode(n, null, 'B');
            n.RightChild = new RBNode(n, null, 'B');

            RBNode u = getUncle(n);

            while (!isRoot(n)) {
                // Duplo rubro
                if (n.Color == 'R' && n.Parent.Color == 'R') {
                    insertBalance(n, u, n.Parent, n.Parent.Parent);
                }

                n = n.Parent;
            }

            root.Color = 'B';

            return n;
            /*// Verifica se no é raiz
            if (isRoot(n)) {
                n.Color = 'B';
            }

            // Inserção de nó com pai rubro
            if (n.Parent.Color == 'R') {
                // Tio rubro

            }*/
        }

        private void insertBalance (RBNode n, RBNode u, RBNode p, RBNode gp) {
            // Tio rubro
            if (u.Color = 'R') {
                p.Color = 'B';
                u.Color = 'B';
                gp.Color = 'R';
                if (!isRoot(gp) && (gp.Color == 'R' && gp.Parent.Color == 'R')) {
                    insertBalance(gp, getUncle(gp), gp.Parent, gp.Parent.Parent);
                }
            }
            else {
                balanceRotation(n, p, gp);
            }
        }

        private void balanceRotation (RBNode n, RBNode p, RBNode gp) {
            // Rotação 1 - Esquerda simples
            if ((gp.RightChild == p) && (p.RightChild == n)) {
                leftRotation(p, n);
            }
            // Rotação 2 - Direita simples
            if ((gp.LeftChild == p) && (p.LeftChild == n)) {
                rightRotation(p, n);
            }
            // Rotação 3 - Esquerda dupla
            if ((gp.LeftChild == p) && (p.RightChild == n)) {
                rightRotation(n, n.LeftChild);
                leftRotation(gp, p);
            }
            // Rotação 4 - Direita dupla
            if ((gp.RightChild == p) && (p.LeftChild == n)) {
                leftRotation(n, n.RightChild);
                rightRotation(gp, p);
            }
        }
        
        private void leftRotation (AVLNode p, AVLNode n) {
            AVLNode lc = null;
            AVLNode rc = null;

            if (n is not null) {
                lc = n.LeftChild;
                rc = n.RightChild;
            }

            // Pai do pai vira pai do no
            if (p == root) {
                root = n;
                n.Parent = null;
            }
            else {
                AVLNode avo = p.Parent;
                n.Parent = avo;
                if (avo.LeftChild == p) avo.LeftChild = n;
                else avo.RightChild = n;
            }

            // Filho esquerdo do no vira filho direito do pai
            if (lc is null) {
                p.RightChild = lc;
            }
            else {
                p.RightChild = lc;
                lc.Parent = p;
            }

            // Pai vira filho esquerdo do no
            n.LeftChild = p;
            p.Parent = n;

            // Atualiza fb
            p.Fb = p.Fb + 1 - Math.Min(n.Fb, 0);
            n.Fb = n.Fb + 1 + Math.Max(p.Fb, 0);
        }

        private void rightRotation (AVLNode p, AVLNode n) {
            AVLNode lc = n.LeftChild;
            AVLNode rc = n.RightChild;

            // Pai do pai vira pai do no
            if (p == root) {
                root = n;
                n.Parent = null;
            }
            else {
                AVLNode avo = p.Parent;
                n.Parent = avo;
                if (avo.LeftChild == p) avo.LeftChild = n;
                else avo.RightChild = n;
            }

            // Filho direito do no vira filho esquerdo do pai
            if (rc is null) {
                p.LeftChild = rc;
            }
            else {
                p.LeftChild = rc;
                rc.Parent = p;
            }

            // Pai vira filho direito do no
            n.RightChild = p;
            p.Parent = n;

            // Atualiza fb
            p.Fb = p.Fb - 1 - Math.Max(n.Fb, 0);
            n.Fb = n.Fb - 1 + Math.Min(p.Fb, 0);
        }

        private RBNode getUncle (RBNode n) {
            if (n.Parent.LeftChild == n) {
                return n.Parent.RightChild;
            }
            else {
                return n.Parent.LeftChild;
            }
        }

        // Metodos Impressão
        public void printElements () {
            if (size == 0) throw new Exception("Arvore Vazia");
            Console.Write("(");
            printElement(root);
            Console.WriteLine(")");
        }

        private void printElement (RBNode n) {
            if (n is null) return;
            if (isInternal(n)) { printElement(n.LeftChild); }
            Console.Write(" " + n.Value + " ");
            if (isInternal(n)) { printElement(n.RightChild); }
        }

        public void printTree () {
            if (size == 0) throw new Exception("Arvore Vazia");

            elements = new ArrayList();
            setElements(root);
            
            int [,] matrix = new int[height(root)+1,size];
            string [,] matrixColor = new string[height(root)+1, size];
            setMatriz(matrix, matrixColor, elements);
            printMatriz(matrix, matrixColor, height(root)+1, size);
        }

        private void printMatriz (int[,] m, string[,] mc , int x, int y) {
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (m[i,j] == 0) Console.Write("   ");
                    else Console.Write("" + m[i, j] + mc[i, j]);
                }
                Console.WriteLine();
            }
        }

        private void setMatriz (int[,] m, string[,] mc, ArrayList elements) {
            int i = 0;
            //Inicia matriz
            for (i = 0; i < height(root); i++) {
                for (int j = 0; j < size; j++) {
                    m[i, j] = 0;
                }
            }

            //Atribui elementos
            i = 0;
            foreach (object obj in elements) {
                m[depth((RBNode)obj), i] = ((RBNode)obj).Value;
                mc[depth((RBNode)obj), i] = miniColor(((RBNode)obj).Color);
                i++;
            }
        }

        private void setElements (RBNode n) {
            if (isInternal(n) && n.LeftChild is not null) setElements(n.LeftChild);
            elements.Add(n);
            if (isInternal(n) && n.RightChild is not null) setElements(n.RightChild);
        }

        private string miniColor (char c) {
            string color = "";
            if (c == 'R') color = "ᴿ";
            else if (c == 'B') color = "ᴮ";

            return color;
        }
        
        public RBTree (int v) {
            RBNode r = new RBNode(null, v);
            root = r;
            size++;
        }
    }
}