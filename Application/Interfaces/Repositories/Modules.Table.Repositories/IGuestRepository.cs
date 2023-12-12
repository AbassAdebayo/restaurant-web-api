using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Table.Repositories
{
    public interface IGuestRepository
    {
        public Task<Guest> AddNumberOfGuestAsync(Guest guest);
        public Task<Guest> GetNumberOfGuestByIdAsync(Guid guestId);
        public Task<Guest> EditNumberOfGuestAsync(Guest guest);
        
    }
}
