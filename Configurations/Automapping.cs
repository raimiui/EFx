using AutoMapper;

namespace EFx.Configurations
{
    public static class Automapping
    {
        public static void InitializeMapings()
        {
            Mapper.CreateMap<Dal.MarketInfoService.Quote, Model.Quote>();
        }
    }
}
