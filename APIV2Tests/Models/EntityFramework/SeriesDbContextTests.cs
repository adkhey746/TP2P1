using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIV2.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APIV2.Models.EntityFramework.Tests
{
    [TestClass()]
    public class SeriesDbContextTests
    {
        [TestMethod()]
        public void SeriesDbContextTest()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres;\npassword=postgres;"); // Chaine de connexion à mettre dans les ( )
            SeriesDbContext context = new SeriesDbContext(builder.Options);
        }

        [TestMethod()]
        public void SeriesDbContextTest1()
        {
            Assert.Fail();
        }
    }
}