using Medisys.Models.Doctors;
using Medisys.Models.Patients;
using System.Text.Json;

namespace Medisys.Services;

public static class FileService
{
    private static readonly string DataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "medisys");

    private static readonly string DoctorsFile = Path.Combine(DataDirectory, "doctors.json");

    private static readonly string PatientsFile = Path.Combine(DataDirectory, "patients.json");

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    static FileService()
    {
        Directory.CreateDirectory(DataDirectory);
    }

    public static void SaveDoctors(IEnumerable<Doctor> doctors)
    {
        string json = JsonSerializer.Serialize(doctors, JsonOptions);

        File.WriteAllText(DoctorsFile, json);
    }

    public static void SavePatients(IEnumerable<Patient> patients)
    {
        string json = JsonSerializer.Serialize(patients, JsonOptions);

        File.WriteAllText(PatientsFile, json);
    }

    public static List<Doctor> LoadDoctors()
    {
        if (!File.Exists(DoctorsFile))
        {
            return [];
        }

        string json = File.ReadAllText(DoctorsFile);

        return JsonSerializer.Deserialize<List<Doctor>>(json, JsonOptions) ?? [];
    }

    public static List<Patient> LoadPatients()
    {
        if (!File.Exists(PatientsFile))
        {
            return [];
        }

        string json = File.ReadAllText(PatientsFile);
        return JsonSerializer.Deserialize<List<Patient>>(json, JsonOptions) ?? [];
    }
}
