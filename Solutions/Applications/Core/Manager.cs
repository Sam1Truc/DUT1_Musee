// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Manager - source file
// Author : Samuel Bussy & Valentin Dumont
// Creation date: 13/05/2016
//
// ======================================================================== 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Manager
    {
        /// <summary>
        /// Propriété Musee qui permet d'avoir toute la partie gestion du musee
        /// </summary>
        public Organisation Organisme
        {
            get;
            private set;
        }

        /// <summary>
        /// Encapsulation de la liste d'utilisateurs.
        /// </summary>
        public ObservableCollection<IUtilisateur> ListeUtilisateurs
        {
            get
            {
                return new ObservableCollection<IUtilisateur>(listeUtilisateurs);
            }
        }
        private ObservableCollection<Utilisateur> listeUtilisateurs = new ObservableCollection<Utilisateur>();

        /// <summary>
        /// Propriété donnant l'accès aux collections et éléments du musée.
        /// </summary>
        public IPieceDeMusee Racine
        {
            get
            {
                return racine;
            }
        }
        private PieceDeMusee racine = new Collection("Base", "Racine");

        /// <summary>
        /// Propriété encapsulant l'utilisateur courrant.
        /// </summary>
        public IUtilisateur UtilisateurCourant
        {
            get { return utilisateurCourant; }
        }
        private IUtilisateur utilisateurCourant;

        private IDataManager iDataManager;


        /***********************************************************************************/
        /*********************************Méthodes******************************************/
        /***********************************************************************************/

        /// <summary>
        /// Constructeur du Mananger.
        /// </summary>
        public Manager(IDataManager iManager)
        {
            iDataManager = iManager;
            ChargerComposite();
            ChargerOrganisme();
            ChargerUtilisateurs();
        }

        /// <summary>
        /// Méthode permettant de créer/modifier une organisation.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <see cref="Organisation.Organisation(string, string, string, string, string)"/>
        public void CreerModifOrganisation(string nom, string adresse, string tel, string mail, string desc)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur)
            {
                if (nom != null && adresse != null && tel != null && mail != null)
                {
                    Organisme.ModifOrg(nom, adresse, tel, mail, desc);
                }
            }
        }

        /// <summary>
        /// Méthode permettant de créer une collection.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <see cref="Collection.Collection(string, string, IPieceDeMusee[])"/>
        public void AjouterCollection(string zone, string nom, params IPieceDeMusee[] tab)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur)
            {
                if (zone != null && nom != null)
                {
                    racine.Ajouter(new Collection(zone, nom, tab));
                    foreach(PieceDeMusee pdm in tab)
                    {
                        racine.ListePDM.Remove(pdm);
                    }
                }
            }
        }

        /// <summary>
        /// Méthode qui par le biais de LINQ nous revoit un élément ou une collection grâce à son nom.
        /// </summary>
        /// <param name="nom">Nom de l'élément/collection à rechercher.</param>
        /// <returns>
        /// PieceDeMusee qui sera donc soit un élément soit une collection.</returns>
        private PieceDeMusee ChercherEltParNom(string nom, PieceDeMusee utile)
        {
            var pdm = ((racine.ListePDM.Select(p => p)).Concat(racine.ListePDM.SelectMany(p => p.ListePDM))).SingleOrDefault(l => l.Nom == nom && l.GetType().Equals(utile.GetType()));
            return (PieceDeMusee)pdm;
        }

        /// <summary>
        /// Méthode qui permet de renvoyer une liste d'éléments dont leur nom contient une chaîne de caractères spécifiée.
        /// </summary>
        /// <param name="pdm">Collection dans laquelle la recherche va s'effectuer.</param>
        /// <param name="caractères">Chaîne de caractères qui sera le filtre de la recherche.</param>
        /// <returns>
        /// Une liste d'éléments qui contiennent le(s) caractères passés en paramètre.</returns>
        public IEnumerable<IPieceDeMusee> RechercheRapide(string caractères)
        {
            if(string.IsNullOrWhiteSpace(caractères))
            {
                return (Racine as Collection).RechercheParCaracteres(caractères);
            }
            return null;
        }

        /// <summary>
        /// Méthode permettant de créer un élément dans la collection principale.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <see cref="Element.Element(string, string, string, string, string)"/>
        public void AjouterElement(string zone, string nom, string caracteristiques, string description, string auteur = null, params Media[] medias)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && racine != null && zone != null && nom != null && caracteristiques != null && description != null)
            {
                if (auteur != null)
                {
                    racine.Ajouter(new Element(zone, nom, caracteristiques, description, auteur, medias));
                }
                else
                {
                    racine.Ajouter(new Element(zone, nom, caracteristiques, description, medias));
                }
            }
        }

        /// <summary>
        /// Méthode qui permet l'ajout direct d'un média d'un élément.
        /// </summary>
        /// <param name="pdm">Pièce de musée qui va se voir ajouter le média.</param>
        /// <param name="media">Chemin du média à ajouter.</param>
        public void AjouterMédia(IPieceDeMusee pdm, String chemin)
        {
            (ChercherEltParNom(pdm.Nom, pdm as Element) as Element).AjouterMedia(new Media(chemin));
        }

        /// <summary>
        /// Méthode permettant d'ajouter un élément à une collection.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <param name="nomCollection">Nom de la collection recherchée.</param>
        /// <param name="pdm">Elément à ajouter sous la collection.</param>
        public void AjouterAUneSousCollection(string nomCollection, IPieceDeMusee pdm)
        {
            var c = ChercherEltParNom(nomCollection, new Collection("",""));
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && pdm != null && (c != null && c is Collection))
            {
                c.Ajouter(pdm as PieceDeMusee);
                racine.Supprimer(pdm as PieceDeMusee);
            }
        }

        /// <summary>
        /// Méthode permettant de modifier le contenu d'une collection.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <param name="coll"">Collection qui va subir les modifications.</param>
        /// <param name="zone">Nouvelle zone de la collection.</param>
        /// <param name="nouvNomCollection">Nouveau nom de la collection.</param>
        /// <param name="lPdmASuppr">Liste de pièces de musée à supprimé de la collection.</param>
        /// <param name="lPpdmAAjouté">Liste de pièces de musée à ajouté à la collection.</param>
        public void ModifierCollection(IPieceDeMusee coll, string zone, string nouvNomCollection, List<IPieceDeMusee> lPdmASuppr, List<IPieceDeMusee> lPpdmAAjouté)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && (coll != null && coll is Collection)&& zone != null && nouvNomCollection != null)
            {
                (coll as Collection).Modifier(zone, nouvNomCollection, lPdmASuppr.ToArray());
            }
            foreach(IPieceDeMusee ipdm in lPpdmAAjouté)
            {
                AjouterAUneSousCollection(coll.Nom, ipdm);
            }
        }

        /// <summary>
        /// Méthode permettant de modifier le contenu d'un élément.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <param name="nomElt">Nom de l'élément recherché.</param>
        /// <see cref="Element.Element(string, string, string, string, string)"/>
        public void ModifierElement(IPieceDeMusee elt, string zone, string nom, string caracteristiques, string description, string auteur = null, List<Media> lMedia = null)
        {
            var e = ChercherEltParNom(elt.Nom, elt as Element);
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && e is Element)
            {
                if (auteur != null)
                {
                    e.Modifier(zone, nom, caracteristiques, description, lMedia, auteur);
                }
                else
                {
                    e.Modifier(zone, nom, caracteristiques, description, lMedia);
                }
            }
        }

        /// <summary>
        /// Méthode permettant de supprimer une collection.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <param name="coll">Collection qui va être supprimé.</param>
        public void SupprimerCollection(IPieceDeMusee coll)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && coll is Collection)
            {
                racine.Supprimer(coll as PieceDeMusee);
            }
        }

        /// <summary>
        /// Méthode permettant de supprimer un élément.
        /// ATTENTION : L'utilisateur courrant doit être administrateur pour pouvoir le faire.
        /// </summary>
        /// <param name="nomElt">Nom de l'élément à supprimer.</param>
        public void SupprimerElement(IPieceDeMusee e)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && e is Element)
            {
                racine.Supprimer(e as PieceDeMusee);
            }
        }

        public void SupprimerMedias(IPieceDeMusee e, List<Media> lMedias)
        {
            if (utilisateurCourant != null && utilisateurCourant is Administrateur && e is Element)
            {
                (ChercherEltParNom(e.Nom, e as Element) as Element).SupprimerMédias(lMedias);
            }
        }


        /// <summary>
        /// Méthode ajoutant un nouvel utilisateur à la liste des utilisateurs déjà inscrits. 
        /// </summary>
        /// <param name="util">Utilisateur qui va être ajouté à la liste des utilisateurs.</param>
        public void InscrireUtilisateur(IUtilisateur util)
        {
            if (!listeUtilisateurs.Contains(util))
            {
                if (util is Administrateur)
                {
                    listeUtilisateurs.Add(util as Administrateur);
                    return;
                }
                listeUtilisateurs.Add(util as Utilisateur);
            }
        }

        /// <summary>
        /// Méthode supprimant un utilisateur dans la liste des utilisateurs existants.
        /// </summary>
        /// <param name="util">Utilisateur à supprimer.</param>
        public void SupprimerUtilisateur(IUtilisateur util)
        {
            if (listeUtilisateurs.Contains(util))
            {
                if (util is Administrateur)
                {
                    listeUtilisateurs.Remove(util as Administrateur);
                    return;
                }
                listeUtilisateurs.Remove(util as Utilisateur);
            }
        }

        /// <summary>
        /// Méthode qui vérifie que la personne qui tente de se connecter existe bien dans la liste des
        /// personnes déjà enregistrées.
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur que le système vérifiera.</param>
        /// <param name="mdp">Mot de passe relié à l'identifiant de l'utilisateur qui sera vérifier.</param>
        /// <returns>true : L'utilisateur a réussi à se connecter.
        /// false : L'utilisateur n'a pas été reconnu ou il y a déjà un utilisateur connecté.</returns>
        public bool Connexion(string id, string mdp)
        {
            Utilisateur util = listeUtilisateurs.SingleOrDefault(u => u.Identifiant == id);

            if (util != null && utilisateurCourant == null && util.MotDePasse == mdp)
            {
                utilisateurCourant = util;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Méthode qui permet de savoir si l'utilisateur qui est connecté est Administrateur ou non.
        /// </summary>
        /// <returns>true : L'utilisateur a les droits d'administrateur.
        /// false : L'utilisateur n'a pas les droits d'administrateur.</returns>
        public bool ConnexionAdmin()
        {
            if(utilisateurCourant != null && utilisateurCourant is Administrateur) { return true; }
            return false;
        }

        /// <summary>
        /// Méthode qui permet à l'utilisateur courant de se déconnecter.
        /// </summary>
        /// <returns>true : La déconnexion s'est bien passée.
        /// false : la déconnexion </returns>
        public void Deconnexion()
        {
            if (utilisateurCourant != null)
            {
                utilisateurCourant = null;
            }
        }

        /// <summary>
        /// Permet de charger la liste des utilisateurs par le biais d'un autre assemblage.
        /// </summary>
        /// <see cref="IDataManager.ChargerUtilisateurs"/>
        public void ChargerUtilisateurs()
        {
            foreach (IUtilisateur iutils in iDataManager.ChargerUtilisateurs())
            {
                if (!listeUtilisateurs.Contains(iutils))
                {
                    listeUtilisateurs.Add(iutils as Utilisateur);
                }
            }
        }

        /// <summary>
        /// Sauvegarde la liste des utilisateurs dans un fichier.
        /// </summary>
        /// <param name="listeU">Liste des utilisateurs qui ont été enregistré pendant le programme.</param>
        /// <see cref="IDataManager.SauvegardeUtilisateurs(IEnumerable{IUtilisateur})"/>
        public void SauvegardeUtilisateurs(IEnumerable<IUtilisateur> listeU)
        {
            iDataManager.SauvegardeUtilisateurs(listeU);
        }

        /// <summary>
        /// Charge l'ensemble des données comprenant les collections et éléments.
        /// </summary>
        /// <see cref="IDataManager.ChargerComposite"/>
        public void ChargerComposite()
        {
            if (iDataManager.ChargerComposite() == null) return;
            foreach(PieceDeMusee pdm in iDataManager.ChargerComposite())
            {
                racine.Ajouter(pdm);
            }
        }

        /// <summary>
        /// Sauvegarde l'ensemble des données comprenant les collections et éléments.
        /// </summary>
        /// <see cref="IDataManager.SauvegarderComposite(IEnumerable{IPieceDeMusee})"/>
        public void SauvegarderMusee(IEnumerable<IPieceDeMusee> lPDM)
        {
            iDataManager.SauvegarderComposite(lPDM);
        }

        /// <summary>
        /// Charge l'ensemble des données relatives à l'organisme.
        /// </summary>
        /// <see cref="IDataManager.ChargerOrganisme"/>
        public void ChargerOrganisme()
        {
            Organisme = iDataManager.ChargerOrganisme();
        }

        /// <summary>
        /// Sauvegarde l'ensemble des données relatives à l'organisme.
        /// </summary>
        /// <see cref="IDataManager.SauvegarderOrganisme(Organisation)})"/>
        public void SauvegarderOrganisme(Organisation org)
        {
            iDataManager.SauvegarderOrganisme(org);
        }
    }
}
