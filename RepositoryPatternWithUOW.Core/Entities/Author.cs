using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Domain.Entities
{
    public class Author : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; } = [];
    }
}
