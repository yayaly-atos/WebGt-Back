using G2T.Models;
using G2T.Models.enums;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IIncidentService
    {
    
        Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId);
        Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber);
        Task<IncidentDto> GetIncidentByIDAsync(int incidentId);
        Task<IncidentDto> CreateIncidentAsync(CreateIncidentDtocs incidentDto,String id);
      
        Task<List<IncidentDto>> GetIncidentsByAgent(String idAgent);
        Task<List<IncidentDto>> GetIncidentsResoluByAgeNT(String idAgent);

        Task<List<IncidentDto>> GetIncidentsOuvertByAgeNT(String idAgent);

        Task<List<IncidentDto>> GetIncidentsNonOuvertByAgeNT(String idAgent);

        Task<Response> DemandeEscalade(int incidentID, String commentaire, String Id);
        Task<List<IncidentDto>> GetIncidentAllAsync();

    
        Task<List<IncidentDto>> GetIncidentsBySuperviseur(int EntiteId);
        Task<List<IncidentDto>> GetIncidentsResoluBySuperviseur(int EntiteId);

        Task<List<IncidentDto>> GetIncidentsOuvertBySuperviseur(int EntiteId);

        Task<List<IncidentDto>> GetIncidentsNonOuvertBySuperviseur(int EntiteId);
        Task<Response> TakeIncident(int incidentId, String idAgent);
          Task<Response> StartResolutionIncident(int incidentID);
        Task<Response> EscaladeIncident(int incidentI, String commentaire, String Id);
        Task<Response> EndResolutionIncident(int incidentID,String commentaire,String Id);

    }
}
