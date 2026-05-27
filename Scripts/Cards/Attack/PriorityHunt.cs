using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class PriorityHunt : CustomCardModel
{
    private const int energyCost = 2;
    private const CardType type = CardType.Attack;
    private const CardRarity rarity = CardRarity.Common;
    private const TargetType targetType = TargetType.AnyEnemy;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(24m, ValueProp.Move)];
    private const bool shouldShowInCardLibrary = true;
    public PriorityHunt() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    public override string PortraitPath => $"res://axeboy/images/cards/priority_hunt.png";
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<Creature> enemies = base.Owner.Creature.CombatState.HittableEnemies.ToList();
        Creature Target = null;
        int maxHp = -1;
        if(enemies.Count>1){
            for (int i = 0; i < enemies.Count; i++){
                Creature enemy = enemies[i];
                if (enemy.CurrentHp > maxHp){
                    maxHp = enemy.CurrentHp;
                    Target = enemy;
                }
            }
        }
        else
        {
            Target=cardPlay.Target;
        }

        await DamageCmd.Attack(DynamicVars.Damage.IntValue)
        .FromCard(this)
        .Targeting(Target)
        .Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(6);
    }
}