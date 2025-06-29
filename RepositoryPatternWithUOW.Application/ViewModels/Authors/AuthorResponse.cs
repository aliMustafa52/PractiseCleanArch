using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Application.ViewModels.Authors
{
    public record AuthorResponse
    (
        int Id,
        string Name
    );
}
