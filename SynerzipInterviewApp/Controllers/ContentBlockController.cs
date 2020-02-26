using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynerzipInterviewApp.Models;
using SynerzipInterviewApp.Models.Repository;

namespace SynerzipInterviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentBlockController : ControllerBase
    {
        private readonly IContentBlockRepository _dataRepository;

        public ContentBlockController (IContentBlockRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ContentBlock> contentBlocks = _dataRepository.GetAll();
                if (contentBlocks == null)
                {
                    return NotFound();
                }
                return Ok(contentBlocks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet("{id}", Name = "GetContentBlock")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                ContentBlock  contentBlock = _dataRepository.Get(id);
                if (contentBlock == null)
                {
                    return NotFound("The Interviews couldn't be found.");
                }
                return Ok(contentBlock);
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] ContentBlock contentBlock)
        {
            try
            {
                if (contentBlock == null)
                {
                    return BadRequest();
                }

                _dataRepository.Add(contentBlock);
                if (contentBlock.ContentBlockId == 0)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, "Already exist");
                }
                return CreatedAtRoute("Get", new { Id = contentBlock.ContentBlockId }, contentBlock);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] ContentBlock contentBlock)
        {
            try
            {

                if (contentBlock == null)
                {
                    return BadRequest("Interview is null.");
                }

                _dataRepository.Update(contentBlock);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long id)
        {
            try
            {
                ContentBlock contentBlock = _dataRepository.Get(id);
                if (contentBlock == null)
                {
                    return NotFound("The Interview record couldn't be found.");
                }

                _dataRepository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

    }
}