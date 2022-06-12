using Refit;
using RefitClientDemo.Repositories.Models.DataModels;
using RefitClientDemo.Repositories.Models.Dtos;
using RefitClientDemo.Repositories.Models.ViewModels;

namespace RefitClientDemo.Services.Interfaces
{
    public interface IClubClient
    {
        //----------- GET from Route -------------
        [Get("/api/RemoteClubs")]
        Task<IEnumerable<ClubDto>> GetClubs();

        [Get("/api/RemoteClubs/{id}")]
        Task<ClubDto> GetClubBy(int id);

        [Get("/api/RemoteClubs/{clubUser.clubId}/users/{clubUser.clubUserId}")]
        Task<ClubUserDto> GetClubUser(ClubUserDataModel clubUser);

        //---------- GET from query ---------------

        [Get("/api/RemoteClubs/{clubId}/users?keyWord={keyWord}")]
        Task<IEnumerable<ClubUserDto>> SearchClubUsers(int clubId, string keyWord);

        [Get("/api/RemoteClubs/users")]
        Task<IEnumerable<ClubUserDto>> SearchClubUsers(UserFilterDataModel userFilter);

        [Get("/api/RemoteClubs/users/searching")]
        Task<IEnumerable<ClubUserDto>> SearchClubUsers([Query(CollectionFormat.Multi)] string[] keywords);


    }
}
