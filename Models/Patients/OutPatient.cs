using Medisys.Models.Treatments;

namespace Medisys.Models.Patients;

public class OutPatient(int id, string name, string address, DateTime birthDate) : Patient(id, name, address, birthDate)
{
    public List<ExternalTreatment> Treatments { get; set; } = [];

    public bool IsAccepted { get; set; }
}
