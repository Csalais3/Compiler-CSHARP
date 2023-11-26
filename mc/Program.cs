// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using System.Collections.Generic;
using Compiler.CodeAnalysis;

namespace Compiler; 

// representation of 1 + 2 * 3
//
//     +        - Leaf Nodes are the tokens in the input file
//    / \       - Root is the binary operator
//   1   *      - Computes by traversing along the tree
//      / \ 
//     2   3


// representation of 1 + 2 + 3
//
//     +        
//    / \       
//   +   3      
//  / \ 
// 1   2

class Program {
    static void Main(string[] args) {
        bool showTree = false;

        while (true) {
            Console.Write("> ");
            var line = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) {
                return;
            }
            if (line == "#showTree") {
                showTree = !showTree;
                Console.WriteLine(showTree ? "Now showing parse trees." : "No longer showing parse trees");
                continue;
            } else if (line == "#cls") {
                Console.Clear();
                continue;
            }

            var syntaxTree = SyntaxTree.Parse(line);
            var colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            
            if (showTree) {
                PrettyPrint(syntaxTree.Root);
                Console.ForegroundColor = colour;
            }

            if (!syntaxTree.Diagnostics.Any()) {
                var eval = new Evaluator(syntaxTree.Root);
                var result = eval.Evaluate();
                Console.WriteLine(result);
            } else {
                
                 Console.ForegroundColor = ConsoleColor.DarkRed;
                foreach (var diagnostic in syntaxTree.Diagnostics) {
                    Console.WriteLine(diagnostic);
                }
                Console.ForegroundColor = colour;
            }
        }
    }

    static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true) {
        // Creates UnixTree

        var marker = isLast ? "└──" : " ├──";

        Console.Write(indent);
        Console.Write(node.Kind);
        Console.Write(node.Kind);

        if (node is SyntaxToken t && t.Value != null) {
            Console.Write(" ");
            Console.Write(t.Value);
        }

        Console.WriteLine();

        indent += isLast ? "    " : "|   ";

        var lastChild = node.GetChildren().LastOrDefault();

        foreach (var child in node.GetChildren()) {
            PrettyPrint(child, indent, child == lastChild);
        }
    }
}

