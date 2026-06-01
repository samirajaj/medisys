namespace Medisys.Models.Doctors;

public class StaffDoctor(int id, string name, string address, DateTime birthDate, decimal monthlySalary, int yearsOfService) : Doctor(id, name, address, birthDate)
{
    public decimal MonthlySalary { get; set; } = monthlySalary;

    public int YearsOfService { get; set; } = yearsOfService;

    public override decimal CalculateSalary()
    {
        int periods = YearsOfService / 2;

        return MonthlySalary * (1 + periods * 0.10m);
    }
}
