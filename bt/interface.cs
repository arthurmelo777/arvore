interface INode {
    
}

interface ITree {
    public abstract bool isRoot (Node n);
    public abstract bool isExternal (Node n);
    public abstract bool isInternal (Node n);
    public abstract int depth (Node n);
    public abstract int height (Node n);
    public abstract bool search (int v);
    protected abstract bool search (Node n, int v);
    public abstract void insert (int v);
    public abstract int remove (int v);
    public abstract int replace (int v);
}