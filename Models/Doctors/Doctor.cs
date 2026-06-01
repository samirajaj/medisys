using Medisys.Models.Common;
using System.Text.Json.Serialization;

namespace Medisys.Models.Doctors;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(StaffDoctor), "staff")]
[JsonDerivedType(typeof(ResidentDoctor), "resident")]
[JsonDerivedType(typeof(ContractDoctor), "contract")]
public abstract class Doctor(int id, string name, string address, DateTime birthDate) : IEntity
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public string Address { get; set; } = address;

    public DateTime BirthDate { get; set; } = birthDate;

    public abstract decimal CalculateSalary();

    public virtual void Display()
    {
        Console.WriteLine($"Id: {Id}, Name: {Name}");
    }
}
