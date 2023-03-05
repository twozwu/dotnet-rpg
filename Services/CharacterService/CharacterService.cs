namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Name = "Sam" }
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
        {
            var servicesResponse = new ServiceResponse<List<Character>>();
            characters.Add(newCharacter);
            servicesResponse.Data = characters;
            return servicesResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var servicesResponse = new ServiceResponse<List<Character>>();
            servicesResponse.Data = characters;
            return servicesResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var servicesResponse = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            servicesResponse.Data = character;
            // if (character is not null)
            //     return character;
            // throw new Exception("Character no found");
            return servicesResponse;
        }
    }
}
