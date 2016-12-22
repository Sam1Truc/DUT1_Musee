// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Media - source file
// Author : Samuel Bussy & Valentin Dumont
// Creation date: 13/05/2016
//
// ======================================================================== 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    public class Media : IEquatable<Media>, INotifyPropertyChanged
    {
        /***********************Propriétés***********************/

        /// <summary>
        /// Chemin du média.
        /// </summary>
        [DataMemberAttribute]
        public string Chemin
        {
            get { return mChemin; }
            private set
            {
                mChemin = value;
                OnPropertyChanged("Chemin");
            }
        }
        /// <summary>
        /// Membre privé à la classe qui permet d'avoir le chemin du média.
        /// </summary>
        private string mChemin;

        /// <summary>
        /// Evènement utilisé si la propriété change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructeur de la classe abstraite Média
        /// </summary>
        /// <param name="chemin">Chemin du média</param>
        public Media(string chemin)
        {
            Chemin = chemin;
        }

        /// <summary>
        /// Redéfinit le HashCode de la classe Media
        /// </summary>
        /// <returns> int : Le HashCode de la classe </returns>
        public override int GetHashCode()
        {
            return Chemin.GetHashCode();
        }

        /// <summary>
        /// Redéfinit la méthode Equals pour la classe Media
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

            return this.Equals(right as Media);
        }

        /// <summary>
        /// Vérifie si les noms sont les mêmes 
        /// </summary>
        /// <param name="other"></param>
        /// <returns> true : Les 2 noms sont les mêmes
        /// false : Les 2 noms ne sont pas les mêmes</returns>
        public bool Equals(Media other)
        {
            return (this.Chemin == other.Chemin);
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Media.
        /// </summary>
        /// <returns>string : Chaine de caractère pour décrire le contenu d'un média.</returns>
        public override string ToString()
        {
            return string.Format("\tNom : {0}",Chemin);
        }


        /// <summary>
        /// Méthode qui gère les changement des propriétés de la classe.
        /// </summary>
        /// <param name="nomPropriété">Nom de la propriété qui change.</param>
        internal void OnPropertyChanged(string nomPropriété)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriété));
            }
        }

    }
}
