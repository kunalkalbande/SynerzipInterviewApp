using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SynerzipInterviewApp.Models.Repository;
namespace SynerzipInterviewApp.Models.DataManager
{
    public class ContentBlockManager : IContentBlockRepository
    {
        readonly ApplicationContext _ctx;
        public ContentBlockManager(ApplicationContext c)
        {
            _ctx = c;
        }
        public ContentBlock Add(ContentBlock ContentBlock)
        {
            _ctx.ContentBlocks.Add(ContentBlock);
            _ctx.SaveChanges();
            return ContentBlock;
        }

        public ContentBlock Delete(long Id)
        {
            var block = _ctx.ContentBlocks.Where(cb => cb.ContentBlockId == Id).FirstOrDefault();
            if (block != null)
            {
                _ctx.ContentBlocks.Remove(block);
                _ctx.SaveChanges();
            }
            return block;
        }

        public ContentBlock Get(long Id)
        {
           return _ctx.ContentBlocks.Where(cb => cb.ContentBlockId == Id).FirstOrDefault();
        }

        public IEnumerable<ContentBlock> GetAll()
        {
            return _ctx.ContentBlocks.ToList();

        }

        public ContentBlock Update(ContentBlock ContentBlockToUpdate)
        {
            var block = _ctx.ContentBlocks.Where(cb => cb.ContentBlockId == ContentBlockToUpdate.ContentBlockId).FirstOrDefault();
            if (block != null)
            {
                block.Name = ContentBlockToUpdate.Name;
                block.Query = ContentBlockToUpdate.Query;
                block.VisualizationType = ContentBlockToUpdate.VisualizationType;
                _ctx.ContentBlocks.Update(block);
                _ctx.SaveChanges();
            }
            return ContentBlockToUpdate;
        }
    }
}
