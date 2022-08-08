using System;
using System.Collections.Generic;
using System.Text;
using TechTestMVC.Controllers;
using TechTestMVC.Formaters;
using TechTestMVC.Models;
using Xunit;

namespace TechTestMVC.Tests.Validation
{
    public class PhoneNumberFormaterTests
    {
        private readonly PhoneNumberFormater _phoneNumberFormater;
        private readonly CustomerController _CustomerController;
        public PhoneNumberFormaterTests()
        {
            this._phoneNumberFormater = new PhoneNumberFormater();
        }
        [Theory]
        [InlineData("0281828089")]
        public void IsValid_PhoneNumber_Returns_True(string phoneNumber)
        {
            //Arrange
            bool isvalid = false;
            //Act
            var result = _phoneNumberFormater.isValidMobileNumber(phoneNumber, "NSW", out isvalid);
            //Assert
            Assert.Equal("(02) 8182 8089", result);
        }
        [Theory]
        [InlineData("0281828")]
        public void IsValid_PhoneNumber_Returns_Invalid(string phoneNumber)
        {
            //Arrange
            bool isvalid = false;
            var expected = "Invalid";
            //Act
            var result = _phoneNumberFormater.isValidMobileNumber(phoneNumber, "NSW", out isvalid);
            //Assert
            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("")]
        public void IsValid_PhoneNumber_Returns_Missed(string phoneNumber)
        {
            //Arrange
            bool isvalid = false;
            var expected = "Missed";
            //Act
            var result = _phoneNumberFormater.isValidMobileNumber(phoneNumber, "NSW", out isvalid);
            //Assert
            Assert.Equal(expected, result);
        }
      
    }
}
