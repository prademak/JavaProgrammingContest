using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Domain.Entities;
using JavaProgrammingContest.Web.DTO;
using WebMatrix.WebData;

namespace JavaProgrammingContest.Web.API{
    /// <summary>
    ///     Controller for interface interaction with the database object Assignments.
    /// </summary>
    public class AssignmentsController : ApiController{
        /// <summary>
        ///     Stores the Database Context
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        ///     API Interface Assignment Controller Constructor
        /// </summary>
        private readonly Participant _participant;

        public AssignmentsController(IDbContext context, Participant participant = null){
            _context = context;
            _participant = participant ?? GetCurrentParticipant();
        }

        /// <summary>
        ///     Get all current assignments for this user and the currently active contest.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AssignmentDTO> Get(){
            var assignmentDtos = new List<AssignmentDTO>();

            foreach (var assignment in _context.Assignments){
                var assignmentDto = Mapper.Map<Assignment, AssignmentDTO>(assignment);

                if (_participant.Scores != null)
                    foreach (var score in _participant.Scores)
                        if (score.Assignment.Id == assignment.Id)
                            assignmentDto.HasBeenSubmitted = true;

                assignmentDtos.Add(assignmentDto);
            }

            return assignmentDtos;
        }

        /// <summary>
        ///     Get all the info of the given Assignment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssignmentDTO Get(int id){
            var assignment = _context.Assignments.Find(id);

            if (assignment == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Assignment, AssignmentDTO>(assignment);
        }

        /// <summary>
        ///     Add an assignment to the database (REDUNDANT)
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(Assignment assignment){
            if (!ModelState.IsValid || assignment == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            if (_context.Assignments == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            try{
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
            } catch (Exception){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        ///     Delete an Assignment (REDUNDANT)
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrator")]
        public void Delete(int id){
            try{
                var assignment = _context.Assignments.Find(id);
                if (assignment == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                _context.Assignments.Remove(assignment);
            } catch (SqlException){
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [ExcludeFromCodeCoverage]
        private Participant GetCurrentParticipant(){
            var participant = _context.Participants.Find(WebSecurity.GetUserId(User.Identity.Name));
            return participant;
        }
    }
}