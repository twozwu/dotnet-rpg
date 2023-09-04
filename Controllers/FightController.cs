using dotnet_rpg.Dtos.Fight;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;

        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(
            WeaponAttackDto request
        )
        {
            return Ok(await _fightService.WeaponAttack(request));
        }

        [HttpPost("Skill")] // (/post/Skill)
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttack(
            SkillAttackDto request
        )
        {
            return Ok(await _fightService.SkillAttack(request));
        }

        /// <summary>
        /// 戰鬥
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>post預設方法</remarks>
        /// <returns></returns>
        // 如果沒有加路徑即為預設 (/Fight)
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(
            FightRequestDto request
        )
        {
            return Ok(await _fightService.Fight(request));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<HighScoreDto>>>> GetHighScore()
        {
            return Ok(await _fightService.GetHighScore());
        }
    }
}
