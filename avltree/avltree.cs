using BinaryTree;

namespace AVLBinaryTree {
    public class AVLNode : Node {
        private int fb;

        public int Fb {
            get {return fb;}
            set {fb = value;}
        }

        public AVLNode (AVLNode p, int v) : base(p, v) {
            this.fb = fb;
        }
    }

    public class AVLTree : Tree {
        public AVLTree (int v) : base (v) {}
    }
}