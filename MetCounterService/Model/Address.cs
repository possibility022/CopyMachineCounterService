using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Address
    {
        public string street { get; set; } //ulica
        public string city { get; set; } //miejscowosc
        public string postcode { get; set; } //kod pocztowy
        public string post_city { get; set; } //miejscowosc poczty
        public string house_number { get; set; }
        public string apartment { get; set; }
        public int id { get; set; } // _id
    }
}
