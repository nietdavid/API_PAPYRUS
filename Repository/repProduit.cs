using API_PAPYRUS.Models;
using Microsoft.Data.SqlClient;
using API_PAPYRUS.Data;

namespace API_PAPYRUS.Repository
{
    public class repProduit
    {
        // --- Constructeur ---
        // Le constructeur de repProduit ne fait rien de spécial ici,
        // car nous n'initialisons pas de connexion globale : 
        // chaque méthode ouvrira et fermera sa propre connexion SQL.
        public repProduit()
        {
        }
        // --- Méthode DELETE : Supprimer un produit par son code ---
        public void DeleteProduit(string codart)
        {
            // Ouverture d'une connexion SQL dans un bloc using pour garantir sa fermeture après usage.
            using (SqlConnection connection = new Connection().GetConnection())
            {
                // Création d'une commande SQL pour la suppression avec la connexion ouverte.
                using (SqlCommand requestDeleteProduit = connection.CreateCommand())
                {
                    // Définition de l'instruction SQL DELETE, qui supprimera le produit ayant le code passé en paramètre.
                    requestDeleteProduit.CommandText = "DELETE FROM PRODUIT WHERE CODART = @codart";

                    // Liaison du paramètre @codart à la valeur du code passée en argument.
                    requestDeleteProduit.Parameters.AddWithValue("@codart", codart);

                    // Exécution de la commande DELETE.
                    requestDeleteProduit.ExecuteNonQuery();
                }
            }
        }

        // --- Méthode UPDATE : Modifier un produit existant ---
        public void UpdateProduit(Produit produit)
        {
            // Ouverture d'une nouvelle connexion SQL dans un bloc using.
            using (SqlConnection connection = new Connection().GetConnection())
            {
                // Création d'une commande SQL pour mettre à jour le produit dans la base.
                using (SqlCommand requestUpdateProduit = connection.CreateCommand())
                {
                    // Définition de l'instruction SQL UPDATE.
                    // Les colonnes LIBART, STKALE, STKPHY, QTEANN et UNIMES sont mises à jour
                    // en fonction du produit identifié par son code (CODART).
                    requestUpdateProduit.CommandText = @"UPDATE PRODUIT SET 
                                                            LIBART = @libart,
                                                            STKALE = @stkale,
                                                            STKPHY = @stkphy,
                                                            QTEANN = @qteann,
                                                            UNIMES = @unimes
                                                         WHERE CODART = @codart";

                    // Liaison des paramètres pour mettre à jour chaque colonne avec les nouvelles valeurs.
                    requestUpdateProduit.Parameters.AddWithValue("@libart", produit.Libart); // Nouveau libellé.
                    requestUpdateProduit.Parameters.AddWithValue("@stkale", produit.Stkale); // Nouveau stock alloué.
                    requestUpdateProduit.Parameters.AddWithValue("@stkphy", produit.Stkphy); // Nouveau stock physique.
                    requestUpdateProduit.Parameters.AddWithValue("@qteann", produit.Qteann); // Nouvelle quantité annuelle.
                    requestUpdateProduit.Parameters.AddWithValue("@unimes", produit.Unimes); // Nouvelle unité de mesure.
                    requestUpdateProduit.Parameters.AddWithValue("@codart", produit.Codart); // Identifiant du produit à modifier.

                    // Exécution de la commande UPDATE pour appliquer les changements dans la base.
                    requestUpdateProduit.ExecuteNonQuery();
                }
            }
        }


        // --- Méthode READ : Récupérer tous les produits depuis la base ---
        public List<Produit> GetProduits()
        {
            // Création d'une liste de Produit qui va contenir tous les produits récupérés.
            List<Produit> listProduits = new List<Produit>();

            // Ouverture d'une connexion SQL dans un bloc using.
            using (SqlConnection connection = new Connection().GetConnection())
            {
                // Création d'une commande SQL pour sélectionner tous les produits.
                using (SqlCommand requestGetProduits = connection.CreateCommand())
                {
                    // Définition de l'instruction SQL SELECT pour récupérer toutes les colonnes de la table PRODUIT.
                    requestGetProduits.CommandText = "SELECT * FROM PRODUIT";

                    // Exécution de la commande et création d'un SqlDataReader pour lire les résultats.
                    // Le SqlDataReader permet de parcourir les lignes retournées par la requête.
                    using (SqlDataReader reader = requestGetProduits.ExecuteReader())
                    {
                        // Parcours de chaque enregistrement (chaque ligne) retourné par la requête.
                        while (reader.Read())
                        {
                            // Instanciation d'un nouvel objet Produit pour chaque ligne récupérée.
                            Produit oneProduit = new Produit
                            {
                                // Assignation de la valeur du champ "CODART" convertie en chaîne.
                                // On utilise l'opérateur null-coalescent "??" pour fournir une chaîne vide si la valeur est null.
                                Codart = reader["CODART"]?.ToString() ?? "",
                                // Même démarche pour le champ "LIBART".
                                Libart = reader["LIBART"]?.ToString() ?? "",
                                // Conversion du champ "STKALE" en entier de type short.
                                Stkale = Convert.ToInt16(reader["STKALE"]),
                                // Conversion du champ "STKPHY" en entier short.
                                Stkphy = Convert.ToInt16(reader["STKPHY"]),
                                // Conversion du champ "QTEANN" en entier short.
                                Qteann = Convert.ToInt16(reader["QTEANN"]),
                                // Conversion du champ "UNIMES" en chaîne, avec chaîne vide en cas de valeur null.
                                Unimes = reader["UNIMES"]?.ToString() ?? ""
                            };

                            // Ajout de l'objet Produit créé à la liste.
                            listProduits.Add(oneProduit);
                        }
                    }
                }
            }

            // Retourne la liste complète des produits récupérés depuis la base de données.
            return listProduits;
        }

        // --- Méthode CREATE : Ajouter un produit en base ---
        public void AddProduit(Produit produit)
        {
            // Le bloc "using" permet d'ouvrir une connexion SQL et garantit qu'elle sera
            // automatiquement fermée et libérée à la fin du bloc, même en cas d'exception.
            using (SqlConnection connection = new Connection().GetConnection())
            {
                // Dans ce bloc, nous créons une commande SQL liée à la connexion ouverte.
                // Le bloc using permet aussi de s'assurer que l'objet SqlCommand est correctement disposé.
                using (SqlCommand requestAddProduit = connection.CreateCommand())
                {
                    // La commande SQL est définie avec l'instruction INSERT.
                    // Les colonnes de la table PRODUIT sont renseignées avec des paramètres.
                    requestAddProduit.CommandText = @"INSERT INTO PRODUIT (CODART, LIBART, STKALE, STKPHY, QTEANN, UNIMES)
                                                      VALUES (@codart, @libart, @stkale, @stkphy, @qteann, @unimes)";

                    // Liaison des paramètres : chaque appel à Parameters.AddWithValue
                    // associe un paramètre de la commande (ex : @codart) à la valeur correspondante
                    // de l'objet produit passé en paramètre.
                    requestAddProduit.Parameters.AddWithValue("@codart", produit.Codart);   // Code de l'article.
                    requestAddProduit.Parameters.AddWithValue("@libart", produit.Libart);   // Libellé de l'article.
                    requestAddProduit.Parameters.AddWithValue("@stkale", produit.Stkale);   // Stock alloué.
                    requestAddProduit.Parameters.AddWithValue("@stkphy", produit.Stkphy);   // Stock physique.
                    requestAddProduit.Parameters.AddWithValue("@qteann", produit.Qteann);   // Quantité annuelle.
                    requestAddProduit.Parameters.AddWithValue("@unimes", produit.Unimes);   // Unité de mesure.

                    // Exécute la commande SQL d'insertion.
                    // ExecuteNonQuery() est utilisé pour les commandes qui ne renvoient pas de résultats (INSERT, UPDATE, DELETE).
                    requestAddProduit.ExecuteNonQuery();
                }
            }
        }

    }
}
