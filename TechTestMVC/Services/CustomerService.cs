using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTestMVC.Models;
using TechTestMVC.Repository;

namespace TechTestMVC.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventScheduler _eventScheduler;
        public CustomerService(ICustomerRepository customerRepository, IEventScheduler eventScheduler)
        {
            this._customerRepository = customerRepository;
            _eventScheduler = eventScheduler;
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
            return this._customerRepository.GetCustomerFirstNameByAge(customerInfo);
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

        /// <summary>
        /// Generates numbers from 1 to 100, retrieves even numbers using LINQ, 
        /// and identifies numbers divisible by 3 or 5 (but not both), 
        /// returning the results as a tuple.
        /// </summary>
        /// <returns></returns>

        public (List<int> EvenNumbers, List<int> DivisibleNumbers) GetNumbers()
        {
            // Create list 1 to 100
            var numbers = Enumerable.Range(1, 100).ToList();

            //// Even numbers using LINQ
            var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
            // Divisible by 3 or 5 but NOT both
            var divisble = numbers
                .Where(n => (n % 3 == 0 || n % 5 == 0) && !(n % 3 == 0 && n % 5 == 0))
                .ToList();

            return (evenNumbers, divisble);
        }
        public (bool FirstEventScheduled, bool SecondEventScheduled, bool Cancelled, List<Event> Events) RunTask2()
        {
            //Task2
            var time = DateTime.Now.AddHours(2);

            var first = _eventScheduler.ScheduleEvent(new Event
            {
                Name = "Test1",
                Location = "Room A",
                DateTime = time
            });

            var second = _eventScheduler.ScheduleEvent(new Event
            {
                Name = "Test2",
                Location = "Room B",
                DateTime = time
            });

            // Cancel test (separate event)
            _eventScheduler.ScheduleEvent(new Event
            {
                Name = "CancelTestEvent",
                Location = "Room C",
                DateTime = DateTime.Now.AddHours(3)
            });

            var cancel = _eventScheduler.CancelEvent("CancelTestEvent");

            var events = _eventScheduler.GetUpcomingEvents();
            return (first, second, cancel, events);

        }
    }
}
