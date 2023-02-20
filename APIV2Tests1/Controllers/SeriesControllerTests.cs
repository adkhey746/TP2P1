using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIV2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using APIV2.Models.EntityFramework;

namespace APIV2.Controllers.Tests
{

    [TestClass()]
    public class SeriesControllerTests
    {
        SeriesController controller;
        SeriesDbContext context;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; \npassword=postgres;");
            context = new SeriesDbContext(builder.Options);

            controller = new SeriesController(context);
        }

        [TestMethod()]
        public void GetSeriesTest()
        {
            Serie serie1 = new Serie(
                serieid: 1,
                titre: "Scrubs",
                resume: "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                nbsaisons: 9,
                nbepisodes: 184,
                anneecreation: 2001,
                network: "ABC (US)"
            );

            Serie serie2 = new Serie(
                serieid: 2,
                titre: "James May's 20th Century",
                resume: "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                nbsaisons: 1,
                nbepisodes: 6,
                anneecreation: 2007,
                network: "BBC Two"
            );

            List<Serie> series = new List<Serie>();
            series.Add(serie1);
            series.Add(serie2);

            var listSeries = controller.GetSeries();

            ActionResult<IEnumerable<Serie>> actionResult = listSeries.Result as ActionResult<IEnumerable<Serie>>;

            var allSeries = series.Where(s => s.Serieid <= 3).ToList();

            Assert.IsNotNull(actionResult, "Ne doit pas être nul");
            Assert.IsNotNull(actionResult.Value.Where(s => s.Serieid <= 2).ToList(), "Ne doit pas être nul");

            CollectionAssert.AreEqual(series, actionResult.Value.Where(s => s.Serieid <= 2).ToList(), "Pas bon");
        }

        public void GetSerieTest()
        {
            Serie serie1 = new Serie(
                serieid: 1,
                titre: "Scrubs",
                resume: "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                nbsaisons: 9,
                nbepisodes: 184,
                anneecreation: 2001,
                network: "ABC (US)"
            );

            Serie serie2 = new Serie(
                serieid: 2,
                titre: "That's not a real serie, test should fail",
                resume: "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                nbsaisons: 1,
                nbepisodes: 6,
                anneecreation: 2007,
                network: "BBC Two"
            );

            Assert.AreEqual(serie1, controller.GetSerie(1));
            Assert.AreNotEqual(serie1, controller.GetSerie(2));
        }

        public void DeleteSerieTest()
        {
            controller.DeleteSerie(138);
            Thread.Sleep(100);

            Serie serie1 = new Serie(
                serieid: 1,
                titre: "Scrubs",
                resume: "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                nbsaisons: 9,
                nbepisodes: 184,
                anneecreation: 2001,
                network: "ABC (US)"
            );

            Serie serie138 = new Serie(
                serieid: 138,
                titre: "Charlie Jade",
                resume: "Que se serait-il passé si les humains n'avaient pas abusé de la Terre et de ses ressources ? Combien le monde serait-il différent ? Faites un saut dans l'imaginaire et explorez le monde à travers 3 univers parallèles : L'Alphaverse (ce que notre monde pourrait devenir), le Betaverse (notre monde) et le Gammaverse (ce qu'aurait pu être notre monde).Charlie Jade est un détective privé dans un monde futuriste (Alphaverse) dominé par la technologie et les multinationales. Il enquête sur le meurtre d'une jeune femme inconnue... Charlie Jade est une série Sud Africaine, co-produite par le Canada, créée en 2005 par Robert Wertheimer et Chris Roland. Il s'agit d'une série originale par de nombreux aspects, notamment par le traitement visuel de l'image. La série bénéficie de décors naturels magnifiques pour le Gammaverse, sombre et futuriste à la 'Blade Runner' pour l'Alphaverse. Le téléspectateur peut ainsi identifier très facilement l'univers dans lequel se déroule chaque action. La série est tournée à Cape Town (Le Cap - Afrique du Sud)...",
                nbsaisons: 1,
                nbepisodes: 20,
                anneecreation: 2005,
                network: "Space"
            );

            Assert.AreEqual(controller.GetSerie(1), serie1);
            Assert.AreNotEqual(controller.GetSerie(138), serie138);
        }
    }
}