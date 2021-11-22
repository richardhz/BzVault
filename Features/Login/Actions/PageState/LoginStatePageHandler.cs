using BlazorState;
using BzVault.Models;
using BzVault.Services.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BzVault.Features.Login
{
    public partial class LoginState
    {
        

        public class LoginStatePageHandler : ActionHandler<LoginStatePage>
        {
            private readonly IApiClient _client;
            public LoginStatePageHandler(IStore store, IApiClient client)
                :base(store)
            {
                _client = client;
            }

            private LoginState LoginState => Store.GetState<LoginState>();

            public override async Task<Unit> Handle(LoginStatePage aAction, CancellationToken aCancellationToken)
            {
                var data = await _client.GetAsync<LoginListMeta, string>($"list?pageNumber={aAction.RequestedPage} &OrderBy=name");

                LoginState.LoginData = data;

                return Unit.Value;
            }
        }
    }
}
