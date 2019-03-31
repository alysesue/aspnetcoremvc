using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeTravelMVC.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace GrandeTravelMVC.Services
{
    public static class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

            DataService<ProviderProfile> providerProfileDataService = new DataService<ProviderProfile>();
            DataService<CustomerProfile> customerProfileDataService = new DataService<CustomerProfile>();

            DataService<Location> locationDataService = new DataService<Location>();
            DataService<Package> packageDataService = new DataService<Package>();

            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                if (await roleManager.FindByNameAsync("Customer") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (await roleManager.FindByNameAsync("Provider") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Provider"));
                }

                if (await userManager.FindByNameAsync("admin@admin.com") == null)
                {
                    IdentityUser admin = new IdentityUser("admin@admin.com");
                    await userManager.CreateAsync(admin, "somePassword");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

                if (await userManager.FindByNameAsync("provider@provider.com") == null)
                {
                    IdentityUser identity = new IdentityUser("provider@provider.com");
                    await userManager.CreateAsync(identity, "somePassword");
                    await userManager.AddToRoleAsync(identity, "Provider");

                    ProviderProfile profile = new ProviderProfile
                    {
                        CompanyLogo = "profilepicgen.png",
                        UserId = identity.Id
                    };
                    providerProfileDataService.Create(profile);

                    //Package package = new Package
                    //{
                    //    PackageId = 1,
                    //    PackageName = "",
                    //    Date = DateTime.Now,
                    //    Quantity = 1,
                    //    TotalPrice = "please upload",
                    //    UserId = identity.Id
                    //};
                    //packageDataService.Create(package);
                }

                if (await userManager.FindByNameAsync("customer@customer.com") == null)
                {
                    IdentityUser identity = new IdentityUser("customer@customer.com");
                    await userManager.CreateAsync(identity, "somePassword");
                    await userManager.AddToRoleAsync(identity, "Customer");

                    CustomerProfile profile = new CustomerProfile
                    {
                        Picture = "profilepicgen.png",
                        UserId = identity.Id
                    };
                    customerProfileDataService.Create(profile);

                    //Order order = new Order
                    //{
                    //    PackageId = 1,
                    //    PackageName = "",
                    //    Date = DateTime.Now,
                    //    Quantity = 1,
                    //    TotalPrice = "please upload",
                    //    UserId = identity.Id
                    //};
                    //orderDataService.Create(order);
                }
            }


            //add sample locations
            Location location1 = new Location();

            location1.Name = "Sydney";
            location1.Description = "Sydney’s buzzing CBD, beautiful Harbourside precincts and beachside suburbs " +
            "offer guests plenty of ways to experience luxury, from world-class dining to opulent accommodation " +
            "and specialised tours. Enjoy the lavish and exclusive side to the harbour city while discovering " +
            "Sydney’s best-kept local secrets and hidden sights. Experience the beauty of the harbour city " +
            "with an aerial tour of Sydney, enjoy dining experiences prepared by celebrated chefs, " +
            "step behind the scenes of the Sydney Opera House or choose from five hotels with " +
            "stunning views of the world-famous harbour to boutique stays in some of the city’s most vibrant suburbs.";
            location1.Picture = "location-sydney.jpg";

            IEnumerable<Package> packageList1 = new List<Package>
            {
                new Package
                {
                    Name = "Adventure Sydney",
                    Location = "Sydney",
                    Price = 1200,
                    Description = "Experience the majesty of Sydney Harbour and the coast from the air. " +
                    "Sydney Seaplanes flies over the city’s world-famous attractions, landing on waterways " +
                    "next to acclaimed restaurants. For winery tours, Heron Airlines takes you to the Hunter Valley " +
                    "and Mudgee. And escape to an island paradise with Air Adventure Australia for five days on the " +
                    "secluded Lord Howe Island.",
                    Picture = "adventure-sydney.jpg",
                    IsAvailable = true,
                    LocationId = location1.LocationId
                },
                new Package
                {
                    Name = "Cultural Sydney",
                    Location = "Sydney",
                    Price = 1500,
                    Description = "Enjoy a tour tailored to your interests with Just Sydney or discover the city’s icons" +
                    " and more on a customised outing with the likes of AEA Luxury Tours, Ultimately Sydney and inART Sydney. " +
                    "Uncover Sydney’s fashion secrets with a chauffeured shopping tour from Chic in the City, " +
                    "with tour-exclusive discounts, complimentary champagne and your own personal stylist.",
                    Picture = "cultural-sydney.jpg",
                    IsAvailable = true,
                    LocationId = location1.LocationId
                },
                new Package
                {
                    Name = "Romantic Sydney",
                    Location = "Sydney",
                    Price = 1100,
                    Description = "Australia’s oldest wine region produces some of the world’s best wines. " +
                    "The Hunter Valley offers you a taste of the finer things of life, from exquisite wines " +
                    "and acclaimed restaurants such as Muse and Bistro Molines to golf courses and day spas. " +
                    "Elegant resorts include Château Élan and Crowne Plaza, and you can marvel at a glorious sunrise " +
                    "in a hot-air balloon with Balloon Aloft.",
                    Picture = "romantic-sydney.jpg",
                    IsAvailable = true,
                    LocationId = location1.LocationId
                },
                new Package
                {
                    Name = "Gourmet Sydney",
                    Location = "Sydney",
                    Price = 1800,
                    Description = "Sydney’s enviable reputation as a world-class dining destination is built on the " +
                    "talents of chefs using the freshest local produce and seafood caught in clean waters. Delight " +
                    "your senses by dining at Sydney’s best restaurants including Quay and Sepia, which are among the top" +
                    " 50 restaurants in the world. You’ll find plenty of quality restaurants with superb waterfront views, too.",
                    Picture = "gourmet-sydney.jpg",
                    IsAvailable = true,
                    LocationId = location1.LocationId
                },
            };

            location1.Packages = packageList1;

            locationDataService.Create(location1);



            Location location2 = new Location();

            location2.Name = "Melbourne";
            location2.Description = "Experience all Melbourne, the 'world's most liveable city' has to offer: " +
            "from culture and laneways to restaurants, shopping and the live music scene. " +
            "But it's not all limited into the big smoke with wineries, the Great Ocean Road, " +
            "nature parks and ski fields all within 90 minutes of the CBD. Melbourne, " +
            "‘voted the most liveable city in the world’, is an ever-changing, exciting city " +
            "with a thriving bar, restaurant and live music scene. Visitors love checking out Melbourne’s " +
            "hidden laneway cafes and bars, which can be found in the most unlikely of locations – " +
            "on roof tops, in basements, and even shipping containers.";
            location2.Picture = "location-melbourne.jpg";

            IEnumerable<Package> packageList2 = new List<Package>
            {
                new Package
                {
                    Name = "Adventure Melbourne",
                    Location = "Melbourne",
                    Price = 1200,
                    Description = "Experience the majesty of Sydney Harbour and the coast from the air. " +
                    "Sydney Seaplanes flies over the city’s world-famous attractions, landing on waterways " +
                    "next to acclaimed restaurants. For winery tours, Heron Airlines takes you to the Hunter Valley " +
                    "and Mudgee. And escape to an island paradise with Air Adventure Australia for five days on the " +
                    "secluded Lord Howe Island.",
                    Picture = "adventure-melbourne.jpg",
                    IsAvailable = true,
                    LocationId = location2.LocationId
                },
                new Package
                {
                    Name = "Cultural Melbourne",
                    Location = "Melbourne",
                    Price = 1500,
                    Description = "Enjoy a tour tailored to your interests with Just Sydney or discover the city’s icons" +
                    " and more on a customised outing with the likes of AEA Luxury Tours, Ultimately Sydney and inART Sydney. " +
                    "Uncover Sydney’s fashion secrets with a chauffeured shopping tour from Chic in the City, " +
                    "with tour-exclusive discounts, complimentary champagne and your own personal stylist.",
                    Picture = "cultural-melbourne.jpg",
                    IsAvailable = true,
                    LocationId = location2.LocationId
                },
                new Package
                {
                    Name = "Romantic Melbourne",
                    Location = "Melbourne",
                    Price = 1100,
                    Description = "Australia’s oldest wine region produces some of the world’s best wines. " +
                    "The Hunter Valley offers you a taste of the finer things of life, from exquisite wines " +
                    "and acclaimed restaurants such as Muse and Bistro Molines to golf courses and day spas. " +
                    "Elegant resorts include Château Élan and Crowne Plaza, and you can marvel at a glorious sunrise " +
                    "in a hot-air balloon with Balloon Aloft.",
                    Picture = "romantic-melbourne.jpg",
                    IsAvailable = true,
                    LocationId = location2.LocationId
                },
                new Package
                {
                    Name = "Gourmet Melbourne",
                    Location = "Melbourne",
                    Price = 1800,
                    Description = "Sydney’s enviable reputation as a world-class dining destination is built on the " +
                    "talents of chefs using the freshest local produce and seafood caught in clean waters. Delight " +
                    "your senses by dining at Sydney’s best restaurants including Quay and Sepia, which are among the top" +
                    " 50 restaurants in the world. You’ll find plenty of quality restaurants with superb waterfront views, too.",
                    Picture = "gourmet-melbourne.jpg",
                    IsAvailable = true,
                    LocationId = location2.LocationId
                },
            };

            location2.Packages = packageList2;

            locationDataService.Create(location2);


            Location location3 = new Location();

            location3.Name = "Gold Coast";
            location3.Description = "Experience all Melbourne, the 'world's most liveable city' has to offer: " +
            "from culture and laneways to restaurants, shopping and the live music scene. " +
            "But it's not all limited into the big smoke with wineries, the Great Ocean Road, " +
            "nature parks and ski fields all within 90 minutes of the CBD. Melbourne, " +
            "‘voted the most liveable city in the world’, is an ever-changing, exciting city " +
            "with a thriving bar, restaurant and live music scene. Visitors love checking out Melbourne’s " +
            "hidden laneway cafes and bars, which can be found in the most unlikely of locations – " +
            "on roof tops, in basements, and even shipping containers.";
            location3.Picture = "location-goldcoast.jpg";

            IEnumerable<Package> packageList3 = new List<Package>
            {
                new Package
                {
                    Name = "Adventure Gold Coast",
                    Location = "Gold Coast",
                    Price = 1200,
                    Description = "Experience the majesty of Sydney Harbour and the coast from the air. " +
                    "Sydney Seaplanes flies over the city’s world-famous attractions, landing on waterways " +
                    "next to acclaimed restaurants. For winery tours, Heron Airlines takes you to the Hunter Valley " +
                    "and Mudgee. And escape to an island paradise with Air Adventure Australia for five days on the " +
                    "secluded Lord Howe Island.",
                    Picture = "adventure-goldcoast.jpg",
                    IsAvailable = true,
                    LocationId = location3.LocationId
                },
                new Package
                {
                    Name = "Cultural Gold Coast",
                    Location = "Gold Coast",
                    Price = 1500,
                    Description = "There’s a new kind of wave in town, one splashed in colour and creativity, sewn with strings of a different sound, bubbling with vision, passion and a sprinkle of tradition. Our cultural current is carving out more street cred than ever before; teeming with talent, creative minds and entrepreneurial souls, our spirit beats to the sound of its own drum. Buzzing festivals and performing arts, live music jams and contemporary art fill our social calendars. We decipher fine art frescos in carefully curated showcases and ponder the experimental in reinvented spaces.",
                    Picture = "cultural-goldcoast.jpg",
                    IsAvailable = true,
                    LocationId = location3.LocationId
                },
                new Package
                {
                    Name = "Romantic Gold Coast",
                    Location = "Gold Coast",
                    Price = 1100,
                    Description = "A vibrant destination that’s renowned for excitement, Surfers Paradise Marriott Resort and Spa offers a luxury escape where discerning travellers can relax, indulge and discover, whilst making memories that will last a lifetime. Featuring sparkling lagoons, white sandy beaches, cascading waterfalls, quiet pool zones and unique aquatic experiences. Indoors there are quiet spaces to hideaway, unique and individual restaurants to suit every taste and spaces to work and play. Enjoy lush amenities in elegantly appointed guest rooms, and then head out for an indulgent bite of the freshest cuisine at Misono Japanese Steakhouse, Citrique Seafood Restaurant or Chapter and Verse bar and lounge.",
                    Picture = "romantic-goldcoast.jpg",
                    IsAvailable = true,
                    LocationId = location3.LocationId
                },
                new Package
                {
                    Name = "Gourmet Gold Coast",
                    Location = "Gold Coast",
                    Price = 1800,
                    Description = "The Coast is a place where night bites, hatted restaurants, street-eats and breweries are all served with a side of alfresco. A place where five-star feasting means dusting the sand from your feet; where surfers swap their boards for a latte at sun-up and little ones break from beachside frolics to feast on epic spreads of fish and chips. From Burleigh’s perennially popular rooftop bars to communal eateries in Palmy, fine dining at Broadbeach to reinvented venues in Surfers, there is a renaissance stirring up the Gold Coast culinary scene.",
                    Picture = "gourmet-goldcoast.jpg",
                    IsAvailable = true,
                    LocationId = location3.LocationId
                },
            };

            location3.Packages = packageList3;

            locationDataService.Create(location3);


            Location location4 = new Location();

            location4.Name = "Perth";
            location4.Description = "Sun-drenched and laidback, Perth is simply a joy to be in. However, the West Australian capital does have tourist attractions, such as the Colonial-era mint, important in the country’s gold industry. East is the WACA, a must-see for cricket fans, and the city is also mad for Aussie Rules football. The city centre has the historic Government House and Supreme Court, both dwarfed by the ultra - modern skyscrapers a few streets behind.The best skyline view is from leafy King’s Park, near the War Memorial overlooking the Swan River below, named after its black swans. Across the river in Perth Zoo are more native animals including koalas, crocodiles and free-hopping kangaroos. Boats to the zoo leave from the Barrack Street jetty by a sail - shaped bell tower. Other day trips from Perth include Rottnest Island and Fremantle, down the river in the opposite direction from Perth Airport. The apartments, B&Bs and hotels in Perth are spread all over this fantastic city, interspersed with restaurants serving specialities such as western rock lobster and chilli mussels.There is also a wealth of bars, many playing the indie music that the city is known for. Local brews from the Swan Brewery and wines from the Swan Valley vineyards round off the evening.";
            location4.Picture = "location-perth.jpg";

            IEnumerable<Package> packageList4 = new List<Package>
            {
                new Package
                {
                    Name = "Adventure Perth",
                    Location = "Perth",
                    Price = 1200,
                    Description = "Long live the Kings! This isn’t your average patch of inner-city parkland. It’s a prodigious expanse of gardens that showcases Perth’s funky flora – vivid banksias, fragrant acacias and even a majestically hoary boab tree. The Beedawong Cultural Space is worth a visit too, where you’ll learn the spiritual customs and practices of the Nyoongar of Western Australia.",
                    Picture = "adventure-perth.jpg",
                    IsAvailable = true,
                    LocationId = location4.LocationId
                },
                new Package
                {
                    Name = "Cultural Perth",
                    Location = "Perth",
                    Price = 1500,
                    Description = "Think less state of the art, more art of the state. This museum brings together a smorgasbord of Western Australian artworks, from Frederick McCubbin’s “Down on His Luck” to the minimalist abstractions of Aborigine artist Rover Thomas. Add in a smattering of 20th-century British paintings and a revolving carousel of excellent exhibitions and you’ve got yourself one fine gallery!",
                    Picture = "cultural-perth.jpg",
                    IsAvailable = true,
                    LocationId = location4.LocationId
                },
                new Package
                {
                    Name = "Romantic Perth",
                    Location = "Perth",
                    Price = 1100,
                    Description = "Once you visit Cottesloe, you’ll see why Aussies and beaches go together like sandals and Speedos. Fine sand, clear waters and a whole lot of sunshine is a winning combo on Perth’s most beloved beach! Fuel up on tea and scones from Indian Tea House or pick up a posh picnic from the Boatshed Market. Then plonk yourself on the sand and watch those endorphins go through the roof!",
                    Picture = "romantic-perth.jpg",
                    IsAvailable = true,
                    LocationId = location4.LocationId
                },
                new Package
                {
                    Name = "Gourmet Perth",
                    Location = "Perth",
                    Price = 1800,
                    Description = "Perth has some serious game when it comes to food, playing host to some of the best restaurants and cafes in the country. If you’re looking for a few hours of fun on the weekend, meeting new people, and discovering new things about your city, Foodi tours will show you where to start. You’ll be able to see the city from an intimate, local perspective while also learning about the history of the area. You’ll get to walk through the historical district of London Court which replicates walking through the streets of London, complete with a walk past the Big Ben clock face.",
                    Picture = "gourmet-perth.jpg",
                    IsAvailable = true,
                    LocationId = location4.LocationId
                },
            };

            location4.Packages = packageList4;

            locationDataService.Create(location4);
        }
    }
}
