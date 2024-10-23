using System;

class Program {
    public static void Main (string[] args) {
        Tree t = new Tree(50);

        t.printTree();
        
        Console.WriteLine("------------------------------");

        //GetRaiz
        //Console.WriteLine(t.Root.Value);

        //Adiciona filhos esquerdo e direito
        t.insert(25);

        t.insert(75);
        
        t.printTree();
        
        Console.WriteLine("------------------------------");
        
        //t.remove(50);
    
        //t.printTree();
        
        //Console.WriteLine("------------------------------");
    
        //Adiciona filhos
        t.insert(20);

        t.insert(60);
        t.insert(30);
        t.insert(80);
        t.insert(70);
        t.insert(65);

        //Printa arvore
        t.printTree();

        Console.WriteLine("------------------------------");

        t.remove(75);

        //t.printElements();

        t.printTree();
        
        Console.WriteLine("------------------------------");
        
        t.remove(60);

        //t.printElements();

        t.printTree();
        
        Console.WriteLine("------------------------------");
        
        t.remove(65);

        //t.printElements();

        t.printTree();

        //Verifica altura e profundidade
          //Raiz, D = 0; H = 4
          //Console.WriteLine("Profundidade = " + t.depth(t.Root));
          //Console.WriteLine("Altura = " + t.height(t.Root));

          // No do meio, D = 2; 2;
          //Console.WriteLine("Profundidade = " + t.depth(t.search(20)));
          //Console.WriteLine("Altura = " + t.height(t.search(20)));

          //Ultimo no, D = 4; H = 0;
          //Console.WriteLine("Profundidade = " + t.depth(t.search(5)));
          //Console.WriteLine("Altura = " + t.height(t.search(5)));

        //Testa metodo elements
        
    }
}
