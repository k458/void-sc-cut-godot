using System.Collections.Generic;
using voidsccut.scripts.shared.serverTypes;
using voidsccut.scripts.shared.serverTypes.characters;
using voidsccut.scripts.shared.serverTypes.enemies;
using voidsccut.scripts.shared.serverTypes.items;
using voidsccut.scripts.shared.serverTypes.progression;

namespace voidsccut.scripts.client.model;

public interface IRequestTaskResultAggregator
{
    void SetCharactersDto(CharactersDto dto);
    void SetProgressionDto(ProgressionDto dto);
    void SetEnemiesDto(EnemiesDto dto);
    void SetItemsDto(ItemsDto dto);
    void SetUsers(List<UserEntity> users);
    void SetTokenTime(TokenTime tt);
}