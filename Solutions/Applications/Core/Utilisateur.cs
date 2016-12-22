// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Utilisateur - source file
// Author : Samuel Bussy & Valentin Dumont
// Creation date: 13/05/2016
//
// ======================================================================== 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract]
    internal class Utilisateur : IUtilisateur, IEquatable<Utilisateur>
    { 
/***********************Propriétés**********************/

        /// <summary>
        /// Propriété pour l'identifiant de l'utilisateur.
        /// </summary>
        [DataMember]
        public string Identifiant
        {
            get;
            private set;
        }

        /// <summary>
        /// Propriété pour le mot de passe de l'utilisateur.
        /// </summary>
        [DataMember]
        public string MotDePasse
        {
            get;
            private set;
        }

        /// <summary>
        /// Propriété pour le nom réél de l'utilisateur.
        /// </summary>
        [DataMember]
        public string Nom
        {
            get;
            private set;
        }

        /// <summary>
        /// Propriété pour le prénom réél de l'utilisateur.
        /// </summary>
        [DataMember]
        public string Prenom
        {
            get;
            private set;
        }

        /// <summary>
        /// Propriété pour le mail de l'utilisateur.
        /// </summary>
        [DataMember]
        public string Mail
        {
            get;
            private set;
        }

/**********************Constructeurs*********************/

        /// <summary>
        /// Constructeur de l'utilisateur avec seulement certains champs renseignés.
        /// </summary>
        /// <param name="identifiant">Identifiant de l'utilisateur</param>
        /// <param name="mdp">Mot de passe de l'utilisateur</param>
        /// <param name="mail">Email de l'utilisateur</param>
        public Utilisateur(string identifiant, string mdp, string mail)
        {
            Identifiant = identifiant;
            MotDePasse = mdp;
            Mail = mail;
        }

        /// <summary>
        /// Même constructeur que le précédent mais avec tous les champs renseignés.
        /// </summary>
        /// <param name="identifiant">Identifiant de l'utilisateur</param>
        /// <param name="mdp">Mot de passe de l'utilisateur</param>
        /// <param name="nom">Nom réél de l'utilisateur</param>
        /// <param name="prenom">Prénom réel de l'utilisateur</param>
        /// <param name="mail">Email de l'utilisateur</param>
        public Utilisateur(string identifiant, string mdp,string nom,string prenom, string mail)
            :this(identifiant, mdp, mail)
        {
            Nom = nom;
            Prenom = prenom;
        }


        /***********************************************************************************/
        /*********************************Méthodes******************************************/
        /***********************************************************************************/

        /// <summary>
        /// Redéfinit le HashCode de la classe Utilisateur
        /// </summary>
        /// <returns> int : Le HashCode de la classe </returns>
        public override int GetHashCode()
        {
            return Identifiant.GetHashCode();
        }

        /// <summary>
        /// Redéfinit la méthode Equals pour la classe Utilisateur
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

            return this.Equals(right as Utilisateur);
        }

        /// <summary>
        /// Vérifie si les identifiants sont les mêmes 
        /// </summary>
        /// <param name="other"></param>
        /// <returns> true : Les 2 identifiants sont les mêmes
        /// false : Les 2 identifiants ne sont pas les mêmes</returns>
        public bool Equals(Utilisateur other)
        {
            return (this.Identifiant == other.Identifiant);
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Utilisateur.
        /// </summary>
        /// <returns>string : Chaine de caractère pour décrire le contenu d'un utilisateur.</returns>
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append("Utilisateur :");
            res.AppendFormat("\nIdentifiant: {0}, MotDePasse: {1},\nNom: {2}, Prenom: {3}, Email: {4}", Identifiant, MotDePasse, Nom, Prenom, Mail);
            return res.ToString();
        }
        
    }


}
