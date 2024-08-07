using G2T.Models;
using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactDto> GetContactByPhoneNumberAsync(string phoneNumber);
        Task<ContactDto> GetContactByIDAsync(string id);

    }
}
