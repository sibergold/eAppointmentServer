using eAppointmentServer.Application.Features.Appointments.CreateAppointment;
using eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById;
using eAppointmentServer.Application.Features.Appointments.GetAllAppointments;
using eAppointmentServer.Application.Features.Appointments.GetAllDoctorByDepartment;
using eAppointmentServer.Application.Features.Appointments.GetPatientByIdentityNumber;
using eAppointmentServer.Application.Features.Appointments.UpdateAppointment;
using eAppointmentServer.Infrastructure.Context;
using eAppointmentServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eAppointmentServer.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AppointmentsController : ApiController
    {
        private readonly new IMediator _mediator;
        private readonly ApplicationDbContext _context;

        public AppointmentsController(IMediator mediator, ApplicationDbContext context) : base(mediator)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

           
            var appointmentsWithDetails = appointments.Select(a => new
            {
                a.Id,
                a.DoctorId,
                Doctor = a.Doctor != null ? new
                {
                    a.Doctor.Id,
                    a.Doctor.FirstName,
                    a.Doctor.LastName,
                    Department = a.Doctor.Department.ToString() //
                } : null,
                a.PatientId,
                Patient = a.Patient != null ? new
                {
                    a.Patient.Id,
                    a.Patient.FullName
                } : null,
                a.StartDate,
                a.EndDate,
                a.IsCompleted
            }).ToList();

            return Ok(new { data = appointmentsWithDetails });
        }

        [HttpPost("GetAllDoctorsByDepartment")]
        public async Task<IActionResult> GetAllDoctorByDepartment([FromBody] GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("GetAllByDoctorId")]
        public async Task<IActionResult> GetAllByDoctorId([FromBody] GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("GetPatientByIdentityNumber")]
        public async Task<IActionResult> GetPatientByIdentityNumber([FromBody] GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("DeleteById")]
        public async Task<IActionResult> DeleteById([FromBody] DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
