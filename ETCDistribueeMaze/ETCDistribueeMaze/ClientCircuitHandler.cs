using ETCDistribueeMaze.Providers;
using ETCDistribueeMaze.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace ETCDistribueeMaze
{
    public class ClientCircuitHandler : CircuitHandler
    {
        private IConnectedClientService _clientService;
        private IUserStateProvider _usersProvider;
        private IMazeService _mazeService;

        public ClientCircuitHandler(IConnectedClientService clientService, IMazeService mazeService, IUserStateProvider usersProvider)
        {
            _clientService = clientService;
            _mazeService = mazeService;
            _usersProvider = usersProvider;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _clientService.Connect(circuit.Id);
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            var user = _usersProvider.GetByClient(_clientService.Client);
            if (null != user)
                _mazeService.Logout(user.Username);

            _clientService.Disconnect();

            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }
}



