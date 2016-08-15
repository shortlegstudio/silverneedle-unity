using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;
using ShortLegStudio.RPG.Actions.NamingThings;
using ShortLegStudio.RPG.Names.Gateways;


namespace RPG.Actions.NamingThings {

	[TestFixture]
	public class NameCharacterTests {
        [Test]
        public void UtilizingAGatewayOfNamesItWillSelectAFirstAndLastName() 
        {
            // Set up test with a name
            var nameGateway = new TestNamesGateway();
            var namer = new NameCharacter(nameGateway);

            nameGateway.FirstName = "John";
            nameGateway.LastName = "Smith";
            Assert.AreEqual("John Smith", namer.CreateFullName());

            // Make a different name
            nameGateway.FirstName = "Alexi";
            nameGateway.LastName = "Johnson";

            Assert.AreEqual("Alexi Johnson", namer.CreateFullName());
        }


        private class TestNamesGateway : ICharacterNamesGateway 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public IList<string> GetFirstNames() 
            {
                return new List<string>(new string[] { FirstName });
            }

            public IList<string> GetLastNames() 
            {
                return new List<string>(new string[] { LastName });
            }
        }
	}
}