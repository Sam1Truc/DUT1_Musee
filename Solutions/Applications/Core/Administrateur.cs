// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Administrateur (Utilisateur) - source file
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
    internal class Administrateur : Utilisateur
    {

        /// <summary>
        /// Constructeur de la classe Administrateur qui se base sur celui de l'utilisateur avec
        /// tous les champs renseignés.
        /// </summary>
        /// <param name="identifiant">Identifiant permettant de connaître l'utilisateur.</param>
        /// <param name="mdp">Mot de passe qui est relié à l'identifiant.</param>
        /// <param name="nom">Nom réél de l'administrateur</param>
        /// <param name="prenom">Prénom réel de l'administrateur</param>
        /// <param name="mail">Email de l'administrateur</param>
        public Administrateur(string identifiant, string mdp, string nom, string prenom, string mail) 
            : base(identifiant, mdp, nom, prenom, mail)
        {
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Administrateur.
        /// </summary>
        /// <returns>string : Chaine de caractère pour décrire le contenu d'un administrateur.</returns>
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append(base.ToString());
            res.Replace("Utilisateur", "Administrateur");
            return res.ToString();
        }

    }
}
