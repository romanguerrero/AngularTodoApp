using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace todo
{
    public class UpdateTodoDTO
    {        
        public int id { get; set; }
        public string title { get; set; }
        public bool done { get; set; }
    }

    public class CreateTodoDTO
    {
        [Required]
        public string title { get; set; }
    }

    public class PatchTodoDTO
    {
        //* Default values indicate no change during PATCH
        public int id { get; set; } = -1;        
        public string title { get; set; } = "";
        public bool done { get; set; } = false;
    }
}