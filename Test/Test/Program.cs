using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // /*
            bool[,] infos = new bool[4, 20];
            affichage(infos);


            Random alea = new Random();
            string[,] grille = new string[36, 14];
            bool[] pionJoue = new bool[5];// on crée la grille qui représentera le jeu
            // infoHasard(infos);
            bool[,] pions = initPions();
            ActuGrille(grille, infos, pions);
            Actuaffichage(grille);
            partie(pions, infos, grille);
            


        }
        static public void partie(bool[,] pions, bool[,] infos, string[,] grille)
        {
            bool victoire = false;
            int placementx = 1;
            int placementy = 1;
            int cpt = 0;
            int[] pionDonne = new int[2];
            while(verifVictoire(placementx, placementy, infos) == false)
            {
                if (cpt % 2 == 0)
                {
                    pionDonne = adversaireNiv1(pions, infos, grille, out placementx, out placementy);
                    victoire = verifVictoire(placementx, placementy, infos);
                    cpt++;
                }
                else
                {
                    joueur(pions, infos, pionDonne, grille, out placementx, out placementy);
                    victoire = verifVictoire(placementx, placementy, infos);
                    cpt++;
                }
            }
            if (cpt % 2 == 1)
            {
                Console.WriteLine("L'adversaire a gagné");
            }
            else
            {
                Console.WriteLine("Vous avez gagné !");
            }

        }
        public static int[] adversaireNiv1(bool[,] pions, bool[,] infos, string[,] grille, out int placementx, out int placementy)
        {
            char[] carac = { 'A', 'B', 'C', 'D' };
            Random alea = new Random();
            bool[] pionJoue = new bool[5];
            
            Console.WriteLine("Quel pions voulez-vous donner à l'adversaire ? (Exemple : B2)");
            string coord = Console.ReadLine();
            char pos = coord[0];
            int posy = (int)Char.GetNumericValue(coord[1]);
            bool test = pions[convertCoord(pos), posy * 5 - 1];
            while(test == false)
            {
                Console.WriteLine("Ce pions a déjà été utilisé.");
                Console.WriteLine("Quel pions voulez-vous donner à l'adversaire ? (Exemple : B2)");
                coord = Console.ReadLine();
                pos = coord[0];
                posy = (int)Char.GetNumericValue(coord[1]);
                test = pions[convertCoord(pos), posy*5-1];
            }
            pionJoue = choixPions(pos, posy, pions);
            placementx = 0;
            placementy = 0;
            test = true;
            while (test == true)
            {
                placementx = alea.Next(0, 4);
                placementy = alea.Next(0, 4);
                test = infos[placementx, placementy*5 + 4];
            }
            for (int i = 0; i < 5; i++)
            {
                infos[placementx, placementy*5 + i] = pionJoue[i];
            }
            Console.Clear();
            ActuGrille(grille, infos, pions);
            Actuaffichage(grille);
            Console.WriteLine("L'adversaire a joué en {0}{1}.", carac[placementx], placementy + 1);
            int choixpionsx = 0;
            int choixpionsy = 0;
            test = false;
            while (test == false)
            {
                Random alea2 = new Random();
                choixpionsx = alea2.Next(0, 4);
                choixpionsy = alea2.Next(0, 4);
                test = pions[choixpionsx, choixpionsy*5 + 4];
            }
            int[] pionDonne = { choixpionsx, choixpionsy };
            return pionDonne;
        }
        public static void joueur(bool[,] pions, bool[,] infos, int[] pionDonne, string[,] grille, out int placementx, out int placementy)
        {
            char[] carac = { 'A', 'B', 'C', 'D' };
            int choixpionsx = pionDonne[0];
            int choixpionsy = pionDonne[1];
            Console.WriteLine("L'adversaire vous donne le pion {0}{1}.", carac[choixpionsx], choixpionsy + 1);
            Console.WriteLine("Où placez-vous le pions ? (Ex : A2)");
            string coord = Console.ReadLine();
            char placement = coord[0];
            placementy = (int)Char.GetNumericValue(coord[1]);
            bool[] pionJoue = new bool[5];
            pionJoue = choixPions(carac[choixpionsx], choixpionsy+1, pions);
            
            placementx = convertCoord(placement);
            bool test = infos[placementx, (placementy) * 5 - 1];
            while (test == true)
            {
                Console.WriteLine("Il y a déjà un pions à cet endroit !");
                Console.Write("Où placez-vous le pions ? (Ex : A2)");
                coord = Console.ReadLine();
                placement = coord[0];
                placementy = (int)Char.GetNumericValue(coord[1]);
                placementx = convertCoord(placement);
                test = infos[placementx, (placementy) * 5 -1];
            }
            placementy--;
            for (int i = 0; i < 5; i++)
            {
                infos[placementx, (placementy) * 5 + i] = pionJoue[i];
            }
            Console.Clear();
            ActuGrille(grille, infos, pions);
            Actuaffichage(grille);
        }


        public static bool verifVictoire(int pos, int posy, bool[,] infos)
        {
            bool[] arg = new bool[16];

            if (verifPions(pos, posy, infos, out arg))
            {
                for (int i = 0; i < 4; i++)
                {
                    if (arg[i] == arg[i + 4] && arg[i + 4] == arg[i + 8] &&  arg[i + 8] == arg[i + 12])
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public static bool verifPions(int posx, int posy, bool[,] infos, out  bool[] arg)
        {
            char[] carac = { 'A', 'B', 'C', 'D' };
            arg = new bool[16];
            int cpt = 0;
            int cpt2 = 0;
            for(int i =4; i<20; i = i + 5)
            {
                if (infos[posx, i] == true)
                {
                    cpt++;
                }
            }
            for(int i = 0; i<4; i++)
            {
                if (infos[i, posy*5 +4] == true)
                {
                    cpt2++;
                }
            }
            if (cpt == 4)
            {
                int indice = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (i % 5 != 4)
                    {
                        arg[indice] = infos[posx, i]; // possibilité de renvoyer les coordonnées
                        indice++;
                    }
                }
                return true;

            }
            if (cpt2 == 4)
            {
                int indice = 0;
                for (int i = 0; i<4; i++)
                    for (int j = 0; j<4; j++)
                    {
                        arg[indice] = infos[i, posy * 5 + j];
                        indice++;
                    }
                return true;
            }
            if(posy == posx)
            {
                int cpt3 = 4;
                int cpt4 = 0;
                for(int i = 0   ; i<4; i++)
                {
                    if (infos[i, cpt3] == true)
                    {
                        cpt4++;
                    }
                    cpt3 = cpt3 + 5;
                }
                int cpt5 = 4;
                int cpt6 = 0;
                for (int i = 3; i >=0; i--)
                {
                    if (infos[i, cpt5] == true)
                    {
                        cpt6++;
                    }
                    cpt5 = cpt5 + 5;
                }
                if(cpt4 ==4)
                {
                    int indice = 0;
                    int reste = 0;
                    for(int i =0; i<4; i++)
                    {
                        for (int j = reste; j < reste + 4; j++)
                        {

                            arg[indice] = infos[i, j]; // possibilité de renvoyer les coordonnées
                            indice++;
                        }
                        reste = reste + 5;
                    }
                    return true;
                }
                if (cpt6 == 4)
                {
                    int indice = 0;
                    int reste = 0;
                    for (int i = 3; i >= 0; i--)
                    {
                        for (int j = reste; j < reste + 4; j++)
                        {
                            arg[indice] = infos[i, j];
                            indice++;// possibilité de renvoyer les coordonnées
                        }
                        reste = reste + 5;
                    }
                    return true;
                }
            }
            return false;
        }
        public static int convertCoord(char pos)
        {
            int posx = 0;
            if (pos == 'A')
                posx = 0;
            if (pos == 'B')
                posx = 1;
            if (pos == 'C')
                posx = 2;
            if (pos == 'D')
                posx = 3;
            return posx;
        }
        public static bool[] choixPions(char pos, int posy, bool[,] pions)
        {
            bool[] pionJoue = new bool[5];
            int posx = convertCoord(pos);
            for (int i = 0; i < 5; i++)
                pionJoue[i] = pions[posx, (posy-1) * 5 + i];
            pions[posx, (posy) * 5 - 1] = false;
            return pionJoue;
        }
        

        

        public static void infoHasard(bool[,] info)
        {
            Random alea = new Random();
            int h;
            for(int i = 0; i<4; i++)
            {
                for(int j = 0; j<20; j++)
                {
                    h = alea.Next(0, 2);
                    if (h == 0)
                        info[i, j] = false;
                    else
                        info[i, j] = true;
                }
            }
        }
        public static void affichage(bool[,] grille)   // actualise l'affichage de la grille une fois celle-ci changée
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (j % 5 == 0)
                        Console.Write(" ");
                    Console.Write(grille[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static bool[,] initPions()
        {
            int k, l, m;
            k = 0;
            l = 0;
            m = 0;
            bool[,] pions = new bool[4, 20];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 20; j++)
                    pions[i, j] = true;
            for (int j = 0; j<4; j++)
                for (int i = 1; i < 21; i++)
                {
                    if (i % 5 == 1)
                        if (i % 10 == 1)
                            pions[j, i-1] = false;
                    if (i % 5 == 2)
                    {
                        if (k % 4 < 2)
                        {
                            pions[j, i - 1] = false;
                        }
                        k++;
                    }
                    if (i % 5 == 3)
                    {
                        if (l % 8 < 4)
                        {
                            pions[j, i - 1] = false;
                        }
                        l++;
                    }
                    if (i % 5 == 4)
                    {
                        if (m % 16 < 8)
                        {
                            pions[j, i - 1] = false;
                        }
                        m++;
                    }
                }
            return pions;
        }
        public static void ActuGrille(string[,] grille, bool[,] infos, bool[,] pions)                            //Cette fonction va refaire la grille entièrement à chaque fois qu'une pièce est rajoutée     
        {
            int cpt3;
            int cpt2 = 0;
            int cpt;
            string[] tabEphemere = new string[9];
            for (int k = 0; k < 4; k = k + 1)  // la variable k va représenter la ligne en terme de figure : en effet k va occuper les lignes 0 à 8 puis 10 à 19...
            {

                cpt = 0;
                cpt3 = 0;
                for (int j = 0; j < 14; j++) // j représente les colonnes
                {
                    for (int i = cpt2; i < cpt2 + 9; i++) // i représente les lignes de k à k + 9
                    {
                        if (j < 7)
                        {
                            if (j % 2 == 1)
                            {
                                grille[i, j] = " | ";
                            }
                            else
                            {
                                if (infos[k, cpt + 4] == false)
                                {
                                    grille[i, j] = "                    ";
                                }
                                else
                                {
                                    tabEphemere = figure(infos[k, cpt], infos[k, cpt + 1], infos[k, cpt + 2], infos[k, cpt + 3]);  //on crée un tableau qui prend pour info les caractéristiques d'une figure située à la jème colonne et à la (k+9) ème ligne
                                    grille[i, j] = tabEphemere[i % 9];
                                }
                            }
                        }
                        else
                        {
                            if (j % 2 == 0)
                            {
                                grille[i, j] = " | ";
                            }
                            else
                            {
                                if (pions[k, cpt3 + 4] == false)
                                {
                                    grille[i, j] = "                    ";
                                }
                                else
                                {
                                    tabEphemere = figure(pions[k, cpt3], pions[k, cpt3 + 1], pions[k, cpt3 + 2], pions[k, cpt3 + 3]);  //on crée un tableau qui prend pour info les caractéristiques d'une figure située à la jème colonne et à la (k+9) ème ligne
                                    grille[i, j] = tabEphemere[i % 9];
                                }
                            }
                        }
                    }

                    if (j < 7 && j % 2 == 0)
                        cpt = cpt + 5;
                    if (j >= 7 && j % 2 == 1)
                    {
                        cpt3 = cpt3 + 5;
                    }
                }
            
                cpt2 = cpt2 + 9;
            }

            // -----------------------------------------------------------------------------------------------


        }
        public static void Actuaffichage(string[,] grille)   // actualise l'affichage de la grille une fois celle-ci changée
        {
            char[] lettre = { 'A', 'B', 'C', 'D' };
            Console.WriteLine("                                               PLATEAU                                                                                    PIONS DISPONIBLES");
            Console.WriteLine("               1                       2                      3                       4                        1                       2                      3                       4");
            Console.WriteLine("     ---------------------------------------------------------------------------------------------     ---------------------------------------------------------------------------------------------");
            for (int i = 0; i < 36; i++)
            {
                if (i %9 == 4)
                    Console.Write(" {0}  | ", lettre[i/9]);
                for (int j = 0; j < 14; j++)
                {
                    
                    {
                        if (j == 0 && i%9 != 4)
                            Console.Write("    | ");
                        Console.Write(grille[i, j]);
                        if (j == 6)
                            Console.Write("    || ");
                        if (j == 13)
                            Console.Write(" | ");


                    }
                }
                Console.WriteLine();
                if (i % 9 == 8)
                {
                    Console.WriteLine("     -------------------------------------------------------------------------------------------     ---------------------------------------------------------------------------------------------"); // double retour à la ligne lorsque l'on change de figure)
                }
            }
        }
        static string[] figure(bool taille, bool interieur, bool forme, bool couleur)
        {
            string[] oui = new string[1];

            if (taille == true && interieur == true && forme ==true && couleur== true)
                return carreGrandPleinFonce();
            if (!taille && interieur && forme && couleur)
                return carrePetitPleinFonce();
            if (taille && !interieur && forme && couleur)
                return carreGrandVideFonce();
            if (taille && interieur && !forme && couleur)
                return cercleGrandPleinFonce();
            if (taille && interieur && forme && !couleur)
                return carreGrandPleinClair();
            if (!taille && !interieur && forme && couleur)
                return carrePetitVideFonce();
            if (!taille && interieur && !forme && couleur)
                return cerclePetitPleinFonce();
            if (!taille && interieur && forme && !couleur)
                return carrePetitPleinClair();
            if (taille && !interieur && forme && !couleur)
                return carreGrandVideClair();
            if (taille && !interieur && !forme && couleur)
                return cercleGrandVideFonce();
            if (taille && interieur && !forme && !couleur)
                return cercleGrandPleinClair();
            if (!taille && !interieur && !forme && couleur)
                return cerclePetitVideFonce();
            if (!taille && interieur && !forme && !couleur)
                return cerclePetitPleinClair();
            if (!taille && !interieur && forme && !couleur)
                return carrePetitVideClair();
            if (taille && !interieur && !forme && !couleur)
                return cercleGrandVideClair();
            if (!taille && !interieur && !forme && !couleur)
                return cerclePetitVideClair();
            return oui;
                                                                

        }

        static void initGrille()
        {
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine("+---+---+---+---+");
                for (int i = 0; i < 4; i++)
                {
                    Console.Write("|   ");
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine("+---+---+---+---+");
        }
        static string[] carreGrandPleinFonce()
        {
            string[] tab = new string[9];
            for (int i = 0; i < 9; i++)
            {
                tab[i] = "@@@@@@@@@@@@@@@@@@@@";
            }
            return tab;
        }
        static string[] carreGrandVideFonce()
        {
            string[] tab = new string[9];

            tab[0] = "@@@@@@@@@@@@@@@@@@@@";

            for (int i = 1; i < 8; i++)
                tab[i] = "@                  @";
            tab[8] = "@@@@@@@@@@@@@@@@@@@@";
            return tab;
        }
        static string[] carrePetitPleinFonce()
        {
            string[] tab = new string[9];
            tab[0] = "                    ";
            for (int i = 1; i < 8; i++)
                tab[i] = "   @@@@@@@@@@@@@@   ";

            tab[8] = "                    ";
            return tab;
        }
        static string[] carrePetitVideFonce()
        {
            string[] tab = new string[9];
            tab[0] = "                    ";
            tab[1] = "   @@@@@@@@@@@@@@   ";
            for (int i = 2; i < 7; i++)
                tab[i] = "   @            @   ";
            tab[7] = "   @@@@@@@@@@@@@@   ";
            tab[8] = "                    ";
            return tab;
        }
        static string[] cercleGrandPleinFonce()
        {
            string[] G_cercle_plein = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    G_cercle_plein[j] = "        @@@@        ";
                }
                if (j == 1 || j == 7)
                {
                    G_cercle_plein[j] = "      @@@@@@@@      ";
                }
                if (j == 2 || j == 6)
                {
                    G_cercle_plein[j] = "    @@@@@@@@@@@@    ";
                }
                if (j == 3 || j == 5)
                {
                    G_cercle_plein[j] = "  @@@@@@@@@@@@@@@@  ";
                }
                if (j == 4)
                {
                    G_cercle_plein[j] = "@@@@@@@@@@@@@@@@@@@@";
                }
            }
            return G_cercle_plein;
        }


        // petit cercle plein 
        static string[] cerclePetitPleinFonce()
        {
            string[] P_cercle_plein = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    P_cercle_plein[j] = "                    ";
                }
                if (j == 1 || j == 7)
                {
                    P_cercle_plein[j] = "         @@         ";
                }
                if (j == 2 || j == 6)
                {
                    P_cercle_plein[j] = "      @@@@@@@@      ";
                }
                if (j == 3 || j == 5)
                {
                    P_cercle_plein[j] = "    @@@@@@@@@@@@    ";
                }
                if (j == 4)
                {
                    P_cercle_plein[j] = "   @@@@@@@@@@@@@@   ";
                }
            }
            return P_cercle_plein;
        }

        // Grand cercle vide
        static string[] cercleGrandVideFonce()
        {
            string[] G_cercle_vide = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    G_cercle_vide[j] = "        @  @        ";
                }
                if (j == 1 || j == 7)
                {
                    G_cercle_vide[j] = "      @      @      ";
                }
                if (j == 2 || j == 6)
                {
                    G_cercle_vide[j] = "    @          @    ";
                }
                if (j == 3 || j == 5)
                {
                    G_cercle_vide[j] = "  @              @  ";
                }
                if (j == 4)
                {
                    G_cercle_vide[j] = "@                  @";
                }
            }
            return G_cercle_vide;
        }
        //petit cercle vide

        public static string[] cerclePetitVideFonce()
        {

            string[] P_cercle_vide = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    P_cercle_vide[j] = "                    ";
                }
                if (j == 1 || j == 7)
                {
                    P_cercle_vide[j] = "         @@         ";
                }
                if (j == 2 || j == 6)
                {
                    P_cercle_vide[j] = "      @      @      ";
                }
                if (j == 3 || j == 5)
                {
                    P_cercle_vide[j] = "    @          @    ";
                }
                if (j == 4)
                {
                    P_cercle_vide[j] = "   @            @   ";
                }
            }
            return P_cercle_vide;
        }
        //--------------------------------
        static string[] carreGrandPleinClair()
        {
            string[] tab = new string[9];
            for (int i = 0; i < 9; i++)
            {
                tab[i] = "oooooooooooooooooooo";
            }
            return tab;
        }
        static string[] carreGrandVideClair()
        {
            string[] tab = new string[9];

            tab[0] = "oooooooooooooooooooo";

            for (int i = 1; i < 8; i++)
                tab[i] = "o                  o";
            tab[8] = "oooooooooooooooooooo";
            return tab;
        }
        static string[] carrePetitPleinClair()
        {
            string[] tab = new string[9];
            tab[0] = "                    ";
            for (int i = 1; i < 8; i++)
                tab[i] = "   oooooooooooooo   ";

            tab[8] = "                    ";
            return tab;
        }
        static string[] carrePetitVideClair()
        {
            string[] tab = new string[9];
            tab[0] = "                    ";
            tab[1] = "   oooooooooooooo   ";
            for (int i = 2; i < 7; i++)
                tab[i] = "   o            o   ";
            tab[7] = "   oooooooooooooo   ";
            tab[8] = "                    ";
            return tab;
        }
        static string[] cercleGrandPleinClair()
        {
            string[] G_cercle_plein = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    G_cercle_plein[j] = "        oooo        ";
                }
                if (j == 1 || j == 7)
                {
                    G_cercle_plein[j] = "      oooooooo      ";
                }
                if (j == 2 || j == 6)
                {
                    G_cercle_plein[j] = "    oooooooooooo    ";
                }
                if (j == 3 || j == 5)
                {
                    G_cercle_plein[j] = "  oooooooooooooooo  ";
                }
                if (j == 4)
                {
                    G_cercle_plein[j] = "oooooooooooooooooooo";
                }
            }
            return G_cercle_plein;
        }


        // petit cercle plein 
        static string[] cerclePetitPleinClair()
        {
            string[] P_cercle_plein = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    P_cercle_plein[j] = "                    ";
                }
                if (j == 1 || j == 7)
                {
                    P_cercle_plein[j] = "         oo         ";
                }
                if (j == 2 || j == 6)
                {
                    P_cercle_plein[j] = "      oooooooo      ";
                }
                if (j == 3 || j == 5 )
                {
                    P_cercle_plein[j] = "    oooooooooooo    ";
                }
                if (j == 4)
                {
                    P_cercle_plein[j] = "   oooooooooooooo   ";
                }

            }
            return P_cercle_plein;
        }

        // Grand cercle vide
        static string[] cercleGrandVideClair()
        {
            string[] G_cercle_vide = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    G_cercle_vide[j] = "        o  o        ";
                }
                if (j == 1 || j == 7)
                {
                    G_cercle_vide[j] = "      o      o      ";
                }
                if (j == 2 || j == 6)
                {
                    G_cercle_vide[j] = "    o          o    ";
                }
                if (j == 3 || j == 5)
                {
                    G_cercle_vide[j] = "  o              o  ";
                }
                if (j == 4)
                {
                    G_cercle_vide[j] = "o                  o";
                }
            }
            return G_cercle_vide;
        }
        //petit cercle vide

        public static string[] cerclePetitVideClair()
        {

            string[] P_cercle_vide = new string[9];
            int j = 0;
            for (j = 0; j <= 8; j = j + 1)
            {
                if (j == 0 || j == 8)
                {
                    P_cercle_vide[j] = "                    ";
                }
                if (j == 1 || j == 7)
                {
                    P_cercle_vide[j] = "         oo         ";
                }
                if (j == 2 || j == 6)
                {
                    P_cercle_vide[j] = "      o      o      ";
                }
                if (j == 3 || j == 5)
                {
                    P_cercle_vide[j] = "    o          o    ";
                }
                if (j == 4)
                {
                    P_cercle_vide[j] = "   o            o   ";
                }
            }
            return P_cercle_vide;
        }
    }
}
