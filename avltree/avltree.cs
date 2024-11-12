using System.Collections;
using System;

namespace AVLBinaryTree {
    public class AVLNode {
        private int value, fb;
        private AVLNode? parent, leftChild, rightChild;

        public int Value {
            get {return value;}
            set {this.value = value;}
        }

        public AVLNode Parent {
            get {return parent;}
            set {parent = value;}
        }

        public AVLNode LeftChild {
            get {return leftChild;}
            set {leftChild = value;}
        }

        public AVLNode RightChild {
            get {return rightChild;}
            set {rightChild = value;}
        }

        public int Fb {
            get {return fb;}
            set {fb = value;}
        }

        public AVLNode (AVLNode p, int v) {
            value = v;
            parent = p;
            fb = 0;
            leftChild = null;
            rightChild = null;
        }
        
        public override string ToString () {
            return $"Elemento = {value}, FB = {fb}";
        }
    }

    public class AVLTree {
        private int size = 0;
        private AVLNode root;
        private ArrayList elements = new ArrayList();

        // Metodos Arvore Binaria
        public int Size {
            get {return size;}
        }

        public AVLNode Root {
            get {return root;}
        }

        public bool isRoot (AVLNode n) {
            return root == n;
        }

        public bool isExternal (AVLNode n) {
            return n.LeftChild == null && n.RightChild == null;
        }

        public bool isInternal (AVLNode n) {
            return !isExternal(n);
        }

        public int depth (AVLNode n) {
            if (isRoot(n)) return 0;
            else return 1 + depth(n.Parent);
        }

        public int height (AVLNode n) {
            if (n is null || isExternal(n)) return 0;
            else {
                return 1 + Math.Max(height(n.LeftChild), height(n.RightChild));
            }
        }

        public AVLNode search (int v) {
            return search(root, v);
        }

        private AVLNode search (AVLNode n, int v) {
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
        
        private AVLNode insert (int v) { 
            // Arvore vazia
            if (root == null) {
                root = new AVLNode(null, v);
                return root;
            }
            
            AVLNode w = search(v);
            if (w.Value == v) throw new Exception("Valor já existe!");
            AVLNode n = new AVLNode(w, v);
            if (v < w.Value) { w.LeftChild = n; }
            if (v > w.Value) { w.RightChild = n; }        
            size++;

            return n;
        }
        
        private AVLNode remove (int v) {
            AVLNode w = removeNo(v);
            size--;
            return w;
        }

        private AVLNode removeNo (int v) { 
            AVLNode w = search(v);

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

        private int caseType (AVLNode n) {
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

        private void removeLeaf (AVLNode n) {
            AVLNode w = n.Parent;
            //Verifica de que lado n é filho e seta como nulo
            if (n == w.LeftChild) w.LeftChild = null;
            else w.RightChild = null;
        }

        private void removeOneChild (AVLNode n) {
            AVLNode w = n.Parent;
            AVLNode z;

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

        private void removeTwoChilds (AVLNode n) {
            AVLNode s = findSuccessor(n.RightChild);

            if (s is not null) {
                int t = s.Value;
                removeNo(t);
                n.Value = t;
            }
        }

        public AVLNode findSuccessor (AVLNode n) {
            if (n.LeftChild != null) return findSuccessor(n.LeftChild);
            return n;
        }

        // Metodos AVL
        public AVLNode insertAVL (int v) {
            AVLNode n = insert(v);
            while (n.Parent is not null) {
                // Atualiza fator de balanceamento
                if (n.Parent.LeftChild == n) { n.Parent.Fb++; }
                else { n.Parent.Fb--; }

                // Verifica se desbalanceou
                if (Math.Abs(n.Parent.Fb) > 1) {
                    balance(n);
                    break;
                }
                
                n = n.Parent;
            }

            return n;
        }

        private void balance (AVLNode n) {
            bool opposite = (isPositive(n.Parent.Fb) && !isPositive(n.Fb) || (!isPositive(n.Parent.Fb) && isPositive(n.Fb)));
            bool positive = isPositive(n.Parent.Fb);

            // Rotação 1 - Esquerda simples
            if (!positive && !opposite) {
                leftRotation(n.Parent, n, n.LeftChild, n.RightChild);
            }
            // Rotação 2 - Direita simples
            if (positive && !opposite) {
                rightRotation(n.Parent, n, n.LeftChild, n.RightChild);
            }
            // NAO TA FUNCIONANDO CORRIJA
            // Rotação 3 - Esquerda dupla
            if (!positive && opposite) {
                rightRotation(n.Parent, n, n.LeftChild, n.RightChild);
                leftRotation(n.Parent, n, n.LeftChild, n.RightChild);
            }
            // Rotação 4 - Direita dupla
            if (positive && opposite) {
                leftRotation(n.Parent, n, n.LeftChild, n.RightChild);
                rightRotation(n.Parent, n, n.LeftChild, n.RightChild);
            }
        }

        private void leftRotation (AVLNode p, AVLNode n, AVLNode lc, AVLNode rc) {
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

        private void rightRotation (AVLNode p, AVLNode n, AVLNode lc, AVLNode rc) {
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

        private bool isPositive (int v) {
            if (v > 0) return true;
            else return false;
        }

        // Metodos Impressão
        public void printElements () {
            if (size == 0) throw new Exception("Arvore Vazia");
            Console.Write("(");
            printElement(root);
            Console.WriteLine(")");
        }

        private void printElement (AVLNode n) {
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
            string [,] matrixFB = new string[height(root)+1,size];
            setMatriz(matrix, matrixFB, elements);
            printMatriz(matrix, matrixFB, height(root)+1, size);
        }

        private void printMatriz (int[,] m, string[,] mfb, int x, int y) {
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    if (m[i,j] == 0) Console.Write("      ");
                    else Console.Write(""+m[i, j]+"⁽"+mfb[i, j]+"⁾");
                }
                Console.WriteLine();
            }
        }

        private void setMatriz (int[,] m, string[,] mfb, ArrayList elements) {
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
                m[depth((AVLNode)obj), i] = ((AVLNode)obj).Value;
                mfb[depth((AVLNode)obj), i] = miniFB(((AVLNode)obj).Fb);
                i++;
            }
        }

        private void setElements (AVLNode n) {
            if (isInternal(n) && n.LeftChild is not null) setElements(n.LeftChild);
            elements.Add(n);
            if (isInternal(n) && n.RightChild is not null) setElements(n.RightChild);
        }

        private string miniFB (int v) {
            string fb = "";
            if (v == 0) fb = "⁰";
            else if (v == 1) fb = "¹";
            else if (v == -1) fb = "⁻¹";
            else if (v == 2) fb = "²";
            else if (v == -2) fb = "⁻²";

            return fb;
        }

        // Construtor
        public AVLTree (int v) {
            AVLNode r = new AVLNode(null, v);
            root = r;
            size++;
        }
    }
}