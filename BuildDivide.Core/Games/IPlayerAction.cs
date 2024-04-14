using System.Threading.Tasks;

namespace BuildDivide.Core.Games
{
    public interface IPlayerAction
	{
		bool IsAvailible();
		Task DoActionAsync();
	}
}