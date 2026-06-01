using Medisys.Models.Doctors;

namespace Medisys.Models.Treatments;

public class ExternalTreatment(int id, int patientId, DateTime treatmentDate, decimal cost, int clinicNumber, Doctor treatingDoctor) : Treatment(id, patientId, treatmentDate, cost)
{
    public int ClinicNumber { get; set; } = clinicNumber;

    public Doctor TreatingDoctor { get; set; } = treatingDoctor;
}
