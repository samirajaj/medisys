namespace Medisys.Models.Doctors;

public class ContractDoctor(int id, string name, string address, DateTime birthDate) : Doctor(id, name, address, birthDate)
{
    public decimal TreatmentsRevenue { get; set; }

    public override decimal CalculateSalary()
    {
        return TreatmentsRevenue * 0.5m;
    }
}
