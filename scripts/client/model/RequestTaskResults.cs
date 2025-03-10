using System.Collections.Generic;
using voidsccut.scripts.shared.serverTypes;
using voidsccut.scripts.shared.serverTypes.characters;
using voidsccut.scripts.shared.serverTypes.enemies;
using voidsccut.scripts.shared.serverTypes.items;
using voidsccut.scripts.shared.serverTypes.progression;

namespace voidsccut.scripts.client.model;

public class RequestTaskResults : IRequestTaskResultAggregator, IRequestTaskResultProvider
{
    private CharactersDto _charactersDto;
    private EnemiesDto _enemiesDto;
    private ItemsDto _itemsDto;
    private ProgressionDto _progressionDto;
    private List<UserEntity> _users;
    private TokenTime _tokenTime;
    
    public void SetCharactersDto(CharactersDto dto) => _charactersDto = dto;
    public void SetProgressionDto(ProgressionDto dto) => _progressionDto = dto;
    public void SetEnemiesDto(EnemiesDto dto) => _enemiesDto = dto;
    public void SetItemsDto(ItemsDto dto) => _itemsDto = dto;
    public void SetUsers(List<UserEntity> users) => _users = users;
    public void SetTokenTime(TokenTime tt) => _tokenTime = tt;


    public CharactersDto ObtainCharactersDto()
    {
        var ret = _charactersDto;
        _charactersDto = null;
        return ret;
    }
    public EnemiesDto ObtainEnemiesDto()
    {
        var ret = _enemiesDto;
        _enemiesDto = null;
        return ret;
    }
    public ItemsDto ObtainItemsDto()
    {
        var ret = _itemsDto;
        _itemsDto = null;
        return ret;
    }
    public ProgressionDto ObtainProgressionDto()
    {
        var ret = _progressionDto;
        _progressionDto = null;
        return ret;
    }
    public List<UserEntity> ObtainUsers()
    {
        var ret = _users;
        _users = null;
        return ret;
    }

    public TokenTime ObtainTokenTime()
    {
        var ret = _tokenTime;
        _tokenTime = null;
        return ret;
    }
}