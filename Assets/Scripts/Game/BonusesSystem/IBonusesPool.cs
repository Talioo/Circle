using System.Threading;
using System.Threading.Tasks;
using Game.BonusesSystem.Bonus;

namespace Game.BonusesSystem
{
    public interface IBonusesPool
    {
        Task<BonusController> GetBonus(BonusModel model, CancellationToken token);
    }
}