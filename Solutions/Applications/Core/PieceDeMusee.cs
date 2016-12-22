// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : PieceDeMusee - source file
// Author : Samuel Bussy & Valentin Dumont
// Creation date: 13/05/2016
//
// ======================================================================== 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    internal abstract class PieceDeMusee : IPieceDeMusee, INotifyPropertyChanged
    {
        /// <summary>
        /// Propriété indiquant une zone spécifique du musée.
        /// </summary>
        [DataMember]
        public string Zone
        {
            get { return mZone; }
            private set
            {
                mZone = value;
                OnPropertyChanged("Zone");
            }
        }
        /// <summary>
        /// Membre privé qui définit la zone.
        /// </summary>
        private string mZone;

        /// <see cref="IPieceDeMusee.Nom"/>
        [DataMember]
        public string Nom
        {
            get { return mNom; }
            private set
            {
                mNom = value;
                OnPropertyChanged("Nom");
            }
        }
        /// <summary>
        /// Membre privé qui définit le nom.
        /// </summary>
        private string mNom;

        [DataMember]
        /// <see cref="IPieceDeMusee.ListePDM"/>
        virtual public ObservableCollection<IPieceDeMusee> ListePDM
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructeur de la classe abstraite.
        /// </summary>
        /// <param name="zone">Zone où se trouve l'objet.</param>
        public PieceDeMusee(string zone, string nom)
        {
            Zone = zone;
            Nom = nom;
            ListePDM = new ObservableCollection<IPieceDeMusee>();
        }

        /// <see cref="IPieceDeMusee.AllElemDePDM"/>
        public virtual IEnumerable<IPieceDeMusee> AllElemDePDM
        {
            get { return null; }
        }

        /// <see cref="IPieceDeMusee.AllColl"/>
        public virtual IEnumerable<IPieceDeMusee> AllColl
        {
            get
            {
                return ListePDM.Where(pdm => pdm is Collection).Concat(ListePDM.SelectMany(pdm => pdm.ListePDM).Where(pdm => pdm is Collection))
                                .OrderBy(l => l.Nom);
            }
        }

        /// <see cref="IPieceDeMusee.AllElemSeuls"/>
        public virtual IEnumerable<IPieceDeMusee> AllElemSeuls
        {
            get
            {
                return ListePDM.Where(pdm => pdm is Element).OrderBy(l => l.Nom);
            }
        }

        /// <summary>
        /// Evènement utilisé si les propriétés changent.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /***********************************************************************************/
        /*********************************Méthodes******************************************/
        /***********************************************************************************/

        /// <summary>
        /// Méthode permettant la modification d'une collection de l'organisme au besoin.
        /// </summary>
        /// <param name="zone">Nouvelle zone où se trouve l'objet.</param>
        /// <param name="nom">Nouveau nom de l'objet.</param>
        public void Modifier(string zone, string nom)
        {
            if (zone != null && nom != null)
            {
                Zone = zone;
                Nom = nom;
            }
            return;
        }

        /// <summary>
        /// Méthode permettant la modification d'une collection de l'organisme au besoin.
        /// </summary>
        /// <param name="zone">Nouvelle zone où se trouve l'objet.</param>
        /// <param name="nom">Nouveau nom de l'objet.</param>
        /// <param name="tab">Tableau des éléments de la collection modifié, si un élément est déjà présent on le supprime sinon on le rajoute.</param>
        public abstract void Modifier(string zone, string nom, params IPieceDeMusee[] tab);

        /// <summary>
        /// Méthode d'ajout d'un élément ou d'une collection dans la collection principale.
        /// </summary>
        /// <param name="pdm">Elément ou collection que l'on souhaite ajouter.</param>
        public abstract void Ajouter(PieceDeMusee pdm);

        /// <summary>
        /// Méthode de la suppression d'un élément
        /// </summary>
        /// <param name="elt"></param>
        public abstract void Supprimer(PieceDeMusee pdm);

        /// <summary>
        /// Méthode permettant l'ajout d'un média.
        /// </summary>
        /// <param name="media">Média à ajouter.</param>
        public abstract void AjouterMedia(Media media);

        /// <summary>
        /// Méthode pour la suppression d'un ou plusieurs médias d'un élément.
        /// </summary>
        /// <param name="pdm"></param>
        public abstract void SupprimerMédias(List<Media> lMedia);

        /// <summary>
        /// Méthode pour modifier un élément ou une collection.
        /// </summary>
        /// <param name="zone">Nouvelle zone de l'élément.</param>
        /// <param name="nom">Nouveau nom de l'élément.</param>
        /// <param name="carac">Nouvelles caractéristiques de l'élément.</param>
        /// <param name="desc">Nouvelle description de l'élément.</param>
        /// <param name="lMedia">Nouvelle liste des médias accompagnant l'élément.</param>
        /// <param name="auteur">Auteur de l'élément.</param>
        /// <param name="lPDM">Liste de sous-collections/éléments s'il s'agit d'une collection à modifier.</param>
        public abstract void Modifier(string zone, string nom, string carac = null, string desc = null, List<Media> lMedia = null, string auteur = null, IEnumerable<IPieceDeMusee> lPDM = null);

        /// <see cref="IPieceDeMusee.ToString"/>
        public override abstract string ToString();

        /// <summary>
        /// Pouvoir recupérer une liste de collections et éléments spécifiques.
        /// </summary>
        /// <param name="mot">Caractère(s) utilisé(s) pour effectuer une recherche.</param>
        /// <returns>Une liste de type PieceDeMusee comprenant les résultats de la recherche effectuée.</returns>
        public abstract IEnumerable<IPieceDeMusee> RechercheParCaracteres(string mot);


        /// <summary>
        /// Méthode qui gère les changement des propriétés de la classe.
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété qui change.</param>
        internal virtual void OnPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
            }
        }

    }
}

