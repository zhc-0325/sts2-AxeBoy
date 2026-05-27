using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class SoulListen : CustomCardModel
{
    private const int energyCost = 1;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Common;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/soul_listen.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<VisibilityPower>()
    };

    public SoulListen() : base(energyCost, type, rarity, TargetType.AnyEnemy, shouldShowInCardLibrary)
    {
    }
     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(6m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {   
        int VisibilityPower=3;
        if(cardPlay.Target.GetPower<ResentfulSoulsPower>()!=null){
            var ResentfulSoulsPower = cardPlay.Target.GetPower<ResentfulSoulsPower>().Amount;
            while (ResentfulSoulsPower >= DynamicVars["ResentfulSoulsPower"].BaseValue)
                {
                    ResentfulSoulsPower-=DynamicVars["ResentfulSoulsPower"].IntValue;
                    VisibilityPower+=1;
                }
           
        }
        await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,VisibilityPower,base.Owner.Creature,this);
        await CreatureCmd.Stun(cardPlay.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ResentfulSoulsPower"].UpgradeValueBy(-1);
    }
}