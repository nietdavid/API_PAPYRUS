using API_PAPYRUS.Models;
using API_PAPYRUS.Repository;

namespace API_PAPYRUS.Business
{
    // La classe bsProduit fait le lien entre le contrôleur (Controller) et le repository (accès base de données).
    // Elle contient la logique métier liée aux produits.
    public class bsProduit
    {
        // Déclaration d'une variable privée qui représente le repository des produits.
        private repProduit repositoryProduit;

        // Constructeur de la classe bsProduit.
        // Lorsqu'on crée une instance de bsProduit, on initialise le repositoryProduit.
        public bsProduit()
        {
            repositoryProduit = new repProduit();
        }

        // Méthode qui retourne la liste de tous les produits en appelant le repository.
        public List<Produit> ListerProduits()
        {
            return repositoryProduit.GetProduits(); // Appelle la méthode GetProduits() du repository
        }

        // Méthode pour ajouter un nouveau produit en base de données.
        // Elle reçoit un objet Produit et le transmet au repository pour insertion.
        public void AjouterProduit(Produit produit)
        {
            // --- VÉRIFICATIONS DES LONGUEURS ---
            if (produit.Codart.Length > 4)
                throw new Exception("Le code article (CODART) ne doit pas dépasser 4 caractères.");

            if (produit.Libart.Length > 25)
                throw new Exception("Le libellé de l'article (LIBART) ne doit pas dépasser 25 caractères.");

            if (produit.Unimes.Length > 5)
                throw new Exception("L'unité de mesure (UNIMES) ne doit pas dépasser 5 caractères.");

            // --- SI TOUT EST OK, INSERTION EN BASE ---
            repositoryProduit.AddProduit(produit);
        }

        //Méthode pour supprimer un produit existant à partir de CODART, elle reçoit l'objet produit et le transmet au repository pour suppression
        public bool SupprimerProduit(Produit produit)
        {
            return repositoryProduit.SupprimerProduit(produit);
        }
    }
}
