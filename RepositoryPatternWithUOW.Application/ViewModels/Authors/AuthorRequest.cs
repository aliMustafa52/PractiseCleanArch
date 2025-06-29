using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Application.ViewModels.Authors
{
    public record AuthorRequest
    (
        string Name
    );
}
