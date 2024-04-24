using Template.Domain.Entities.Game;
using Template.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Web.Models.Results
{
    public class MatchProcessingErrorResult<T> : Result<T>
    {
        private T _match;
        public MatchProcessingErrorResult(T match)
        {
            _match = match;
        }
        public override ResultType ResultType => ResultType.NotProcessed;
        public override List<string> Errors => new List<string>();
        public override T Data => _match;
    }
}