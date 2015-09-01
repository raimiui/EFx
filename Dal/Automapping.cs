using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using EFx.Model;

namespace EFx.Dal
{
    public class Automapping
    {
        public static void Configure()
        {
            Mapper.CreateMap<MarketInfoService.HistoricalQuote, HistoricalQuote>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.createUTC))
                ;
            Mapper.CreateMap<MarketInfoService.RateTypeIds, RateType>();

            Mapper.CreateMap<TradingService.TradableQuote, TradableQuote>();
            Mapper.CreateMap<TradableQuote, TradingService.TradableQuote>();

            //Mapper.AssertConfigurationIsValid();
        }
    }
        

}
