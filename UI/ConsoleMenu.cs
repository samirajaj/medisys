using Medisys.Models.Doctors;
using Medisys.Models.Patients;
using Medisys.Services;

namespace Medisys.UI;

public class ConsoleMenu(HospitalService hospitalService)
{
    private readonly HospitalService _hospitalService = hospitalService;

    public void Run()
    {
        bool running = true;

        while (running)
        {

            var choice = Menu.Show(
                title: "MEDISYS",
                options:
                [
                    "Doctors",
                    "Patients",
                    "Treatments",
                    "Reports",
                    "Exit"
                ]);

            switch (choice)
            {
                case 1:
                    ShowDoctorsMenu();
                    break;

                case 2:
                    ShowPatientsMenu();
                    break;

                case 3:
                    ShowTreatmentsMenu();
                    break;

                case 4:
                    ShowReportsMenu();
                    break;

                default:
                    SaveData();
                    running = false;
                    break;
            }
        }

        Environment.Exit(0);
    }

    private void ShowDoctorsMenu()
    {
        bool back = false;

        while (!back)
        {
            var choice = Menu.Show(
                title: "DOCTORS",
                options:
                [
                    "Add Doctor",
                    "Delete Doctor",
                    "Promote Resident Doctor",
                    "Count Resident Doctors",
                    "Display All Doctors",
                    "Back"
                ]);

            switch (choice)
            {
                case 1:
                    AddDoctor();
                    break;

                case 2:
                    DeleteDoctor();
                    break;

                case 3:
                    PromoteResidentDoctor();
                    break;

                case 4:
                    CountResidentDoctors();
                    break;

                case 5:
                    DisplayAllDoctors();
                    break;

                default:
                    back = true;
                    break;
            }
        }
    }

    private void ShowPatientsMenu()
    {
        bool back = false;

        while (!back)
        {
            int choice = Menu.Show(
                title: "PATIENTS",
                options:
                [
                    "Add Patient",
                    "Admit Patient",
                    "Discharge Patient",
                    "Display All Patients",
                    "Back"
                ]);

            switch (choice)
            {
                case 1:
                    AddPatient();
                    break;

                case 2:
                    AdmitPatient();
                    break;

                case 3:
                    DischargePatient();
                    break;

                case 4:
                    DisplayAllPatients();
                    break;

                default:
                    back = true;
                    break;
            }
        }
    }

    private void DischargePatient()
    {
        Console.Clear();

        Console.Write("Patient Id: ");

        if (!int.TryParse(Console.ReadLine(), out int patientId))
        {
            Console.WriteLine("Invalid Id.");
            Pause();
            return;
        }

        bool discharged =
            _hospitalService.DischargePatient(patientId);

        Console.WriteLine(
            discharged
            ? "Patient discharged successfully."
            : "Patient not found or not admitted.");

        Pause();
    }

    private void AdmitPatient()
    {
        Console.Clear();

        Console.Write("Patient Id: ");

        if (!int.TryParse(Console.ReadLine(), out int patientId))
        {
            Console.WriteLine("Invalid Id.");
            Pause();
            return;
        }

        bool admitted =
            _hospitalService.AdmitPatient(patientId);

        Console.WriteLine(
            admitted
            ? "Patient admitted successfully."
            : "Patient not found or already admitted.");

        Pause();
    }

    private void DisplayAllPatients()
    {
        Console.Clear();

        Console.WriteLine("ALL PATIENTS");
        Console.WriteLine();

        foreach (var patient in _hospitalService.GetAllPatients())
        {
            patient.Display();
        }

        Pause();
    }

    private void AddPatient()
    {
        int choice = Menu.Show(
            title: "PATIENT TYPE",
            options:
            [
                "In Patient",
                "Out Patient",
                "Back"
            ]);

        switch (choice)
        {
            case 1:
                AddInPatient();
                break;

            case 2:
                AddOutPatient();
                break;
        }

        void AddInPatient()
        {
            var (Id, Name, Address, BirthDate) = ReadPatientData();

            InPatient patient = new(
                Id,
                Name,
                Address,
                BirthDate);

            _hospitalService.AddPatient(patient);

            Console.WriteLine("Patient added successfully.");

            Pause();
        }

        void AddOutPatient()
        {
            var (Id, Name, Address, BirthDate) = ReadPatientData();

            OutPatient patient = new(Id, Name, Address, BirthDate);

            _hospitalService.AddPatient(patient);

            Console.WriteLine("Patient added successfully.");

            Pause();
        }
    }

    private static (int Id, string Name, string Address, DateTime BirthDate) ReadPatientData()
    {
        Console.Clear();

        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Name: ");
        string name = Console.ReadLine()!;

        Console.Write("Address: ");
        string address = Console.ReadLine()!;

        Console.Write("Birth Date (yyyy-MM-dd): ");
        DateTime birthDate =
            DateTime.Parse(Console.ReadLine()!);

        return (id, name, address, birthDate);
    }

    private void ShowTreatmentsMenu()
    {
    }

    private void ShowReportsMenu()
    {
    }

    private void SaveData()
    {
        _hospitalService.SaveData();

        Console.WriteLine("\nData saved successfully.");
        Pause();
    }

    private static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void DisplayAllDoctors()
    {
        Console.Clear();

        Console.WriteLine("ALL DOCTORS");
        Console.WriteLine();

        foreach (Doctor doctor in _hospitalService.GetAllDoctors())
        {
            doctor.Display();
        }

        Pause();
    }

    private void AddDoctor()
    {
        int choice = Menu.Show(
            title: "DOCTOR TYPE",
            options:
            [
                "Staff Doctor",
                "Resident Doctor",
                "Contract Doctor",
                "Back"
            ]);

        switch (choice)
        {
            case 1:
                AddStaffDoctor();
                break;

            case 2:
                AddResidentDoctor();
                break;

            case 3:
                AddContractDoctor();
                break;
        }

        void AddContractDoctor()
        {
            var (Id, Name, Address, BirthDate) = ReadDoctorData();

            ContractDoctor doctor = new(Id, Name, Address, BirthDate);

            _hospitalService.AddDoctor(doctor);

            Console.WriteLine("Doctor added successfully.");

            Pause();
        }

        void AddResidentDoctor()
        {
            var (Id, Name, Address, BirthDate) = ReadDoctorData();

            Console.Write("Training Year (1 or 2): ");
            int trainingYear = int.Parse(Console.ReadLine()!);

            Console.Write("Staff Salary Reference: ");
            decimal staffSalary = decimal.Parse(Console.ReadLine()!);

            ResidentDoctor doctor = new(Id, Name, Address, BirthDate, DateTime.Now, trainingYear, staffSalary);

            _hospitalService.AddDoctor(doctor);

            Console.WriteLine("Doctor added successfully.");

            Pause();
        }

        void AddStaffDoctor()
        {
            var (Id, Name, Address, BirthDate) = ReadDoctorData();

            Console.Write("Monthly Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine()!);

            Console.Write("Years Of Service: ");
            int years = int.Parse(Console.ReadLine()!);

            StaffDoctor doctor = new(Id, Name, Address, BirthDate, salary, years);

            _hospitalService.AddDoctor(doctor);

            Console.WriteLine("Doctor added successfully.");

            Pause();
        }
    }

    private static (int Id, string Name, string Address, DateTime BirthDate) ReadDoctorData()
    {
        Console.Clear();

        Console.Write("Id: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Name: ");
        string name = Console.ReadLine()!;

        Console.Write("Address: ");
        string address = Console.ReadLine()!;

        Console.Write("Birth Date (yyyy-MM-dd): ");
        DateTime birthDate = DateTime.Parse(Console.ReadLine()!);

        return (id, name, address, birthDate);
    }

    private void PromoteResidentDoctor()
    {
        Console.Clear();

        Console.Write("Doctor Id: ");

        if (!int.TryParse(Console.ReadLine(), out int doctorId))
        {
            Console.WriteLine("Invalid Id.");
            Pause();
            return;
        }

        bool promoted = _hospitalService.PromoteResidentDoctor(doctorId);

        Console.WriteLine(
            promoted
            ? "Doctor promoted successfully."
            : "Resident doctor not found.");

        Pause();
    }

    private void DeleteDoctor()
    {
        Console.Clear();

        Console.Write("Doctor Id: ");

        if (!int.TryParse(Console.ReadLine(), out int doctorId))
        {
            Console.WriteLine("Invalid Id.");
            Pause();
            return;
        }

        bool deleted = _hospitalService.DeleteDoctor(doctorId);

        Console.WriteLine(
            deleted
                ? "Doctor deleted successfully."
                : "Doctor not found.");

        Pause();
    }

    private void CountResidentDoctors()
    {
        Console.Clear();

        int count = _hospitalService.CountResidentDoctors();

        Console.WriteLine($"Resident doctors count: {count}");

        Pause();
    }
}