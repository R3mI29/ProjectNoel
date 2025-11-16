using System.Collections.Generic;

namespace ProjectNoel
{
  
        public class ListeChainee<T>
        {
            public Node<T>? Head;
            public ListeChainee()
            {
                Head = null;
            }
            
            /****---------- METHODES INTERNES à LA CLASSE ---------****/
            
            
            /* ADDFIRST : Ajout en début de liste : Complexité O(1) */
            public void AddFirst(T valeur)
            {    // Nouveau nœud dont Next pointe vers l'ancien head        
                Head = new Node<T>(valeur, Head);
            }

            /* ADDLAST : Ajout en fin de liste : Complexité O(n) */
            public void AddLast(T valeur)
            {
                // Cas 1 : Liste vide
                if (Head == null)
                {
                    AddFirst(valeur);   // AddLast et AddFirst sont identiques dans ce cas
                    return;
                }

                // Cas 2 : Liste non vide - parcourir jusqu'au dernier nœud
                Node<T> pt = Head; // On crée toujours un pointeur pour parcourir la liste.
                while (pt.Next != null) // Le dernier noeud a son Next null.
                {
                    pt = pt.Next;   // Donc on avance tant qu'on n'est pas au dernier.
                }

                // Ajouter le nouveau nœud à la fin
                pt.Next = new Node<T>(valeur, null);
            }

            /* INSERT : Ajout à l'intérieur de la liste : Complexité O(index). Au pire O(n) */
            public void Insert(int index, T valeur)
            {
                // Validation : index négatif
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), " : INSERT LISTECHAINEE : L'index ne peut pas être négatif.");

                // Cas 1 : Insertion au début (index == 0)
                if (index == 0)
                {
                    AddFirst(valeur);   // Inserez en position 0 c'est AddFirst.
                    return;
                }

                // La liste est nulle, mais je n'insère pas en tête.
                if (Head == null) 
                    throw new ArgumentOutOfRangeException(nameof(index), " : INSERT LISTECHAINEE: L'index dépasse la taille de la liste.");

                // Cas 2 : Insertion ailleurs (index > 0)
                Node<T>? pt = Head; // On crée toujours un pointeur pour parcourir la liste.

                // Parcourir jusqu'au nœud AVANT la position d'insertion
                for (int i = 0; i < index - 1; i++) // On avance Index-2 fois, pour se retrouver sur le noeud d'indice "index-1"
                {
                    if (pt.Next == null)    // si pt.next = null, en avançant, je vais dépasser la fin de la liste. 
                        throw new ArgumentOutOfRangeException(nameof(index), " : INSERT LISTECHAINEE: L'index dépasse la taille de la liste.");

                    pt = pt.Next;   // On avance au prochain noeud.
                }

                // On est sur le noeud d'indice "index-1". On va intercaler un nouveau noeud.
                pt.Next = new Node<T>(valeur, pt.Next);
            }

            /* SHOWLIST : Affichage d'une liste. Si le type T n'a pas d'affichage connu, il faut décliner. Complexité : O(n) */
            public void ShowList()
            {
                if (Head == null)
                {
                    Console.WriteLine("Liste vide");    // Vous pouvez aussi simplement faire return; si vous ne voulez pas d'affichage parasite.
                    return;
                }

                Node<T> pt = Head;   // On crée toujours un pointeur pour parcourir la liste.
                while (pt != null)  // On avcance tant qu'on n'a pas atteint la fin de la liste.
                {
                    // *** ATTENTION ICI : pour afficher des objets plus complexes (Lettre, Lutin, etc. Il faut remplacer la ligne qui suit
                    Console.Write(pt.Valeur);   // On affiche la valeur (cela suppose que l'on sait afficher le type T. Sinon il faut définir son affichage)
                    if (pt.Next != null)    // tant qu'on n'est pas au bout à l'avant-dernier noeud, rajouter un flèche.
                        Console.Write(" -> ");  // Pour faire joli. A éliminer au besoin.

                    pt = pt.Next;   // Au prochain noeud
                }
                Console.WriteLine(); // Retour à la ligne final. A vous de voir si vous voulez le garder.
            }

            /* REMOVEFIRST : Suppression du 1er élément. Complexité O(1) */
            public void RemoveFirst()
            {
                if (Head == null)  // Si la liste est vide, on a bien sur une erreur.
                    throw new InvalidOperationException("REMOVEFIRST LISTECHAINEE : Impossible de supprimer : la liste est vide.");

                // Le deuxième nœud devient le premier
                Head = Head.Next;
            }

            /* REMOVELAST : Suppression du dernier élément d'une liste. Complexité O(n) */
            public void RemoveLast()
            {
                if (Head == null)   // Si la liste est vide, on a bien sur une erreur.
                    throw new InvalidOperationException("REMOVELAST LISTECHAINEE : Impossible de supprimer : la liste est vide.");

                // Cas spécial : s'il y a un seul élément, c'est comme RemoveFirst, mais on peut le voir de manière encore plus simple
                if (Head.Next == null)
                {
                    Head = null;
                    return;
                }

                // Parcourir jusqu'à l'avant-dernier nœud
                Node<T> pt = Head;    // On crée toujours un pointeur pour parcourir la liste.
                while (pt.Next.Next != null)    // On avance tant qu'on n'a pas atteint l'avant-dernier noeud.
                {
                    pt = pt.Next;
                }

                // Supprimer le dernier nœud
                pt.Next = null;
            }

            /* REMOVEAT : suppression d'un élément d'indice "index". Rappel : On compte à partir de 0. Complexité O(index). Au pire O(n) */
            public void RemoveAt(int index)
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index), "L'index ne peut pas être négatif.");

                if (Head == null)  // Erreur si la liste est vide.
                    throw new InvalidOperationException("REMOVEAT LISTECHAINEE : Impossible de supprimer : la liste est vide.");

                // Cas 1 : Suppression du premier élément
                if (index == 0)
                {
                    RemoveFirst();
                    return;
                }

                // La liste n'a qu'un élément, mais je ne supprime pas en tête.
                if (Head.Next == null)
                    throw new ArgumentOutOfRangeException(nameof(index), " : REMOVEAT LISTECHAINEE: L'index dépasse la taille de la liste.");

                // Cas 2 : Suppression ailleurs
                Node<T> pt = Head;  // On crée toujours un pointeur pour parcourir la liste.

                // Parcourir jusqu'au nœud AVANT celui à supprimer. Il faut mettre le ".Next" du noeud avant celui à supprimer à jour.
                for (int i = 0; i < index - 1; i++) // On avance Index-2 fois, pour se retrouver sur le noeud d'indice "index-1"
                {
                    if (pt.Next == null) // Si le prochain est null, c'est qu'on n'aura rien à supprimer donc erreur.
                        throw new ArgumentOutOfRangeException(nameof(index), " : REMOVEAT LISTECHAINEE: L'index dépasse la taille de la liste.");

                    pt = pt.Next;
                }

                // "Sauter" le nœud à supprimer
                pt.Next = pt.Next.Next;
            }

            /* CLEAR : on vide la liste. Complexité O(1). */
            public void Clear()
            {
                Head = null;
            }

            /* ISEMPTY : est-ce que la liste est vide ? */
            public bool IsEmpty()
            {
                return Head == null;
            }

            /* ISFULL : est-ce que la liste est pleine ? */
            public bool IsFull()
            {
                return false;   // Une liste chaînée n'est jamais supposée être pleine.
            }
        }
        public class Node<T>
        {
            public T Valeur { get; set; }
            public Node<T>? Next { get; set; }
            public Node(T valeur, Node<T>? next)
            { Valeur = valeur; Next = next; }
    }
}