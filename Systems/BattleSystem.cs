using RPG_Character_System.Characters;
using RPG_Character_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Character_System.Systems
{
    public class BattleSystem : IBattleStart
    {
        private readonly TakeDamageService _takeDamageService;
        private readonly PhysicalAttackService _physicalAttackService;
        private readonly MagicAttackService _magicAttackService;

        public event EventHandler<Character> OnBattleEnded;
        public event DamageHandler OnDamageDealt;

        public BattleSystem(TakeDamageService takeDamageService)
        {
            _takeDamageService = takeDamageService;
            _physicalAttackService = new PhysicalAttackService();
            _magicAttackService = new MagicAttackService();
        }

        public void StartBattle(Character c1, Character c2)
        {
            c1.OnCharacterDied += (s, e) => OnBattleEnded?.Invoke(this, c1);
            c2.OnCharacterDied += (s, e) => OnBattleEnded?.Invoke(this, c2);

            bool turn = true;

            while (c1.Health > 0 && c2.Health > 0)
            {
                if (turn)
                    ExecuteTurn(c1, c2);
                else
                    ExecuteTurn(c2, c1);

                turn = !turn;
            }
        }

        private void ExecuteTurn(Character attacker, Character target)
        {
            int beforeHp = target.Health;

            switch (attacker.ClassType)
            {
                case CharacterClassType.Warrior:
                    _physicalAttackService.ExecuteAttack(attacker, target, _takeDamageService);
                    break;

                case CharacterClassType.Mage:
                    _magicAttackService.ExecuteMagicAttack(attacker, target, _takeDamageService);
                    break;
            }

            int damage = beforeHp - target.Health;
            OnDamageDealt?.Invoke(attacker, target, damage);
        }
    }
}
