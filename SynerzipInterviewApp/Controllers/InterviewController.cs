using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SynerzipInterviewApp.Models;
using SynerzipInterviewApp.Models.Repository;
using SynerzipInterviewApp.Models.DataManager;
using Microsoft.AspNetCore.Authorization;




namespace SynerzipInterviewApp.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class InterviewController : Controller
    {

        private readonly IInterviewRepository _dataRepository;

        public InterviewController(IInterviewRepository dataRepository)
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
                IEnumerable<Interview> interviews = _dataRepository.GetAll();
                if (interviews == null)
                {
                    return NotFound();
                }
                return Ok(interviews);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

       
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                Interview interview = _dataRepository.Get(id);
                if (interview == null)
                {
                    return NotFound("The Interviews couldn't be found.");
                }
                return Ok(interview);
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
        public IActionResult Post([FromBody] Interview interview)
        {
            try
            {
                if (interview == null)
                {
                    return BadRequest();
                }

                _dataRepository.Add(interview);
                if (interview.InterviewId == 0)
                {
                    return StatusCode(StatusCodes.Status208AlreadyReported, "Already exist");
                }
                return CreatedAtRoute("Get", new { Id = interview.InterviewId }, interview);
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
        public IActionResult Put(long id, [FromBody] Interview interview)
        {
            try
            {

                if (interview == null)
                {
                    return BadRequest("Interview is null.");
                }

                Interview interviewToUpdate = _dataRepository.Get(id);
                if (interviewToUpdate == null)
                {
                    return NotFound("The Interview record couldn't be found.");
                }

                _dataRepository.Update(interviewToUpdate, interview);
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
                Interview interview = _dataRepository.Get(id);
                if (interview == null)
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


        [HttpGet("GetInterviewByDate/{date}", Name = "GetInterviewByDate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetInterviewByDate(DateTime date)
        {
            try
            {
                IEnumerable<Interview> interviews = _dataRepository.GetInterviewByDate(date);

                if (interviews == null)
                {
                    return NotFound("The Interviews couldn't be found.");
                }

                return Ok(interviews);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
           
        }

    }
}
