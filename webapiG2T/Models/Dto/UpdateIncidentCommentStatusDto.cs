using G2T.Models.enums;

namespace webapiG2T.Models.Dto
{
    public class UpdateIncidentCommentStatusDto
    {
        public string NewComment { get; set; }
        public String NewStatus { get; set; }
    }
}
