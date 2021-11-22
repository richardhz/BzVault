using BlazorState;
using BzVault.Models;

namespace BzVault.Features.Login
{
    public partial class LoginState : State<LoginState>
    {
        public LoginListMeta LoginData { get; private set; }
        public override void Initialize() => LoginData = new LoginListMeta();
    }
}
