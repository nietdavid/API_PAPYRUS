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
            repositoryProduit.AddProduit(produit); // Appelle AddProduit() du repository
        }
    }
}
