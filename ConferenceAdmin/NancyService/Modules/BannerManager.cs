using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class BannerManager
    {
        public BannerManager()
        {

        }

        public BannerSponsorQuery getBannerList()
        {
            BannerSponsorQuery banners = new BannerSponsorQuery();

            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    banners.diamond = context.sponsors.Where(x => x.deleted != true && x.sponsortype1.name == "Diamond" && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor = x.company,
                        logo = x.logo

                    }).ToList();

                    banners.platinum = context.sponsors.Where(x => x.deleted != true && x.sponsortype1.name == "Platinum" && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor= x.company,
                        logo=x.logo

                    }).ToList();

                    banners.gold = context.sponsors.Where(x => x.deleted != true && x.sponsortype1.name == "Gold" && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor = x.company,
                        logo = x.logo

                    }).ToList();

                    banners.silver = context.sponsors.Where(x => x.deleted != true && x.sponsortype1.name == "Silver" && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor = x.company,
                        logo = x.logo

                    }).ToList();

                    banners.bronze = context.sponsors.Where(x => x.deleted != true && x.sponsortype1.name == "Bronze" && x.logo != "" && x.logo != null && x.sponsorID != 1).Select(x => new BannerQuery
                    {
                        sponsor = x.company,
                        logo = x.logo

                    }).ToList();

                    return banners;
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
        public List<BannerQuery> diamond;
        public List<BannerQuery> platinum;
        public List<BannerQuery> gold;
        public List<BannerQuery> silver;
        public List<BannerQuery> bronze;

        public BannerSponsorQuery()
        {
            diamond = new List<BannerQuery>();
            platinum = new List<BannerQuery>();
            gold = new List<BannerQuery>();
            silver = new List<BannerQuery>();
            bronze = new List<BannerQuery>();

        }
    }
}