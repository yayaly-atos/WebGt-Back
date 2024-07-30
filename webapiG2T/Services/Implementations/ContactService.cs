using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class ContactService : IContactService
    {

        private readonly DataContext _context;

        public ContactService(DataContext context)
        {
            _context = context;
        }

        public async Task<ContactDto> GetContactByPhoneNumberAsync(string phoneNumber)
        {
            var contact = await _context.Contacts
                .Include(c => c.Compte)
                .FirstOrDefaultAsync(c => c.Telephone == phoneNumber);

            if (contact == null)
            {
                return null;
            }

            return new ContactDto
            {
                Id = contact.Id,
                Nom = contact.Nom,
                Prenom = contact.Prenom,
                Telephone = contact.Telephone,
                Adresse = contact.Adresse,
                CompteId = contact.Compte.Id
            };
        }
    }
}
