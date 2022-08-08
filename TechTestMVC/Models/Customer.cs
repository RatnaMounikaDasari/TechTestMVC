using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestMVC.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public bool IsValid { get; set; }

    }
    public class CustomerPhoneNumber
    {
        public string PhoneNumber { get; set; }
    }
}
