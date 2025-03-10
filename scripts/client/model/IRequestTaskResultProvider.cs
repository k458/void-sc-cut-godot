using System.Collections.Generic;
using voidsccut.scripts.shared.serverTypes;
using voidsccut.scripts.shared.serverTypes.characters;
using voidsccut.scripts.shared.serverTypes.enemies;
using voidsccut.scripts.shared.serverTypes.items;
using voidsccut.scripts.shared.serverTypes.progression;

namespace voidsccut.scripts.client.model;

public interface IRequestTaskResultProvider
{
    CharactersDto ObtainCharactersDto();
    EnemiesDto ObtainEnemiesDto();
    ItemsDto ObtainItemsDto();
    ProgressionDto ObtainProgressionDto();
    List<UserEntity> ObtainUsers();
    TokenTime ObtainTokenTime();
}