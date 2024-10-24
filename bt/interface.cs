namespace IBinaryTree {
public interface INode {
    
}

public interface ITree {
    public abstract bool isRoot (INode n);
    public abstract bool isExternal (INode n);
    public abstract bool isInternal (INode n);
    public abstract int depth (INode n);
    public abstract int height (INode n);
    public abstract INode search (int v);
    public abstract INode insert (int v);
    public abstract INode remove (int v);
    public abstract void printElements ();
    public abstract void printTree ();
}
}