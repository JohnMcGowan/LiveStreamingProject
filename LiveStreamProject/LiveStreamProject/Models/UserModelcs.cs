using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LiveStreamProject.Models
{
    public class UserModelcs
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "E-Mail")]
        public String Email { get; set; }

        [Required]
        [StringLength(20,MinimumLength = 8)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public enum UserTypes { Admin, User };

        [Required]
        [EnumDataType(typeof(UserTypes))]
        public UserTypes UserType { get; set; }
    }

    public class UserProfile
    {
        [StringLength(50)]
        [Display(Name = "Frist Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string SurName { get; set; }

        [StringLength(20)]
        [Display(Name = "House Name/Number")]
        public string HouseNameNumber { get; set; }

        [StringLength(100)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [StringLength(100)]
        [Display(Name = "Town/City")]
        public string TownCity { get; set; }

        public enum Countys { Antrim, Armagh,Carlow,Cavan,Clare,Cork,Derry,Donegal,Down,Dublin,
            Fermanagh,Galway, Kerry,Kildare,Kilkenny,Laois,Leitrim, Limerick, Longford, Louth,
            Mayo, Meath, Monaghan, Offaly, Roscommon, Sligo, Tipperary, Tyrone, Waterford,
            Westmeath, Wexford, Wicklow }

        [StringLength(50)]
        [Display(Name = "County")]
        [EnumDataType(typeof(Countys))]
        public Countys County { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
    }

    public class test
    {
        public string Test { get; set; }
    }

}