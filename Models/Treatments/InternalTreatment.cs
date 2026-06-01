using Medisys.Models.Doctors;

namespace Medisys.Models.Treatments;

public class InternalTreatment(int id, int patientId, DateTime treatmentDate, decimal cost, int departmentId) : Treatment(id, patientId, treatmentDate, cost)
{
    public DateTime? DischargeDate { get; set; }

    public int DepartmentId { get; set; } = departmentId;

    public List<Doctor> Supervisors { get; set; } = [];
}
