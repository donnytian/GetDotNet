﻿using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Gdn.Users;

namespace Gdn.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }
    }
}
