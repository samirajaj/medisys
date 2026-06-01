namespace Medisys.Models.Treatments;

public abstract class Treatment(int id, int patientId, DateTime treatmentDate, decimal cost)
{
    public int Id { get; set; } = id;

    public int PatientId { get; set; } = patientId;

    public DateTime TreatmentDate { get; set; } = treatmentDate;

    public decimal Cost { get; set; } = cost;

    public virtual void Display()
    {
        Console.WriteLine($"Treatment #{Id} | Cost: {Cost}");
    }
}
