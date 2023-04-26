namespace EntityFramework7Relationships.Dtos
{
    public record struct CharacterCreateDto(
        string Name, 
        BackpackCreateDto Backpack, 
        List<WeaponCreateDto> Weapons, 
        List<FactionCreateDto> Factions
        );
   
}
