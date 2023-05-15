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
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(
            AddCharacterDto newCharacter
        )
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character); // 新增映射角色
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters
                .Select(c => _mapper.Map<GetCharacterDto>(c)) // 取得對應的角色欄位
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            var dbCharacter = await _context.Characters.ToListAsync();
            // 將檔案比對Dto後回傳
            serviceResponse.Data = dbCharacter
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(
            UpdateCharacterDto updatedCharacter
        )
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            // 當找不到角色id時的兩種處理方式：1. 使用try catch，2. 用if判斷是否找不到角色。
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                // 自訂錯誤訊息
                if (character is null)
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

                // _mapper.Map(updatedCharacter, character); // 使用映射法

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                // 自訂錯誤訊息
                if (character is null)
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                // serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Data = await _context.Characters
                    .Select(c => _mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
