using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTestMVC.Formaters;
using TechTestMVC.Models;

namespace TechTestMVC.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly PhoneNumberFormater _phoneNumberFormater;
        public IConfiguration _configuration { get; }
        public CustomerRepository(IConfiguration iconfiguration)
        {
            this._phoneNumberFormater = new PhoneNumberFormater();
            this._configuration = iconfiguration;

        }


        /// <summary>
        /// Gets  Customer Data 
        /// </summary>
        /// <returns>customer data</returns>
        public async Task<IEnumerable<Customer>> GetCustomerData()
        {
            try
            {
                //Hosted web API REST Service base url 
                 string baseURL = _configuration.GetSection("BaseUrl").Value;
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllCustomers using HttpClient
                    HttpResponseMessage Res = await client.GetAsync(baseURL);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var custResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        return JsonConvert.DeserializeObject<List<Customer>>(custResponse);
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex) { throw; }
        }
        /// <summary>
        /// Gets  Customer's FirstName By Age
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>comma seperated customer's first name</returns>

        public string GetCustomerFirstNameByAge(IEnumerable<Customer> customerInfo)
        {
            try
            {
                if (customerInfo != null)
                    return String.Join(",", customerInfo.Where(a => a.Age == 56).Select(a => a.FirstName).ToArray());
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets Valid Customer PhoneNumber with Id
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns>list of customer id with phone numbers</returns>
       
        public IEnumerable<Customer> GetCustomerIDWithPhoneNumbers(IEnumerable<Customer> customerInfo)
        {
            try
            {
                bool isValid = false;
                if (customerInfo != null)
                {
                    return customerInfo.Select(a => new Customer()
                    {
                        Id = a.Id,
                        PhoneNumber = _phoneNumberFormater.isValidMobileNumber(a.PhoneNumber, a.State, out isValid),
                        IsValid = isValid,
                        State = a.State
                    }).ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets Valid Customer PhoneNumber By State
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns>list of phone numbers group by state</returns>
   
        public IEnumerable<CustomerPhoneNumber> GetValidPhoneNumbersByState(IEnumerable<Customer> customerDetails)
        {
            try
            {
                if (customerDetails != null)
                {
                    var ValidCustomerDetailsBystate = customerDetails.Where(a => a.IsValid == true).GroupBy(a => a.State).OrderBy(a => a.Key);
                    return ValidCustomerDetailsBystate.Select(a => new CustomerPhoneNumber()
                    {
                        PhoneNumber = a.Key + ": " + a.Count(),
                    }).ToList();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
