using System;
using System.Collections.Generic;
using Core;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections.ObjectModel;

namespace Persistance_XML
{
    public class PersistanceXML : IDataManager
    {

        /// <summary>
        /// Chargement par Deserialisation d'une liste d'utilisateur à partir du fichier "utilisateurs.xml"
        /// </summary>
        /// <returns>Retourne une liste d'utilisateurs issus du fichier</returns>
        public IEnumerable<IUtilisateur> ChargerUtilisateurs()
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "utilisateurs.xml");

            List<IUtilisateur> listeDefaut = new List<IUtilisateur>() { new Administrateur("Admin", "admin", "default", "default", "none") };
            var dc = new DataContractSerializer(typeof(IEnumerable<IUtilisateur>),
                new Type[] { typeof(Utilisateur), typeof(Administrateur) }, 1000, false, true, null);

            if (!Directory.Exists(dirData)) { Directory.CreateDirectory(dirData); }

            if (!File.Exists(xmlFile))
            {
                File.Create(xmlFile);
                return listeDefaut;
            }
            else
            {
                using (Stream s = File.OpenRead(xmlFile))
                {
                    if (s.Length > 0)
                    {
                        return dc.ReadObject(s) as IEnumerable<IUtilisateur>;
                    }
                    else return listeDefaut;
                }
            }

        }

        /// <summary>
        /// Sauvegarde par Serialisation d'une liste d'utilisateurs dans le fichier "utilisateurs.xml"
        /// </summary>
        /// <param name="listeU"></param>
        public void SauvegardeUtilisateurs(IEnumerable<IUtilisateur> listeU)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "utilisateurs.xml");

            var ds = new DataContractSerializer(typeof(IEnumerable<IUtilisateur>),
                new Type[] { typeof(Utilisateur), typeof(Administrateur) }, 1000, false, true, null);

            if (!Directory.Exists(dirData)) Directory.CreateDirectory(dirData);

            if (!File.Exists(xmlFile)) File.Create(xmlFile);

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

            using (XmlWriter w = XmlWriter.Create(xmlFile, settings))
            {
                ds.WriteObject(w, listeU);
            }
        }

        /// <summary>
        /// Chargement par Deserialisation d'une organisation à partir du fichier "organisation.xml"
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPieceDeMusee> ChargerComposite()
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "piecedemusee.xml");

            var dc = new DataContractSerializer(typeof(IEnumerable<IPieceDeMusee>),
                new Type[] { typeof(ObservableCollection<IPieceDeMusee>), typeof(Collection), typeof(Element), typeof(Media) });

            if (!Directory.Exists(dirData)) Directory.CreateDirectory(dirData);

            if (!File.Exists(xmlFile))
            {
                File.Create(xmlFile);
                return null;
            }
            else
            {
                using (Stream s = File.OpenRead(xmlFile))
                {
                    if (s.Length > 0)
                    {
                        return dc.ReadObject(s) as IEnumerable<IPieceDeMusee>;
                    }
                    else return null;
                }
            }
        }

        /// <summary>
        /// Sauvegarde par Serialisation d'une organisation dans le fichier "organisation.xml"
        /// </summary>
        /// <param name="lPDM"></param>
        public void SauvegarderComposite(IEnumerable<IPieceDeMusee> lPDM)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "piecedemusee.xml");

            var ds = new DataContractSerializer(typeof(IPieceDeMusee),
                 new Type[] { typeof(ObservableCollection<IPieceDeMusee>), typeof(Collection), typeof(Element), typeof(Media) } );

            if (!Directory.Exists(dirData)) Directory.CreateDirectory(dirData);

            if (!File.Exists(xmlFile)) File.Create(xmlFile);

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

            using (XmlWriter w = XmlWriter.Create(xmlFile, settings))
            {
                ds.WriteObject(w, lPDM);
            }
        }

        /// <summary>
        /// Chargement par Deserialisation d'un organisme à partir du fichier "organisme.xml"
        /// </summary>
        /// <returns>Retourne une liste d'utilisateurs issus du fichier</returns>
        public Organisation ChargerOrganisme()
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "organisme.xml");

            Organisation orgnull = new Organisation(null, null, null, null, null);
            var dc = new DataContractSerializer(typeof(Organisation));

            if (!Directory.Exists(dirData)) Directory.CreateDirectory(dirData);

            if (!File.Exists(xmlFile))
            {
                File.Create(xmlFile);
                return orgnull;
            }
            else
            {
                using (Stream s = File.OpenRead(xmlFile))
                {
                    if (s.Length > 0)
                    {
                        return dc.ReadObject(s) as Organisation;
                    }
                    else return orgnull;
                }
            }
        }

        /// <summary>
        /// Sauvegarde par Serialisation d'un organisme dans le fichier "organisme.xml"
        /// </summary>
        /// <param name="org"></param>
        public void SauvegarderOrganisme(Organisation org)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            string dirData = string.Format("{0}\\Persistance_XML\\", dirInfo.FullName);
            string xmlFile = string.Format("{0}{1}", dirData, "organisme.xml");

            var ds = new DataContractSerializer(typeof(Organisation));

            if (!Directory.Exists(dirData)) Directory.CreateDirectory(dirData);

            if (!File.Exists(xmlFile)) File.Create(xmlFile);

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

            using (XmlWriter w = XmlWriter.Create(xmlFile, settings))
            {
                ds.WriteObject(w, org);
            }
        }

    }


}
