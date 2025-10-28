namespace CompanyRatingApi.Shared.Handlers;

public interface IHandler<in TReq, TRes>
{
    public Task<TRes> Handle(TReq request, CancellationToken cancellationToken);
}