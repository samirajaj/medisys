using Medisys.Models.Treatments;

namespace Medisys.Models.Patients;

public class InPatient(int id, string name, string address, DateTime birthDate) : Patient(id, name, address, birthDate)
{
    public List<InternalTreatment> InternalTreatments { get; set; } = [];

    public List<ExternalTreatment> ExternalTreatments { get; set; } = [];

    public bool IsDischarged { get; set; }
}
