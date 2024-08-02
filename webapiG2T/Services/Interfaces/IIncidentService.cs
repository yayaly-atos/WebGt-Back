using G2T.Models;
using G2T.Models.enums;
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
        Task<IncidentDto> UpdateIncident(int incidentId, CreateIncidentDtocs incidentDto);
        Task<List<IncidentDto>> GetIncidentsByAgent(String idAgent);
        Task<List<IncidentDto>> GetIncidentsResoluByAgeNT(String idAgent);

        Task<List<IncidentDto>> GetIncidentsOuvertByAgeNT(String idAgent);

        Task<List<IncidentDto>> GetIncidentsNonOuvertByAgeNT(String idAgent);

        Task<Incident> DemandeEscalade(int incidentId);
        Task<List<IncidentDto>> GetIncidentAllAsync();
    }
}
