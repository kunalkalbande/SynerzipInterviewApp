using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models.Repository
{
    public interface IContentBlockRepository
    {
        ContentBlock Get(long Id);
        IEnumerable<ContentBlock> GetAll();
        ContentBlock Add(ContentBlock ContentBlock);
        ContentBlock Update(ContentBlock ContentBlockToUpdate);
        ContentBlock Delete(long Id);
    }
}
