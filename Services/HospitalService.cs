using Medisys.DataStructures;
using Medisys.Models.Doctors;
using Medisys.Models.Patients;
using Medisys.Models.Treatments;

namespace Medisys.Services;

public class HospitalService
{
    public DoctorLinkedList Doctors { get; } = new();

    public PatientLinkedList Patients { get; } = new();

    #region Doctors

    public void AddDoctor(Doctor doctor)
    {
        Doctors.InsertSorted(doctor);
    }

    public bool DeleteDoctor(int doctorId)
    {
        return Doctors.Delete(doctorId);
    }

    public int CountResidentDoctors()
    {
        return Doctors.CountResidents();
    }

    public Doctor? FindDoctor(int doctorId)
    {
        return Doctors.Find(doctorId);
    }

    public IEnumerable<Doctor> GetAllDoctors()
    {
        return Doctors.GetAll();
    }

    public bool PromoteResidentDoctor(int doctorId)
    {
        Doctor? doctor = Doctors.Find(doctorId);

        if (doctor is not ResidentDoctor resident)
            return false;

        StaffDoctor staffDoctor = new(
            resident.Id,
            resident.Name,
            resident.Address,
            resident.BirthDate,
            resident.StaffSalary,
            0);

        Doctors.Delete(doctorId);

        Doctors.InsertSorted(staffDoctor);

        return true;
    }

    #endregion

    #region Patients

    public IEnumerable<Patient> GetAllPatients()
    {
        return Patients.GetAll();
    }

    public Patient? FindPatient(int patientId)
    {
        return Patients.Find(patientId);
    }

    public void AddPatient(Patient patient)
    {
        Patients.InsertSorted(patient);
    }

    public bool AdmitPatient(int patientId)
    {
        Patient? patient = Patients.Find(patientId);

        if (patient is not OutPatient outPatient)
            return false;

        InPatient inPatient = new(
            outPatient.Id,
            outPatient.Name,
            outPatient.Address,
            outPatient.BirthDate);

        inPatient.ExternalTreatments.AddRange(outPatient.Treatments);

        Patients.Delete(patientId);

        Patients.InsertSorted(inPatient);

        return true;
    }

    public bool DischargePatient(int patientId)
    {
        Patient? patient = Patients.Find(patientId);

        if (patient is not InPatient inPatient)
            return false;

        inPatient.IsDischarged = true;

        return true;
    }

    #endregion

    public bool AddTreatment(int patientId, Treatment treatment)
    {
        Patient? patient = Patients.Find(patientId);

        if (patient is null)
            return false;

        switch (patient)
        {
            case OutPatient outPatient
                when treatment is ExternalTreatment externalTreatment:

                outPatient.Treatments.Add(externalTreatment);

                return true;

            case InPatient inPatient:

                if (treatment is ExternalTreatment ext)
                {
                    inPatient.ExternalTreatments.Add(ext);

                    return true;
                }

                if (treatment is InternalTreatment inter)
                {
                    inPatient.InternalTreatments.Add(inter);

                    return true;
                }

                break;
        }

        return false;
    }

    public IEnumerable<Treatment> GetPatientTreatments(int patientId)
    {
        Patient? patient = Patients.Find(patientId);

        if (patient is null)
            return [];

        if (patient is OutPatient outPatient)
        {
            return outPatient.Treatments;
        }

        if (patient is InPatient inPatient)
        {
            return inPatient.ExternalTreatments.Cast<Treatment>().Concat(inPatient.InternalTreatments);
        }

        return [];
    }


    public IEnumerable<(Patient Patient, InternalTreatment Treatment)>
    GetPatientsTreatedDuringPeriod(DateTime startDate, DateTime endDate)
    {
        List<(Patient, InternalTreatment)> result = [];

        foreach (Patient patient in Patients.GetAll())
        {
            if (patient is not InPatient inPatient)
                continue;

            foreach (InternalTreatment treatment in inPatient.InternalTreatments)
            {
                if (treatment.TreatmentDate >= startDate &&
                    treatment.TreatmentDate <= endDate)
                {
                    result.Add((patient, treatment));
                }
            }
        }

        return result;
    }

    public int CountPatientsInDepartment(int departmentId, DateTime startDate, DateTime endDate)
    {
        HashSet<int> patientIds = [];

        foreach (Patient patient in Patients.GetAll())
        {
            if (patient is not InPatient inPatient)
                continue;

            bool found = inPatient.InternalTreatments.Any(t => t.DepartmentId == departmentId && t.TreatmentDate >= startDate && t.TreatmentDate <= endDate);

            if (found)
            {
                patientIds.Add(patient.Id);
            }
        }

        return patientIds.Count;
    }

    public void SaveData()
    {
        FileService.SaveDoctors(Doctors.GetAll());

        FileService.SavePatients(Patients.GetAll());
    }

    public void LoadData()
    {
        foreach (Doctor doctor in FileService.LoadDoctors())
        {
            Doctors.InsertSorted(doctor);
        }

        foreach (Patient patient in FileService.LoadPatients())
        {
            Patients.InsertSorted(patient);
        }
    }
}
