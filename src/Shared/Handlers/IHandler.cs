namespace CompanyRatingApi.Shared.Handlers;

public interface IHandler<T, U>
{
    public Task<U> Handle(T request, CancellationToken cancellationToken);
}