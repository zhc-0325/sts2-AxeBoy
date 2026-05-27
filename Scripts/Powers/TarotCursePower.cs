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
using static BaseLib.Utils.BetaMainCompatibility;

namespace AxeBoy.Scripts.Powers;

public class TarotCursePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override string? CustomPackedIconPath => "res://axeboy/images/powers/tarot_curse_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/tarot_curse_power.png";
    public override async Task AfterCardPlayed(PlayerChoiceContext choicecontext,CardPlay cardPlay)
	{
        if (cardPlay.Card != null&&cardPlay.Card.DynamicVars.ContainsKey("ResentfulSoulsPower"))
        {
            if (cardPlay.Card.DynamicVars["ResentfulSoulsPower"]!= null)
            {
               await CardPileCmd.Draw(null, base.Amount, base.Owner.Player);
            }
            else
            {
                return;
            }
        }
    }
}