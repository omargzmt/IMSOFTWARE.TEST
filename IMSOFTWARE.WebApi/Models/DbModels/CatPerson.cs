using System;
using System.Collections.Generic;

namespace IMSOFTWARE.WebApi.Models.DbModels
{
    public partial class CatPerson
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
    }
}
