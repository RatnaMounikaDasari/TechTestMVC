using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVC.Models;
using TechTestMVC.Repository;

namespace TechTestMVC.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;

        }


        /// <summary>
        /// Gets  Customer Data 
        /// </summary>
        /// <returns>customer data</returns>
        public async Task<IEnumerable<Customer>> GetCustomerData()
        {
            return await this._customerRepository.GetCustomerData();
        }
        /// <summary>
        /// Gets  Customer's FirstName By Age
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>comma seperated customer's first name</returns>

        public string GetCustomerFirstNameByAge(IEnumerable<Customer> customerInfo)
        {
          return  this._customerRepository.GetCustomerFirstNameByAge(customerInfo);
        }
        /// <summary>
        /// Gets Valid Customer PhoneNumber with Id
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>list of customer id with phone numbers</returns>

        public IEnumerable<Customer> GetCustomerIDWithPhoneNumbers(IEnumerable<Customer> customerInfo)
        {
            return this._customerRepository.GetCustomerIDWithPhoneNumbers(customerInfo);
        }

        /// <summary>
        /// Gets Valid Customer PhoneNumber By State
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns>list of phone numbers group by state</returns>

        public IEnumerable<CustomerPhoneNumber> GetValidPhoneNumbersByState(IEnumerable<Customer> customerDetails)
        {
            return this._customerRepository.GetValidPhoneNumbersByState(customerDetails);

        }
    }
}
