using Microsoft.AspNetCore.Mvc;

namespace RefitClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteClubsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetClubs()
        {
            var clubs = GetFakeClubs();

            return Ok(clubs); 
        }

        [HttpGet("{clubId:int}")]
        public async Task<IActionResult> GetClubBy(int clubId)
        {
            var club = GetFakeClubs()
                .Where(club => club.RemoteClubId == clubId)
                .FirstOrDefault();
            
            return Ok(club);
        }

        [HttpGet("{clubId:int}/users/{userId}")]
        public async Task<IActionResult> GetClubsUser(int clubId, string userId)
        {
            var clubUser = GetFakeClubsUsers()
                .Where(users => users.RemoteClubId == clubId && users.RemoteClubUserId == userId)
                .FirstOrDefault();

            return Ok(clubUser);
        }

        [HttpGet("{clubId:int}/users")]
        public async Task<IActionResult> SearchClubsUsers([FromRoute] int clubId, [FromQuery] string keyWord)
        {
            var clubUsers = GetFakeClubsUsers()
                .Where(users => users.RemoteClubId == clubId && users.RemoteClubUserName.Contains(keyWord));

            return Ok(clubUsers);
        }

        [HttpGet("users")]
        public async Task<IActionResult> SearchClubsUsers([FromQuery] UserFilterParameter userFilter)
        {
            var clubUsers = GetFakeClubsUsers()
                .Where(users => users.RemoteClubUserName.Contains(userFilter.KeyWord));

            if (userFilter.Sort.Equals("descend")) return Ok(clubUsers.OrderByDescending(user => user.RemoteClubUserName));

            return Ok(clubUsers.OrderBy(user => user.RemoteClubUserName));
        }

        [HttpGet("users/searching")]
        public async Task<IActionResult> SearchClubsUsers([FromQuery] string[] keywords)
        {
            var clubsUsers = GetFakeClubsUsers()
                .Where(user => 
                {
                    var existingKeywords = keywords
                    .Where(keyword => user.RemoteClubUserName.ToUpper().Contains(keyword.ToUpper()));
                    
                    if (existingKeywords.Any()) return true;

                    return false;
                });

            return Ok(clubsUsers);
        }


        private IEnumerable<RemoteClubViewModel> GetFakeClubs()
        {
            var clubs = new List<RemoteClubViewModel>();

            clubs.Add(new RemoteClubViewModel { RemoteClubId = 1, RemoteClubName = "club01", RemoteClubDescription = "Description01" });
            clubs.Add(new RemoteClubViewModel { RemoteClubId = 2, RemoteClubName = "club02", RemoteClubDescription = "Description02" });
            clubs.Add(new RemoteClubViewModel { RemoteClubId = 3, RemoteClubName = "club03", RemoteClubDescription = "Description03" });
            clubs.Add(new RemoteClubViewModel { RemoteClubId = 4, RemoteClubName = "club04", RemoteClubDescription = "Description04" });
            clubs.Add(new RemoteClubViewModel { RemoteClubId = 5, RemoteClubName = "club05", RemoteClubDescription = "Description05" });

            return clubs; 
        }

        private IEnumerable<RemoteClubUserViewModel> GetFakeClubsUsers()
        {
            var clubsUsers = new List<RemoteClubUserViewModel>();

            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user01", RemoteClubId = 1, RemoteClubUserName = "Alice"});
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user02", RemoteClubId = 5, RemoteClubUserName = "Melon" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user03", RemoteClubId = 2, RemoteClubUserName = "Ken" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user04", RemoteClubId = 3, RemoteClubUserName = "Linda" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user05", RemoteClubId = 1, RemoteClubUserName = "Lucy" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user06", RemoteClubId = 2, RemoteClubUserName = "Amy" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user07", RemoteClubId = 3, RemoteClubUserName = "Lisa" });
            clubsUsers.Add(new RemoteClubUserViewModel { RemoteClubUserId = "user08", RemoteClubId = 3, RemoteClubUserName = "Wenny" });

            return clubsUsers;
        }
    }

    public class RemoteClubUserViewModel
    {
        public string RemoteClubUserId { get; set; }

        public int RemoteClubId { get; set; }

        public string RemoteClubUserName { get; set; }
    }

    public class RemoteClubViewModel
    {
        public int RemoteClubId { get; set; }

        public string RemoteClubName { get; set; }

        public string RemoteClubDescription { get; set; }
    }

    public class UserFilterParameter
    {
        public string KeyWord { get; set; } = String.Empty;
        public string Sort { get; set; } = "ascend";
    }
}
