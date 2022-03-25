using API.Models;
using System;

namespace API.ViewModel
{
    public class GetAllData
    {
        public string NIK { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Education_name { get; set; }
        public string GPA { get; set; }
        public string Degree { get; set; }
    }
}
