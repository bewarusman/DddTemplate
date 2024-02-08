using Application.Common.Messaging;
using MediatR;

namespace Application.Users;

public class ChangePasswordCommand : BaseRequest<Result>
{

    public class Handler : IRequestHandler<ChangePasswordCommand, Result>
    {
        public Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}