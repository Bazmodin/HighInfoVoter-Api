using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class MemberAddRequest
    {
        [Required]
        [MaxLength(64)]
        [Display(Name = "Member ID")]
        public string MemberId { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "API URI")]
        public string ApiUri { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(128)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "District")]
        public string District { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "Party")]
        public string Party { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "Next Election")]
        public string NextElection { get; set; }

        [MaxLength(128)]
        [Display(Name = "FacebookAccount")]
        public string FacebookAccount { get; set; }

        [MaxLength(64)]
        [Display(Name = "TwitterId")]
        public string TwitterId { get; set; }

        [MaxLength(64)]
        [Display(Name = "YoutubeId")]
        public string YoutubeId { get; set; }

        [MaxLength(256)]
        [Display(Name = "Portrait URL")]
        public string PortraitUrl { get; set; }
    }
}
