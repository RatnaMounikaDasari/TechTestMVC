using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechTestMVC.Models;
using TechTestMVC.Repository;
using TechTestMVC.Services;
using TechTestMVC.ViewModels;

namespace TechTestMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CustomerViewModel customerNames = null;
            try
            {
                string customerFirstNamesByAge = string.Empty;
                IEnumerable<Customer> customerInfo = new List<Customer>();
                //Get Customer Data from Api
                customerInfo = await _customerService.GetCustomerData();
                if (customerInfo != null)
                {
                    //Get All Customer's first names (comma separated) who are 56.
                   string custFirstNames = _customerService.GetCustomerFirstNameByAge(customerInfo);
                    customerNames = new CustomerViewModel()
                    {
                        Name = custFirstNames
                    };
                }
                return View("Index",customerNames);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerIdWithPhoneNumber()
        {
            IEnumerable<Customer> customerInfo, customerDetails = new List<Customer>();
            IEnumerable<CustomerPhoneNumber> CustomerIDWithPhone=null;
            try {
              
                
                //Get Customer Data from Api
                customerInfo = await _customerService.GetCustomerData();
                if (customerInfo != null)
                {
                    //All customer’s IDs and associated phone numbers
                    customerDetails = _customerService.GetCustomerIDWithPhoneNumbers(customerInfo);
                    CustomerIDWithPhone = customerDetails.Select(a => new CustomerPhoneNumber()
                    {
                        PhoneNumber = "ID: " + a.Id + ", " + "Phone Number: " + a.PhoneNumber,
                    }).ToList();
                    ViewBag.CustomerIDWithPhone = JsonConvert.SerializeObject(CustomerIDWithPhone);


                }
                return View("CustomerPhoneNumber", CustomerIDWithPhone);
            }
            catch(Exception ex) { throw; }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerPhoneNumberByState()
        {
            IEnumerable<Customer> customerInfo, customerDetails = new List<Customer>();
            IEnumerable<CustomerPhoneNumber> CustomerByState = null;
            try
            {

                //Get Customer Data from Api
                customerInfo = await _customerService.GetCustomerData();
                if (customerInfo != null)
                {
                    //All customer’s IDs and associated phone numbers
                    customerDetails = _customerService.GetCustomerIDWithPhoneNumbers(customerInfo);
                    // The number of valid phone numbers per state, displayed in ascending alphabetical order
                    CustomerByState = _customerService.GetValidPhoneNumbersByState(customerDetails);
                    ViewBag.NumberByState = JsonConvert.SerializeObject(CustomerByState);

                }
                return View("CustomerPhoneNumber", CustomerByState);
            }
            catch (Exception ex) { throw; }
        }
    }
}
