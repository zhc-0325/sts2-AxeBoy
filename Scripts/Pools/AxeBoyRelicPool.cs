using BaseLib.Abstracts;

public class AxeBoyRelicPool : CustomRelicPoolModel
{
    // 描述中使用的能量图标。大小为24x24。
    public override string? TextEnergyIconPath => "res://axeboy/images/energy_axeboy.png";
    // tooltip和卡牌左上角的能量图标。大小为74x74。
    public override string? BigEnergyIconPath => "res://axeboy/images/energy_axeboy_big.png";
}