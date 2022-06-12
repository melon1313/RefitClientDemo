using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefitClientDemo.Controllers.Parameters;
using RefitClientDemo.Repositories.Models.DataModels;
using RefitClientDemo.Repositories.Models.ViewModels;
using RefitClientDemo.Services.Interfaces;

namespace RefitClientDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase
    {
        private readonly IClubClient _clubClient;
        public ClubsController(IClubClient clubClient)
        {
            _clubClient = clubClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetClubs()
        {
            //注意: IClubClient 的資料應經過service層包裝處理後，給 Controller 使用
            var clubs = await _clubClient.GetClubs();

            //注意: 回傳值應轉換成 ViewModel 才能回傳
            return Ok(clubs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClubBy(int id)
        {
            var club = await _clubClient.GetClubBy(id);

            return Ok(club);
        }

        [HttpGet("{clubId:int}/users/{clubUserId}")]
        public async Task<IActionResult> GetClubUser([FromRoute] UserParameter userParameter)
        {
            var clubUser = await _clubClient.GetClubUser(
                new ClubUserDataModel { clubId = userParameter.clubId, clubUserId = userParameter.clubUserId});
        
            return Ok(clubUser);
        }

        [HttpGet("{clubId:int}/users")]
        public async Task<IActionResult> SearchClubsUsers([FromRoute] int clubId, [FromQuery] string keyword)
        {
            var clubUsers = await _clubClient.SearchClubUsers(clubId, keyword);

            return Ok(clubUsers);
        }

        [HttpGet("users")]
        public async Task<IActionResult> SearchClubsUsers([FromQuery] string keyword, [FromQuery] string sort)
        {
            var clubsUsers = await _clubClient.SearchClubUsers(new UserFilterDataModel { KeyWord = keyword, Sort = sort });
            
            return Ok(clubsUsers);
        }

        [HttpGet("users/searching")]
        public async Task<IActionResult> SearchClubsUsers([FromQuery] string[] keywords)
        {
            var clubUsers = await _clubClient.SearchClubUsers(keywords);

            return Ok(clubUsers);
        }
    }
}
