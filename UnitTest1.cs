using ServiceReference1;
using System;


namespace SOAPHomework
{
    [TestClass]
    public class CountryListTest
    {
        //Global Variable   
        public CountryInfoServiceSoapTypeClient countryListTest;
        //private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryListTest =
        //    new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestInitialize]
        public void TestInitialize()
        {
            countryListTest = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }


        [TestMethod]
        public void AscCountryCode()
        {
            // Verify Ascending Order of Country Code
            var countryCode = countryListTest.ListOfCountryNamesByCode();
            var countryCodeAsc = countryCode.OrderBy(isoCode => isoCode.sISOCode);
            Assert.IsTrue(Enumerable.SequenceEqual(countryCodeAsc, countryCode), "Not Ascending Order");
        }

        [TestMethod]
        public void InvalidCountryCode()
        {
            // Verify Passing of Invalid Country Code
            var countryName = countryListTest.CountryName("XX");

            // To verify that method get a country when country code is correct
            Assert.AreEqual("Philippines", countryName, "Country not found in the database");

            // To verify that invalid country code is not found in the database
            Assert.AreEqual("Country not found in the database", countryListTest.CountryName("143"), "Country is found in the database");
        }

        [TestMethod]
        public void SameCountryName()
        {
            // Verify Country Name from both API is the same
            var lastCountryCode = countryListTest.ListOfCountryNamesByCode().Last();
            var countryName = countryListTest.CountryName(lastCountryCode.sISOCode);
            Assert.AreEqual(lastCountryCode.sName, countryName, "The country name from ListOfCountryNamesByCode() and CountryName() are Not Equal");
        }

    }
}