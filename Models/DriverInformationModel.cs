namespace AAOAdmin.Models
{
    public class DriverInformationModel
    {
        public int DriverInformationId { get; set; }

        public int UserId { get; set; }

        public int LocationId { get; set; }

        int DriverLicenceId { get; set; }

        int LorryLicenceId { get; set; }

        int EUCertificateId { get; set; }
    }
}
