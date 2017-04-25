using System.Linq;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Gdn.Authorization;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public class TokenAppService : AsyncCrudAppService<Token, TokenDto, int, GetAllTokensInput>, ITokenAppService
    {
        /// <inheritdoc />
        public TokenAppService(IRepository<Token> repository) : base(repository)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<Token> CreateFilteredQuery(GetAllTokensInput input)
        {
            return base.CreateFilteredQuery(input).WhereIf(input?.ClientId != null, t=>t.ClientId == input.ClientId);
        }
    }
}
