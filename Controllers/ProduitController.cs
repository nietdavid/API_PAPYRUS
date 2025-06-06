﻿using System.Text.Json;
using API_PAPYRUS.Business;
using API_PAPYRUS.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_PAPYRUS.Controllers
{
    [ApiController]
    [Route("[controller]")] // Ce contrôleur répond aux routes commençant par /Produit
    public class ProduitController : ControllerBase
    {
        // Liste temporaire des produits (utilisée pour le GET)
        public List<Produit> produitList = new List<Produit>();

        // Cette méthode interne appelle la couche business (bsProduit) pour récupérer les produits
        public List<Produit> getListProduits()
        {
            bsProduit bsProduit = new bsProduit();
            produitList = bsProduit.ListerProduits();
            return produitList;
        }

        // GET /Produit => Renvoie tous les produits au format JSON
        [HttpGet]
        public JsonResult GetProduits()
        {
            getListProduits(); // Récupère la liste à jour
            return new JsonResult(produitList); // Retourne en JSON
        }

        // POST /produit — version classique (automatique)
        [HttpPost]
        public JsonResult PostProduit([FromBody] Produit produit)
        {
            try
            {
                if (produit == null)
                {
                    return new JsonResult(new { message = "L'élément Produit est invalide." });
                }

                // Appelle la couche métier pour ajouter le produit à la BDD
                bsProduit bs = new bsProduit();
                bs.AjouterProduit(produit);

                // Retourne l’objet ajouté pour vérification
                return new JsonResult(produit);
            }
            catch (Exception ex)
            {
                // Capture toute exception (ex: validations, erreurs SQL) et retourne le message d'erreur
                return new JsonResult(new { message = ex.Message });
            }
        }

        //DELETE /produit
        [HttpDelete]
        public IActionResult SupprimerProduit([FromBody] Produit produit)
        {
            bsProduit bs = new bsProduit();

            if (bs.SupprimerProduit(produit))
                return Ok(new { message = "Produit supprimé avec succès."});
            else
                return NotFound(new { message = "Produit non trouvé." });
        }

    }
}
