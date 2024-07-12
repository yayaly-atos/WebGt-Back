using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IIncidentService
    {
        Task<IEnumerable<Incident>> GetAllIncidentsAsync();
        Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId);
        Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber);
        Task<IncidentDto> GetIncidentByIDAsync(int incidentId);
    }
}
