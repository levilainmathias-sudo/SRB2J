using MySql.Data.MySqlClient; 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Scrabble2Joueurs
{
    public class BddGestion
    {
        private string connectionString;

        public BddGestion(string chConnexion)
        {
            this.connectionString = chConnexion;
        }

        /// <summary>
        /// Enregistre la partie et les deux joueurs en BDD
        /// </summary>
        public void EnregistrerPartie(Joueur j1, Joueur j2)
        {
            string nomGagnant = "Égalité";
            if (j1.GetTotalPoints() > j2.GetTotalPoints())
            {
                nomGagnant = j1.GetNom();
            }
            else if (j2.GetTotalPoints() > j1.GetTotalPoints())
            {
                nomGagnant = j2.GetNom();
            }

            using (MySqlConnection conn = new MySqlConnection(this.connectionString))
            {
                conn.Open();

                // 1. Insertion dans 'parties' et récupération de l'ID généré
                string reqPartie = "INSERT INTO parties (nom_gagnant) VALUES (@gagnant); SELECT LAST_INSERT_ID();";
                MySqlCommand cmdPartie = new MySqlCommand(reqPartie, conn);
                cmdPartie.Parameters.AddWithValue("@gagnant", nomGagnant);

                long idPartie = Convert.ToInt64(cmdPartie.ExecuteScalar());

                // 2. Insertion des détails de chaque joueur
                List<Joueur> lesJoueurs = new List<Joueur> { j1, j2 };
                foreach (Joueur j in lesJoueurs)
                {
                    string reqJoueur = "INSERT INTO partie_joueurs (partie_id, nom_joueur, total_points, nb_mots, meilleur_mot) " +
                                       "VALUES (@idPartie, @nom, @points, @nbMots, @meilleurMot);";

                    MySqlCommand cmdJoueur = new MySqlCommand(reqJoueur, conn);
                    cmdJoueur.Parameters.AddWithValue("@idPartie", idPartie);
                    cmdJoueur.Parameters.AddWithValue("@nom", j.GetNom());
                    cmdJoueur.Parameters.AddWithValue("@points", j.GetTotalPoints());
                    cmdJoueur.Parameters.AddWithValue("@nbMots", j.GetNbMots());
                    cmdJoueur.Parameters.AddWithValue("@meilleurMot", j.MotMeilleur());

                    cmdJoueur.ExecuteNonQuery();
                }
            }
        }
    }
}