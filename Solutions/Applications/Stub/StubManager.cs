using System;
using System.Collections.Generic;
using Core;
using System.IO;

namespace Stub
{
    public class StubManager : IDataManager
    {
        string cheminOrdi = string.Format("{0}/Stub/Images/",Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
        public IEnumerable<IPieceDeMusee> ChargerComposite()
        {
            return new List<PieceDeMusee>
            {
                new Collection("Base1","Animaux",
                    new Element("Base1","Chèvre","Taille : Moyenne\nPelage : Doux","Mange de l'herbe. Utile pour tondre la pelouse.",new Media(cheminOrdi+"chèvre.jpeg"),new Media(cheminOrdi+"chèvre2.jpg"),new Media(cheminOrdi+"chèvre3.jpg")),
                    new Element("Base1", "Rouge gorge", "Règne : Animalia\nPériode de nidification : Avril à Août\nNombre de couvaisons : 1-2\nMigration : De Septembre à Avril", "Le rouge-gorge est présent sur une grande partie du continent eurasien ainsi qu'en Afrique du Nord. Il est totalement absent du continent américain et de l'Océanie. Le rouge-gorge chante toute l'année sauf en été. En hiver, les deux sexes défendent chacun un territoire en chantant. Son chant mélodieux et allongé lui sert à défendre ses territoires de printemps et d'hiver. Sa nature peu farouche et son plumage attractif l'ont rendu populaire chez des générations de jardiniers. Le rouge-gorge fait partie d'une espèce d'oiseau très active, dont les adultes patrouillent et défendent vivement leur territoire. Présent dans presque chaque jardin, c'est l'un des oiseaux les plus familiers, cherchant sa nourriture à proximité des humains en train de jardiner. Il ira jusqu'à venir se nourrir de proies vivantes, comme des vers de terre ou des vers de farine, présentés à la main. Si l'hiver est rude, il deviendra encore plus familier, car vu son métabolisme, le manque de nourriture dû au froid le rend très vulnérable et la mortalité est alors importante, l'espèce étant casanière et rechignant à migrer.Le rouge-gorge défend son territoire à longueur d'année, sauf durant la mue ou si l'hiver est très froid. En hiver, les femelles aussi occupent et défendent un territoire, qui leur est nécessaire non seulement pour nicher mais aussi pour garantir des sources suffisantes de nourriture. Un rouge-gorge sans territoire meurt au bout de quelques semaines. C'est pourquoi cet espace est défendu avec une telle énergie. En général, il suffit que le propriétaire exhibe son plastron rouge pour que l'intrus recule mais il peut arriver que la lutte s'engage et les combats s'achèvent parfois par la mort de l'un des adversaires. À l'opposé de nombreux autres oiseaux, le rouge-gorge vit en solitaire pendant l'automne et l'hiver, mâle et femelle restant sur leur territoire hivernal respectif avec comme résultat qu'ils continuent à chanter même l'hiver, y compris la nuit. C'est surtout en hiver que le rouge-gorge vient dans les jardins des villes et des villages. À la belle saison, il habite les bois et les forêts ou le bocage dans les haies, les boqueteaux et sous-bois denses. Dans certaines régions, les rouges-gorges restent toute l'année près de l'homme. Ce petit oiseau passe la nuit sur un buisson touffu, un lierre, parfois dans un nichoir.",
                    new Media(cheminOrdi+"Rouge gorge.jpg"), new Media(cheminOrdi+"Rouge gorge2.jpg"), new Media(cheminOrdi+"Rouge gorge3.jpg")),
                    new Collection("Base10","Primates",
                            new Element("Base1", "Chimpanzé", "mignon", "trop mimi"),
                            new Element("Base1", "Gorille", "gros", "très gros"))
                        ),
                new Collection("Base2","Plantes",
                    new Element("Base2","Pissenlit","Classification : Angiosperme\nDamille : Composées\nPériode de floraison : Mars à Novembre\nType de fruit : Akène de type Samare\nMéthode de dissémination : le vent\nBiotope : prairies, bois clairs, clairières, chemins…\nDistribution : très commun jusqu’à 2000 m, plus rare en région méditerranéenne\nPériode de végétation : Vivace\nUtilisation : médicinale, cuisine","Le Pissenlit est également appelé Couronne de Moine, Dent de Lion, Florion d'Or, Laitue de Chien et Salade de Taupe. Il doit son surnom de dent de lion au fait de ses feuilles, disposées en rosettes, ressemblent  à la denture du félin. C'est aussi une des rares plantes qui porte une foultitude de surnoms. En France on dénombre plus d'appellations que de régions. On ne retrouve la trace du pissenlit qu'à partir du 15ème siècle où il est principalement utilisé par les apothicaires.  C'est une plante sauvage que l'on trouve dans de nombreuses prairies et qui peut se révéler envahissante. Il se referme la nuit et s'ouvre largement dès que le jour se lève. Sa floraison est très mellifère et les abeilles adorent butiner son nectar. Au jardin, le jardinier fulmine souvent après le pissenlit qui envahit la pelouse. Mais pour plusieurs raisons il mérite d'être réhabilité et installé au potager ou dans un coin du jardin. Il a longtemps été cultivé comme une salade, sa culture étant d'ailleurs la même. Il est très riche en vitamine C puisqu'il en renferme plus que l'épinard. Il est également possible de faire avec les racines de pissenlit séchées puis moulues très fines une sorte de café. Ne contenant pas de caféine mais des vitamines et des oligoéléments il est très bon pour la santé. Nos aïeux en utilisaient fréquemment. Côté culinaire, le pissenlit permettra de réaliser de bonnes salades et les feuilles peuvent également être cuites à la manière des épinards et servies en accompagnement d'une viande.",
                    new Media(cheminOrdi+"pissenlit.jpg"), new Media(cheminOrdi+"pissenlit2.jpg"))
                ),
                new Element("Base3","Asticot","petit","très petit"),
            };
        }
        public Organisation ChargerOrganisme()
        {
            return new Organisation("Musée Du Test", "5 Avenue du Test", "0978251342", "museedutest@mail.com",
                "Le musée du Test est ouvert du lundi au vendredi de 8h à 20h30 sans interruption et le samedi de 8h30 à 18h30. Nous vous rappelons que grâce à une entrée payée, vous avez accès à toutes les salles y compris aux évènements ponctuels tout au long de la journée. Merci de bien vouloir respecter les oeuvres présentes au sein du bâtiment. Nous vous souhaitons une agréable visite.");
        }

        public IEnumerable<IUtilisateur> ChargerUtilisateurs()
        {
            return new List<Utilisateur>
            {
                new Utilisateur("Toto","Toto+123","toto@mail.com"),
                new Utilisateur("Titi","TiTi","titi@mail.com"),
                new Administrateur("Admin","admin","Miel","Paul","admin@organisme.com"),
                new Administrateur("Jean","J3an4","Steech","Jean-Pierre","Steech.JeanP@organisme.com")
            };
        }

        public void SauvegarderComposite(IEnumerable<IPieceDeMusee> lPDM)
        {
            throw new NotImplementedException();
        }

        public void SauvegarderOrganisme(Organisation org)
        {
            throw new NotImplementedException();
        }

        public void SauvegardeUtilisateurs(IEnumerable<IUtilisateur> listeU)
        {
            throw new NotImplementedException();
        }
    }
}
