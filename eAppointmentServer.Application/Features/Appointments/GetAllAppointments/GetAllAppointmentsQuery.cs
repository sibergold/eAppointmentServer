using MediatR;
using TS.Result;
using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointments;

public sealed record GetAllAppointmentsQuery : IRequest<Result<List<Appointment>>>;
