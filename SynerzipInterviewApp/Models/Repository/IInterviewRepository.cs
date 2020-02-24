using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models.Repository
{
    public interface IInterviewRepository
    {
        Interview Get(long Id);
        IEnumerable<Interview> GetAll();
        Interview Add(Interview interview);
        Interview Update(Interview interviewToUpdate, Interview interview);
        Interview Delete(long Id);
        IEnumerable<Interview> GetInterviewByDate(DateTime date);  
    }
}
