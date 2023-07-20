using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.SharedCore7.Services
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
