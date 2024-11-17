using ShopTARge23.Core.Dto.ChuckNorrisDtos;


namespace ShopTARge23.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisResultDto> ChuckNorrisResult(ChuckNorrisResultDto dto);
    }
}
