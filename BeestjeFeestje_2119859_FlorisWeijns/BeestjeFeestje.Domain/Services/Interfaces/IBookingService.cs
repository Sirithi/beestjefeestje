using BeestjeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingModel> Get(string id);
        Task<BookingModel> Add(BookingModel booking);
        Task<IEnumerable<BookingModel>> GetAll();
        Task<IEnumerable<BookingModel>> GetAllWithRelations();
        Task<bool> Delete(string id);
        Task<BookingModel> AddPlaceholder(BookingModel booking);
        Task<BookingModel> Update(BookingModel animal);
        Task<IEnumerable<BookingModel>> GetByUser(UserModel user);
        Task<IEnumerable<BookingModel>> GetByUserWithRelations(UserModel user);
        Task<IEnumerable<BookingModel>> GetByOwnerWithRelations(UserModel user);
        Task<BookingModel> GetWithRelations(string id);
    }
}
