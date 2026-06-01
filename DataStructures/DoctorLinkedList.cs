using Medisys.Models.Doctors;

namespace Medisys.DataStructures;

public class DoctorLinkedList : SortedLinkedList<Doctor>
{
    public int CountResidents()
    {
        int count = 0;

        foreach (Doctor doctor in GetAll())
        {
            if (doctor is ResidentDoctor)
            {
                count++;
            }
        }

        return count;
    }
}
