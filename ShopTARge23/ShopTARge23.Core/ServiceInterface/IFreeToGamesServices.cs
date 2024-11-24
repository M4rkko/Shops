using ShopTARge23.Core.Dto;

namespace ShopTARge23.Core.ServiceInterface
{
    public interface IFreeToGamesServices
    {
        Task<List<FreeToGamesRootDto>> FreeToGameResult();
    }
}
