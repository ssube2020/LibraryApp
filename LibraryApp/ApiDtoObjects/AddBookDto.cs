using System.ComponentModel.DataAnnotations;

namespace LibraryAppAPI.ApiDtoObjects
{
    public class AddBookDto
    {
        [Required]
        public int AuthorId;
        [Required]
        public string Header = "";
        [Required]
        public string Descricption = "";
    }
}
