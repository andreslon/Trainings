﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Responses
{
    public class UserLoginResponseDto 
    {
        public object UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
