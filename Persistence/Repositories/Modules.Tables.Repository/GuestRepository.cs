using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.Tables;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Tables.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly ApplicationContext _context;

        public GuestRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guest> AddNumberOfGuestAsync(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
           await _context.SaveChangesAsync();

           return guest;
        }


        public async Task<Guest> EditNumberOfGuestAsync(Guest guest)
        {
            _context.Guests.Update(guest);
            await _context.SaveChangesAsync();

            return guest;
        }

        public async Task<Guest> GetNumberOfGuestByIdAsync(Guid guestId)
        {
            var guest =  await _context.Guests
                .Where(b => b.Id == guestId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return guest;
        }
    }
}
