using ErrorOr;
using MediatR;

namespace Ticky.Application.Common.Interfaces;

internal interface ITickyRequest<T> : IRequest<ErrorOr<T>>
{
}

internal interface ITickyRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<TResponse>>
    where TRequest : ITickyRequest<TResponse>
{
}
