namespace Excelsior.Business.DtoEntities
{
    public class BaseRequestDto
    {
        public string UserId { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public string Search { get; set; } 
    }
}
