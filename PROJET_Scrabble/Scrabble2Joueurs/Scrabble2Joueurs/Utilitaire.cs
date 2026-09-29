using System;

namespace Scrabble2Joueurs
{
    /// <summary>
    /// Classe qui regroupe les fonctions de calcul
    /// </summary>
    public static class Utilitaire
    {
        /// <summary>
        /// Méthode qui retourne le nombre de points que rapporte une lettre
        /// </summary>
        private static int PointsLettre(char l)
        {
            int p;
            switch (char.ToUpper(l))
            {
                case 'D':
                case 'G':
                case 'M':
                    p = 2;
                    break;
                case 'B':
                case 'C':
                case 'P':
                    p = 3;
                    break;
                case 'F':
                case 'H':
                case 'V':
                    p = 4;
                    break;
                case 'J':
                case 'Q':
                    p = 8;
                    break;
                case 'K':
                case 'W':
                case 'X':
                case 'Y':
                case 'Z':
                    p = 10;
                    break;
                default:
                    p = 1;
                    break;
            }
            return p;
        }

        /// <summary>
        /// Méthode qui retourne le nombre de points que rapporte un mot
        /// </summary>
        public static int PointsMot(string mot)
        {
            if (string.IsNullOrEmpty(mot)) return 0;

            mot = mot.ToUpper();
            int pts = 0;
            for (int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                pts = pts + PointsLettre(lettre);
            }
            return pts;
        }
    }
}