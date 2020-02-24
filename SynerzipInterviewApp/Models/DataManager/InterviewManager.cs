using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SynerzipInterviewApp.Models.Repository;

namespace SynerzipInterviewApp.Models.DataManager
{
    public class InterviewManager : IInterviewRepository
    {

        readonly ApplicationContext _ctx;
        public InterviewManager(ApplicationContext c)
        {
            _ctx = c;
        }

        public Interview Add(Interview interview)
        {
            if (_ctx.Interviews.Any(o => o.CandidateName == interview.CandidateName && o.dateofInterview == interview.dateofInterview))
           {
                return new Interview();
            }
            _ctx.Interviews.Add(interview);
            _ctx.SaveChanges();
          return  interview;
        }

        public Interview Delete(long Id)
        {
            Interview interview = _ctx.Interviews.Find(Id);
            if (interview != null)
            {
                _ctx.Interviews.Remove(interview);
                _ctx.SaveChanges();
            }
            return interview;
        }

        public Interview Get(long Id)
        {
            return _ctx.Interviews.Find(Id);
        }

        public IEnumerable<Interview> GetAll()
        {
            return _ctx.Interviews.OrderByDescending(x => x.InterviewId).ToList();
        }

        public IEnumerable<Interview> GetInterviewByDate(DateTime date)
        {
            return _ctx.Interviews.ToList().Where(x => x.dateofInterview == date);
        }

        public Interview Update(Interview Updatedinterview, Interview item)
        {

            Updatedinterview.CandidateName = item.CandidateName;
            Updatedinterview.ContactPerson = item.ContactPerson;
            Updatedinterview.dateofInterview = item.dateofInterview;
            Updatedinterview.MobileNo = item.MobileNo;

            _ctx.SaveChanges();
            return Updatedinterview;
        }
    }
}
