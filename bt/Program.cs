﻿using System;

class Program {
    public static void Main (string[] args) {
        Tree t = new Tree(50);

        //GetRaiz
        Console.WriteLine(t.Root.Value);

        //Adiciona filhos esquerdo e direito
        t.insert(25);

        t.insert(75);

        //Adiciona filhos na extrema esquerda
        t.insert(20);

        t.insert(10);

        t.insert(5);

        //Printa arvore
        t.printTree();

        //Verifica altura e profundidade
          //Raiz, D = 0; H = 4
          Console.WriteLine("Profundidade = " + t.depth(t.Root));
          Console.WriteLine("Altura = " + t.height(t.Root));

          // No do meio, D = 2; 2;
          Console.WriteLine("Profundidade = " + t.depth(t.search(20)));
          Console.WriteLine("Altura = " + t.height(t.search(20)));

          //Ultimo no, D = 4; H = 0;
          Console.WriteLine("Profundidade = " + t.depth(t.search(5)));
          Console.WriteLine("Altura = " + t.height(t.search(5)));

        //Testa metodo elements
        
    }
}