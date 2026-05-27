using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using System.Collections;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace AxeBoy.Scripts.Powers;

public class ObsessionPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径（需要添加图片）
    public override string? CustomPackedIconPath => "res://axeboy/images/powers/obsession_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/decay_realmAura_power.png";
    public override Task BeforeCardPlayed(CardPlay cardPlay)
	{
		if (cardPlay.Card.Owner.Creature != base.Owner)
		{
			return Task.CompletedTask;
		}
		if (cardPlay.Card.Type != CardType.Skill)
		{
			return Task.CompletedTask;
		}
        for(int i=1;i<=Amount;i++){
		    CardPileCmd.Draw(null, 1, base.Owner.Player);
        }
        
		return Task.CompletedTask;
	}
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (Owner?.Side != side)
        {
            return;
        }
        await PowerCmd.Remove(this);
    }

}