// ========================================================================
//
// Copyright (C) 2015-2016 IUT CLERMONT1 - UNIVERSITE D’AUVERGNE
// www.iut.u-clermont1.fr-
//
// Module : Organisation - source file
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
    public class Organisation : INotifyPropertyChanged
    {

        /********************Propriétés*************************/

        /// <summary>
        /// Propriété pour le nom de l'organisme.
        /// </summary>
        [DataMember]
        public string NomOrg
        {
            get { return mNomOrg; }
            private set
            {
                
                mNomOrg = value;
                OnPropertyChanged("NomOrg");
            }
        }
        /// <summary>
        /// Membre privé qui définit le nom.
        /// </summary>
        private string mNomOrg;

        /// <summary>
        /// Propriété pour l'adresse de l'organisme.
        /// </summary>
        [DataMember]
        public string AdresseOrg
        {
            get { return mAdressOrg; }
            private set
            {
                mAdressOrg = value;
                OnPropertyChanged("AdresseOrg");
            }
        }
        /// <summary>
        /// Membre privé pour définir l'addresse.
        /// </summary>
        private string mAdressOrg;

        /// <summary>
        /// Propriété pour le numéro de téléphone de l'organisme.
        /// </summary>
        [DataMember]
        public string TelOrg
        {
            get { return mTelOrg; }
            private set
            {
                mTelOrg = value;
                OnPropertyChanged("TelOrg");
            }
        }
        /// <summary>
        /// Membre privé qui définit le numéro téléphone.
        /// </summary>
        private string mTelOrg;

        /// <summary>
        /// Propriété pour l'email.
        /// </summary>
        [DataMember]
        public string MailOrg
        {
            get { return mMailOrg; }
            private set
            {
                mMailOrg = value;
                OnPropertyChanged("MailOrg");
            }
        }
        /// <summary>
        /// Membre privé qui définit l'adresse mail.
        /// </summary>
        private string mMailOrg;

        /// <summary>
        /// Propriété pour la description de l'organisme.
        /// </summary>
        [DataMember]
        public string DescOrg
        {
            get { return descOrg; }
            private set
            {
                descOrg = value;
                OnPropertyChanged("DescOrg");
            }
        }
        /// <summary>
        /// Membre privé qui définit la description.
        /// </summary>
        private string descOrg;

        /// <summary>
        /// Evènement utilisé si la propriété change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Méthode qui gère les changement des propriétés de la classe.
        /// </summary>
        /// <param name="nomPropriété">Nom de la propriété qui change.</param>
        private void OnPropertyChanged(string nomPropriété)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriété));
            }
        }

        /// <summary>
        /// Constructeur de la classe Organisation.
        /// </summary>
        /// <param name="nom">Nom de l'organisme</param>
        /// <param name="adresse">Adresse de l'organisme</param>
        /// <param name="tel">Numéro de téléphone de l'organisme</param>
        /// <param name="mail">Email de l'organisme</param>
        /// <param name="description">Description de l'organisme (horaires..)</param>
        public Organisation(string nom, string adresse, string tel, string mail, string description)
        {
            NomOrg = nom;
            AdresseOrg = adresse;
            TelOrg = tel;
            MailOrg = mail;
            DescOrg = description;
        }

        public void ModifOrg(string nom, string adresse, string tel, string mail, string description)
        {
            NomOrg = nom;
            AdresseOrg = adresse;
            TelOrg = tel;
            MailOrg = mail;
            DescOrg = description;
        }

        /// <summary>
        /// Redéfinit la méthode ToString() pour la classe Organisation.
        /// </summary>
        /// <returns>string : Chaine de caractère pour décrire le contenu d'un organisation.</returns>
        public override string ToString()
        {
            return string.Format("Nom: {0}, Adresse: {1}, Numéro de téléphone : {2}, Email : {3},\nDesciption: {4} \n",NomOrg,AdresseOrg,TelOrg,MailOrg,DescOrg);
        }

    }
}
