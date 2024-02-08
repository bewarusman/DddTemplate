using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Users;

public class LoginUserCommand : BaseRequest<Result>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<LoginUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindOne(request.Username);
            if (user == null)
                throw new LoginException("Wrong username or password!");

            //check hash with password
            
            return Result.Success(user);
        }
    }
}