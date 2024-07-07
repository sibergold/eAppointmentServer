using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using eAppointmentServer.Infrastructure.Context;
using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eAppointmentServer.Infrastructure.Repositories;
public sealed class AppointmentRepository : Repository<Appointment, ApplicationDbContext>, IAppointmentRepository
{
   
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
       
    }
    
}