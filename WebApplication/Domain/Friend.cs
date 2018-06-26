using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Domain
{
    public class Friend
    {
        public int FriendId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Greeting()
        {
            return $"Hello, my name is {FirstName}";
        }
    }
}