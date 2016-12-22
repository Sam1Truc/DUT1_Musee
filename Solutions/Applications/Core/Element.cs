// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Element (PieceDeMusee) - source file
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
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    internal class Element : PieceDeMusee, IEquatable<Element>
    {
        /***************************Propriétés*****************************/

        /// <summary>
        /// Propriété pour la description de l'élément
        /// </summary>
        [DataMember]
        public string Description
        {
            get { return mDescription; }
            private set
            {
                mDescription = value;
                OnPropertyChanged("Description");
            }
        }
        /// <summary>
        /// Membre privé pour la description.
        /// </summary>
        private string mDescription;

        /// <summary>
        /// Propriété pour l'auteur/découvreur de l'élémént
        /// </summary>
        [DataMember]
        public string Auteur
        {
            get { return mAuteur; }
            private set
            {
                mAuteur = value;
                OnPropertyChanged("Auteur");
            }
        }
        /// <summary>
        /// Membre privé pour l'auteur.
        /// </summary>
        private string mAuteur;

        /// <summary>
        /// Propriété pour la description des caractéristiques de l'élément
        /// </summary>
        [DataMember]
        public string Caracteristiques
        {
            get { return mCaracteristiques; }
            private set
            {
                mCaracteristiques = value;
                OnPropertyChanged("Caracteristiques");
            }
        }
        /// <summary>
        /// Membre privé pour les caractéristiques de l'élément.
        /// </summary>
        private string mCaracteristiques;

        /// <summary>
        /// Encapsulation de la liste d'ensemble de médias par une collection ReadOnlyCollection.
        /// </summary>
        public ObservableCollection<Media> EnsMedia
        {
            get { return ensMedia; }
        }
        /// <summary>
        /// Collection privée d'un ensemble d'objets de type Média.
        /// </summary>
        [DataMember]
        private ObservableCollection<Media> ensMedia = new ObservableCollection<Media>();

        /// <summary>
        /// Déclaration d'une exception spécifique pour les méthodes de PieceDeMusee qui ne sont pas utiles dans la classe Element.
        /// </summary>
        Exception ExceptionNonElt = new Exception("Cette méthode n'est pas à utiliser sur un élément.");


        /***********************************Constructeurs********************************/

        /// <summary>
        /// Constructeur de la classe élément sans l'auteur car s'il s'agit d'une plante ou d'un animal,
        /// il n'y a pas toujours de noms de celui qui l'a découvert le premier.
        /// </summary>
        /// <param name="zone">Zone où se trouve l'élément dans l'organisme</param>
        /// <param name="nom">Nom de l'élément</param>
        /// <param name="caracteristiques">Caractéristiques de l'élément</param>
        /// <param name="description">Description de l'élément</param>
        public Element(string zone, string nom, string caracteristiques, string description, params Media[] medias)
            : base(zone, nom)
        {
            Caracteristiques = caracteristiques;
            Description = description;
            foreach (Media med in medias)
            {
                ensMedia.Add(med);
            }

        }

        /// <summary>
        /// Constructeur de la classe élément avec l'auteur renseigné
        /// </summary>
        /// <param name="zone">Zone où se trouve l'élément dans l'organisme</param>
        /// <param name="nom">Nom de l'élément</param>
        /// <param name="caracteristiques">Caractéristiques de l'élément</param>
        /// <param name="description">Description de l'élément</param>
        /// <param name="auteur">Auteur/Découvreur de l'élément</param>
        public Element(string zone, string nom, string caracteristiques, string description, string auteur, params Media[] medias)
            : this(zone, nom, caracteristiques, description, medias)
        {
            Auteur = string.Format("Auteur : {0}", auteur);
        }


        /***********************************************************************************/
        /*********************************Méthodes******************************************/
        /***********************************************************************************/

        /// <summary>
        /// Redéfinit le HashCode de la classe Element
        /// </summary>
        /// <returns> int : Le HashCode de la classe </returns>
        public override int GetHashCode()
        {
            return Nom.GetHashCode();
        }

        /// <summary>
        /// Redéfinit la méthode Equals pour la classe Element
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

            return this.Equals(right as Element);
        }

        /// <summary>
        /// Vérifie si les noms sont les mêmes 
        /// </summary>
        /// <param name="other"></param>
        /// <returns> true : Les 2 noms sont les mêmes
        /// false : Les 2 noms ne sont pas les mêmes</returns>
        public bool Equals(Element other)
        {
            return (this.Nom == other.Nom);
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Element.
        /// </summary>
        /// <returns>string : Chaine de caractère pour décrire le contenu d'un élément.</returns>
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            if (Auteur != null)
            {
                res.AppendFormat("\nElément {0} :\nZone: {1},Auteur: {2}\nDescription: {3}\nCaractéristique(s): {4}\nListe des médias associés:\n", Nom, Zone, Auteur, Description, Caracteristiques);
            }
            else
            {
                res.AppendFormat("\nElément {0} :\nZone: {1}\nDescription: {2}\nCaractéristiques: {3}\nListe des médias associés:\n", Nom, Zone, Description, Caracteristiques);
            }

            if (ensMedia != null)
            {
                if (ensMedia.Count > 0)
                {
                    foreach (Media m in EnsMedia)
                    {
                        res.Append("\n" + m.ToString());
                    }
                }
            }
            else
            {
                res.AppendFormat("{0} ne contient pas de médias associé(s)", Nom);
            }
            return res.ToString();
        }

        /// <see cref="PieceDeMusee.Modifier(string, string, string, string, List{Media}, string, List{PieceDeMusee})"/>
        public override void Modifier(string zone, string nom, string caracteristiques, string description, List<Media> lMedia, string auteur = null, IEnumerable<IPieceDeMusee> lPDM = null)
        {
            if (caracteristiques != null && description != null)
            {
                if (auteur != null)
                {
                    Auteur = string.Format("Auteur : {0}", auteur);
                }
                else
                {
                    Auteur = "";
                }
                Modifier(zone, nom);
                Caracteristiques = caracteristiques;
                Description = description;
                if (lMedia == null) return;
                foreach (Media media in lMedia)
                {
                    ensMedia.Add(media);
                }
            }
        }

        /// <summary>
        /// Méthode qui doit être réécrite mais qui renvoit une exception car elle ne doit pas être redéfinie ici.
        /// </summary>
        /// <param name="pdm">Un élément ou une collection</param>
        public override void Ajouter(PieceDeMusee pdm)
        {
            throw ExceptionNonElt;
        }

        /// <summary>
        /// Méthode qui doit être réécrite mais qui renvoit une exception car elle ne doit pas être redéfinie ici.
        /// </summary>
        /// <param name="pdm">Un élément ou une collection</param>
        public override void Supprimer(PieceDeMusee pdm)
        {
            throw ExceptionNonElt;
        }

        /// <see cref="PieceDeMusee.AjouterMedia"/>
        public override void AjouterMedia(Media media)
        {
            if (ensMedia != null)
            {
                if (!ensMedia.Contains(media))
                {
                    ensMedia.Add(media);
                }
            }
            else
            {
                ensMedia = new ObservableCollection<Media>();
                AjouterMedia(media);
            }
            return;
        }

        /// <see cref="PieceDeMusee.SupprimerMédias"/>
        public override void SupprimerMédias(List<Media> lMedia)
        {
            foreach (Media media in lMedia)
            {
                if (ensMedia.Contains(media))
                {
                    ensMedia.Remove(media);
                }
            }
        }

        public override IEnumerable<IPieceDeMusee> RechercheParCaracteres(string mot)
        {
            throw ExceptionNonElt;
        }

        /// <see cref="PieceDeMusee.Modifier(string, string, IPieceDeMusee[])"/>
        public override void Modifier(string zone, string nom, params IPieceDeMusee[] tab)
        {
            throw new NotImplementedException();
        }
    }
}
