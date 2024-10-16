interface INode {
    
}

interface ITree {
    public abstract bool isRoot (Node n);
    public abstract bool isExternal (Node n);
    public abstract bool isInternal (Node n);
    public abstract int depth (Node n);
    public abstract int height (Node n);
    public abstract Node search (int v);
    public abstract Node insert (int v);
    public abstract int replace (int v, int x);
    //public abstract int remove (int v);
    public abstract void printElements ();
    public abstract void printTree ();
}