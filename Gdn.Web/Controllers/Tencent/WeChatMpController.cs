using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using Gdn.Authorization;
using Gdn.WeChat;
using Gdn.WeChat.Dto;
using WeChat.Core;

namespace Gdn.Web.Controllers.Tencent
{
    public static class MpParam
    {
        public static readonly string AppId = WebConfigurationManager.AppSettings["WeChat.MP.AppId"];
        public static readonly string AppSecret = WebConfigurationManager.AppSettings["WeChat.MP.AppSecret"];
        public static readonly string Token = WebConfigurationManager.AppSettings["WeChat.MP.Token"];
    }

    [AbpMvcAuthorize(PermissionNames.Pages)]
    public class WeChatMpController : GdnControllerBase
    {
        // Test account.
        private const string ClientId = "wxe5451a44e4833131";
        private const string ClientSecret = "8f60df3674781095f3f64c6e40158ce0";

        private static readonly TokenClient TokenClient =
            new TokenClient("DonnyTestAccount", ClientId, ClientSecret);

        private static TokenResponse _token;

        private readonly ITokenAppService _tokenAppService;

        public WeChatMpController(ITokenAppService tokenAppService)
        {
            _tokenAppService = tokenAppService;
        }

        //
        // Receives request from WeChat server to verify URL set in WeChat MP site.
        //
        [HttpGet]
        [ActionName("Index")]
        [AllowAnonymous]
        public ActionResult Index(WeChatSignatureModel model)
        {
            if (model != null && model.Verify(MpParam.Token))
            {
                return Content(model.EchoStr); // Returns the echo string to indicate a successful validation.
            }

            return Content("failed! signature:" + model?.Signature + ", computed hash:"
                           + model?.ComputeHash(MpParam.Token) + "。" +
                           "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
        }

        [HttpGet]
        [ActionName("Token")]
        public async Task<ActionResult> ViewToken()
        {
            if (_token == null)
            {
                var tokens = await _tokenAppService.GetAll(new GetAllTokensInput { ClientId = ClientId });
                if (tokens.Items.Count > 0)
                {
                    _token = tokens.Items[0].MapTo<TokenResponse>();
                }
            }

            return View(_token);
        }

        [HttpGet]
        [ActionName("NewToken")]
        public async Task<ActionResult> NewToken()
        {
            _token = await TokenClient.RequestAsync();

            var dto = _token.MapTo<TokenDto>();
            var existed = await _tokenAppService.GetAll(new GetAllTokensInput { ClientId = ClientId });

            if (existed.TotalCount > 0)
            {
                dto.Id = existed.Items[0].Id;
                await _tokenAppService.Update(dto);
            }
            else
            {
                await _tokenAppService.Create(dto);
            }

            return View("Token", _token);
        }
    }
}