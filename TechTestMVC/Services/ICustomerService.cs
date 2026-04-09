using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVC.Models;

namespace TechTestMVC.Services
{
   public interface ICustomerService
    {

        /// <summary>
        /// Gets  Customer Data from Api
        /// </summary>
        /// <returns>Customer Data</returns>
        Task<IEnumerable<Customer>> GetCustomerData();

        /// <summary>
        /// Gets  Customer's FirstName By Age
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>comma seperated customer's first name</returns>
        string GetCustomerFirstNameByAge(IEnumerable<Customer> customerInfo);

        /// <summary>
        /// Gets valid Customer Id PhoneNumber 
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>list of customer id with phone numbers</returns>
        IEnumerable<Customer> GetCustomerIDWithPhoneNumbers(IEnumerable<Customer> customerInfo);

        /// <summary>
        /// Gets Valid Customer PhoneNumber By State
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns>list of phone numbers group by state</returns>
        IEnumerable<CustomerPhoneNumber> GetValidPhoneNumbersByState(IEnumerable<Customer> customerDetails);

        /// <summary>
        /// Generates numbers from 1 to 100, retrieves even numbers using LINQ, 
        /// and identifies numbers divisible by 3 or 5 (but not both), 
        /// </summary>
        /// <returns>returning the results as a tuple</returns>
        (List<int> EvenNumbers, List<int> DivisibleNumbers) GetNumbers();

        (bool FirstEventScheduled, bool SecondEventScheduled, bool Cancelled, List<Event> Events) RunTask2();
    }
}
