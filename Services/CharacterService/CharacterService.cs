namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };
        public IMapper _mapper { get; }

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var servicesResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character); // 新增映射角色
            servicesResponse.Data = characters
                .Select(c => _mapper.Map<GetCharacterDto>(c)) // 取得對應的角色欄位
                .ToList();
            return servicesResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var servicesResponse = new ServiceResponse<List<GetCharacterDto>>();
            servicesResponse.Data = characters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToList();
            return servicesResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var servicesResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            servicesResponse.Data = _mapper.Map<GetCharacterDto>(character);
            // if (character is not null)
            //     return character;
            // throw new Exception("Character no found");
            return servicesResponse;
        }
    }
}
