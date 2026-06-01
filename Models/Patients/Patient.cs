using Medisys.Models.Common;
using System.Text.Json.Serialization;

namespace Medisys.Models.Patients;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(InPatient), "in")]
[JsonDerivedType(typeof(OutPatient), "out")]
public abstract class Patient(int id, string name, string address, DateTime birthDate) : IEntity
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public string Address { get; set; } = address;

    public DateTime BirthDate { get; set; } = birthDate;

    public virtual void Display()
    {
        Console.WriteLine($"Patient #{Id} - {Name}");
    }
}