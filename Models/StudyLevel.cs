namespace InsaClub.Models
{
    public enum ESpeciality
    {
        GL,
        RT,
        IMI,
        IIA,
        CH,
        BIO,
        CBA,
        MPI,
        
    }
    public enum EStudyLevel
    {
        A1,
        A2,
        A3,
        A4,
        A5,
    }
    public class StudyLevel
    {
        public int Id { get; set; }
        public EStudyLevel Level { get; set; }
        public ESpeciality Speciality { get; set; }
    }
}