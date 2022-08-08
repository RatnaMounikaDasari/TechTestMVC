using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using TechTestMVC.Controllers;
using TechTestMVC.Formaters;
using TechTestMVC.Models;
using TechTestMVC.Repository;
using TechTestMVC.Services;
using TechTestMVC.ViewModels;
using Xunit;

namespace TechTestMVC.Tests.Validation
{
    public class CustomerControllerTests
    {
        private readonly PhoneNumberFormater _phoneNumberFormater;
        private readonly CustomerController _customerController;
        private readonly ICustomerService _customerService;
        public CustomerControllerTests()
        {
            this._phoneNumberFormater = new PhoneNumberFormater();
            this._customerService = A.Fake<ICustomerService>();

            this._customerController = A.Fake<CustomerController>();
        }

        //checks valid phone number and returns in a format
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

        //checks a phone number and return Invalid if it is not a valid number
        [Theory]
        [InlineData("0281828")]
        [InlineData("028-18-28")]
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

        //checks a phone number and return Missed if it is not a valid number
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

        //returns view with customer firstnames
        [Fact]
        public void CustomerController_Index_Returns_CustomersFirstName_View()
        {
            //Arrange
            var customerViewModel = A.Fake<CustomerViewModel>();
            var customerInfo = A.Fake<IEnumerable<Customer>>();
            string customerFirstNames="";
          
            //Act
            A.CallTo(() => _customerService.GetCustomerFirstNameByAge(customerInfo)).Returns<string>(customerFirstNames);
            var result = _customerController.Index();
            //Assert
            Assert.NotNull(result);
            result.Should().BeOfType<Task<IActionResult>>();
        }
        //returns view with customer id with valid phonenumbers
        [Fact]
        public void CustomerController_GetCustomerIdWithPhoneNumber_Returns_View()
        {
            //Arrange
            var customerInfo = A.Fake<IEnumerable<Customer>>();
            var CustomerIdWithPhoneNumber = A.Fake<CustomerPhoneNumber>();

            //Act
            A.CallTo(() => _customerService.GetCustomerIDWithPhoneNumbers(customerInfo)).Returns<IEnumerable<Customer>>(customerInfo);
            var result = _customerController.Index();
            //Assert
            Assert.NotNull(result);
            result.Should().BeOfType<Task<IActionResult>>();
        }

        //returns view with valid customer phone number by state
        [Fact]
        public void CustomerController_GetCustomerPhoneNumberByState_Returns_View()
        {
            //Arrange
            var customerInfo = A.Fake<IEnumerable<Customer>>();
            var CustomerIdWithPhoneNumber = A.Fake<IEnumerable<CustomerPhoneNumber>>();

            //Act
            A.CallTo(() => _customerService.GetValidPhoneNumbersByState(customerInfo)).Returns<IEnumerable<CustomerPhoneNumber>>(CustomerIdWithPhoneNumber);
            var result = _customerController.Index();
            //Assert
            Assert.NotNull(result);
            result.Should().BeOfType<Task<IActionResult>>();
        }

    }
}
