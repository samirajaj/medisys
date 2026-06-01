namespace Medisys.Models.Doctors;

public class ResidentDoctor(int id, string name, string address, DateTime birthDate, DateTime trainingStartDate, int trainingYear, decimal staffSalary) : Doctor(id, name, address, birthDate)
{
    public DateTime TrainingStartDate { get; set; } = trainingStartDate;

    public DateTime? TrainingEndDate { get; set; }

    public int TrainingYear { get; set; } = trainingYear;

    public decimal StaffSalary { get; set; } = staffSalary;

    public override decimal CalculateSalary()
    {
        return TrainingYear switch
        {
            1 => StaffSalary * 0.5m,
            2 => StaffSalary * 0.75m,
            _ => StaffSalary
        };
    }
}
