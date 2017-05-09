using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Copyinfo.Database
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

        private string address_string { get; set; } = "";
        
        public Address()
        {
            street = "";
            city = "";
            postcode = "";
            post_city = "";
            house_number = "";
            apartment = "";
        }

        public override string ToString()
        {
            if (address_string.Length == 0)
                address_string = street + " " + house_number + (apartment.Length > 0 ? "\\" + apartment : "") + " " + city;
            return address_string;
        }

        public static bool operator ==(Address a, Address b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.id == b.id;
        }

        public void SearchInGoogleMaps()
        {
            Global.SearchThisStringInGoogleMaps(street + " " + house_number + ", " + city);
        }

        public static bool operator !=(Address a, Address b)
        {
            return !(a == b);
        }
    }
}
