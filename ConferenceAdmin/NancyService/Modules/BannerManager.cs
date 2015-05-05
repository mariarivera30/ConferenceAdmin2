using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class BannerManager
    {
        //By:Heidi Negron
        public BannerManager()
        {

        }

        //Get sponsor's logos by category.
        public BannerSponsorQuery getBannerList(String sponsor, int index)
        {
            BannerSponsorQuery page = new BannerSponsorQuery();

            try
            {
                int pageSize = 10;
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var query = context.sponsor2.Where(x => x.deleted != true && x.sponsortype1.name == sponsor && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor = x.user.affiliationName,
                        logo = x.logo

                    }).OrderBy(x => x.sponsor);

                    //Paging: Depending on the index value (which represents a page number) filters the results to the adequeate pageSize
                    page.rowCount = query.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var administrators = query.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = administrators;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("BannerManager.getBannerList error " + ex);
                return null;
            }
        }

    }

    public class BannerQuery
    {
        public String sponsor;
        public String logo;

        public BannerQuery()
        {

        }
    }

    public class BannerSponsorQuery
    {
        public int indexPage;
        public int maxIndex;
        public int rowCount;
        public List<BannerQuery> results;

        public BannerSponsorQuery()
        {
            results = new List<BannerQuery>();

        }
    }
}