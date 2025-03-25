using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SunriseShelter.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SunriseShelterUser class
public class SunriseShelterUser : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }
}

