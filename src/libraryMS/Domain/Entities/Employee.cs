using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Employee : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string NationalIdentity { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public Guid UserId { get; set; }
    //public bool? EmployeeStatus { get; set; }  //
    public virtual User? User { get; set; }
    public Guid LibraryId { get; set; }
    public virtual Library? Library { get; set; }

    public Employee()
    {
        Name = string.Empty;
        Surname = string.Empty;
        NationalIdentity = string.Empty;
        PhoneNumber = string.Empty;
        Address = string.Empty;
        Position = string.Empty;
    }


}