// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Collection (PieceDeMusee) - source file
// Author : Samuel Bussy & Valentin Dumont
// Creation date: 13/05/2016
//
// ======================================================================== 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Core
{
    [DataContract]
    internal class Collection : PieceDeMusee, IEquatable<Collection>
    {

        /// <summary>
        /// "Encapsulation" de la liste de PièceDeMusée.
        /// </summary>
        public override ObservableCollection<IPieceDeMusee> ListePDM
        {
            get
            {
                return new ObservableCollection<IPieceDeMusee>(listePDM);
            }
        }
        /// <summary>
        /// Liste de PieceDeMusee qui permet à une collection de contenir des sous-collections et/ou des
        /// éléments.
        /// </summary>
        [DataMember]
        internal ObservableCollection<PieceDeMusee> listePDM = new ObservableCollection<PieceDeMusee>();

        /// <see cref="PieceDeMusee.AllElemDePDM"/>
        public override IEnumerable<IPieceDeMusee> AllElemDePDM
        {
            get
            {
                return (ListePDM.Where(pdm => pdm is Element).Concat(ListePDM.SelectMany(pdm => pdm.AllElemSeuls))).Where(pdm => pdm is Element).OrderBy(l =>l.Nom);
            }
        }

        /// <summary>
        /// Constructeur de la classe Collection
        /// </summary>
        /// <param name="zone">Zone où est localisée la collection dans l'organisme.</param>
        /// <param name="nom">Nom de la collection</param>
        /// <param name="liste">Sous éléments/collections de la collection.</param>
        public Collection(string zone, string nom, params IPieceDeMusee[] tab)
            : base(zone, nom)
        {

            foreach (IPieceDeMusee pdm in tab)
            {
                listePDM.Add(pdm as PieceDeMusee);
            }

            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemDePDM");
            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllColl");
            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemSeuls");

        }

        /***********************************************************************************/
        /*********************************Méthodes******************************************/
        /***********************************************************************************/

        /// <summary>
        /// Redéfinit le HashCode de la classe Collection
        /// </summary>
        /// <returns> int : Le HashCode de la classe </returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode();
        }

        /// <summary>
        /// Redéfinit la méthode Equals pour la classe Collection
        /// </summary>
        /// <param name="right"></param>
        /// <returns> true : Right est le même objet que le courant
        /// false : Right n'est pas le même objet que le courant</returns>
        public override bool Equals(object right)
        {
            //check null
            if (object.ReferenceEquals(right, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, right))
            {
                return true;
            }

            if (this.GetType() != right.GetType())
            {
                return false;
            }

            return this.Equals(right as Collection);
        }

        /// <summary>
        /// Vérifie si les noms sont les mêmes 
        /// </summary>
        /// <param name="other"></param>
        /// <returns> true : Les 2 noms sont les mêmes
        /// false : Les 2 noms ne sont pas les mêmes</returns>
        public bool Equals(Collection other)
        {
            return (this.Nom == other.Nom);
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Collection.
        /// </summary>
        /// <returns>string : Chaine de caractères pour décrire le contenu d'une collection.</returns>
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.AppendFormat("\nCollection {0}:", Nom);
            if (listePDM.Count > 0)
            {
                res.Append("\nContenu :");
                foreach (PieceDeMusee p in listePDM)
                {
                    res.Append(p.ToString());
                }
            }
            else
            {
                res.Append("\nElle ne contient de pas de contenu pour le moment.");
            }
            return res.ToString();
        }

        /// <summary>
        /// Fonction qui met à jour toutes les listes de la collection.
        /// </summary>
        public void MiseAJourListes()
        {
            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemDePDM");
            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllColl");
            listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemSeuls");
        }

        ///<see cref="PieceDeMusee.Ajouter(PieceDeMusee)"/>
        public override void Ajouter(PieceDeMusee pdm)
        {
            if (!listePDM.Contains(pdm))
            {
                listePDM.Add(pdm);
            }
        }

        /// <see cref="PieceDeMusee.Supprimer(PieceDeMusee)"/>
        public override void Supprimer(PieceDeMusee pdm)
        {
            if (pdm != null)
            {

                if (listePDM.Contains(pdm))
                {
                    listePDM.Remove(pdm);
                }
                else
                {
                    var collMère = listePDM.SingleOrDefault(c => c.ListePDM.Contains(pdm));
                    if (collMère != null)
                    {
                        (collMère as Collection).Supprimer(pdm);
                        listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemDePDM");
                        listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllColl");
                        listePDM.CollectionChanged += (o, args) => OnPropertyChanged("AllElemSeuls");
                    }
                }
            }
        }

        /// <see cref="PieceDeMusee.Modifier(string, string, IPieceDeMusee[])"/>
        public override void Modifier(string zone, string nom, params IPieceDeMusee[] tab)
        {
            Modifier(zone, nom);
            if (tab == null) { return; }
            var eltsPremierNiveau = listePDM.Where(e => e is Element);
            var collSousNiveaux = listePDM.Where(c => c is Collection);

            foreach (IPieceDeMusee ipdm in tab)
            {
                if ((listePDM.Where(e => e is Element).Concat(listePDM.SelectMany(p => p.ListePDM))).Contains(ipdm))
                {
                    if (eltsPremierNiveau.Contains(ipdm as PieceDeMusee))
                    {
                        listePDM.Remove(ipdm as PieceDeMusee);
                    }
                    else
                    {
                        var sousCollection = collSousNiveaux.SingleOrDefault(c => c.ListePDM.Contains(ipdm));
                        if (sousCollection != null)
                        {
                            sousCollection.Supprimer(ipdm as PieceDeMusee);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Méthode qui renvoit tous les éléments/collections qui ont leur nom contenant la chaine de caractères.
        /// </summary>
        /// <param name="caractere">Chaine de caractères</param>
        /// <returns>
        /// Une collection d'objets de type IPieceDeMusee selon la recherche.</returns>
        public IEnumerable<IPieceDeMusee> RechercheEltsParCaracteres(string caracteres)
        {
            return ((ListePDM.Select(p => p)).Concat(ListePDM.SelectMany(p => p.ListePDM))).Where(l => l.Nom.Contains(caracteres));
        }

        /// <see cref="PieceDeMusee.RechercheParCaracteres(string)"/>
        public override IEnumerable<IPieceDeMusee> RechercheParCaracteres(string mot)
        {
            return ListePDM.Where(pdm => pdm is Element && pdm.Nom.Contains(mot)).Concat(ListePDM.SelectMany(pdm => pdm.ListePDM).Where(pdm => pdm is Element && pdm.Nom.Contains(mot)));
        }

        public override void Modifier(string zone, string nom, string carac = null, string desc = null, List<Media> lMedia = null, string auteur = null, IEnumerable<IPieceDeMusee> lPDM = null)
        {
            throw new NotImplementedException();
        }

        public override void SupprimerMédias(List<Media> lMedia)
        {
            throw new NotImplementedException();
        }

        public override void AjouterMedia(Media media)
        {
            throw new NotImplementedException();
        }
    }
}
