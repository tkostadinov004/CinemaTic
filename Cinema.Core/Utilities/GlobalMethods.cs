using Cinema.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Core.Utilities
{
    public static class GlobalMethods
    {
        public static IEnumerable<SelectListItem> GetCountries()
        {
            var countries = new List<string>();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.LCID);
                if (!countries.Contains(region.EnglishName))
                {
                    countries.Add(region.EnglishName);
                }
            }

            return countries.OrderBy(i => i).Select(i => new SelectListItem(i, i));
        }
    }
}
