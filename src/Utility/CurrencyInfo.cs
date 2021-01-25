using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Models.Miscellaneous;

namespace Utility
{
    public static class CurrencyInfo
    {
        public static List<Currency> CurrencyList { get; private set; } = new();

        public static void GenerateAll()
        {
            CurrencyList = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => c.Name)
                .Distinct()
                .Select(c => new RegionInfo(c))
                .GroupBy(r => r.ISOCurrencySymbol)
                .Select(g => g.First())
                .Select(region => new Currency
                {
                    CurrencyEnglishName = region.CurrencyEnglishName,
                    ISOCurrencySymbol = region.ISOCurrencySymbol,
                    CurrencySymbol = region.CurrencySymbol
                })
                .OrderBy(c => c.CurrencyEnglishName)
                .Skip(1)
                .ToList();
        }
    }
}