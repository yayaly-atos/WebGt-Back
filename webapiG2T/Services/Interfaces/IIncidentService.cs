using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IIncidentService
    {
    
        Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId);
        Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber);
        Task<IncidentDto> GetIncidentByIDAsync(int incidentId);
        Task<IncidentDto> CreateIncidentAsync(CreateIncidentDtocs incidentDto);

    }
}
