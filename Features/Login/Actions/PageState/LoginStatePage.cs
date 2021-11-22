using BlazorState;

namespace BzVault.Features.Login
{
    public partial class LoginState
    {
        public class LoginStatePage : IAction
        {
            public int RequestedPage { get; set; }
        }
        
    }
}
