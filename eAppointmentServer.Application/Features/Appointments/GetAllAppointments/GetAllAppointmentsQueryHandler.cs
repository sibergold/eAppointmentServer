using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments;

internal sealed class GetAllAppointmentsQueryHandler(
    IAppointmentRepository appointmentRepository
) : IRequestHandler<GetAllAppointmentsQuery, Result<List<Appointment>>>
{
    public async Task<Result<List<Appointment>>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        List<Appointment> appointments = await appointmentRepository
           .GetAll()
           .OrderBy(p => p.StartDate)
           .ThenBy(p => p.Doctor)
           .ToListAsync(cancellationToken);
        return appointments;
    }
}
