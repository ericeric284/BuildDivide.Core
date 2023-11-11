using BuildDivide.Core.Cards;
using BuildDivide.Core.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildDivide.Core.Windows
{
    /// <summary>
    /// 1100 プレイウィンドウ
    /// </summary>
    public class PlayWindow
    {
        private readonly GameCore gameCore;

        public PlayWindow(GameCore gameCore)
        {
            this.gameCore = gameCore;
        }

        public async Task ResolveAsync()
        {
            //1103-2
            var player = gameCore.TurnPlayer;

            //1103-3 TODO handle 1300

            //1103-5 TODO 1104
_1103_5:
            //1103-6 TODO 1300

            //1103-7
            var actionType = await player.ResolvePlayWindowActionAsync(this);
            var passed = false;
            switch (actionType)
            {
                case PlayWindowActionType.Pass:
                    passed = true;
                    break;
                case PlayWindowActionType.PlaceEnergy:
                    PlaceEnergy();
                    break;
                case PlayWindowActionType.PlayQuick:
                    PlayQuick();
                    break;
                case PlayWindowActionType.PlayQuickActivationAbitliy:
                    PlayQuickActivationAbitliy();
                    break;
                case PlayWindowActionType.PlayNormal:
                    PlayNormal();
                    break;
                case PlayWindowActionType.PlayNormalActivationAbility:
                    PlayNormalActivationAbility();
                    break;
            }

            
            if (passed && player == gameCore.TurnPlayer)
            {
                //1103-9
                player = gameCore.NonTurnPlayer;
                goto _1103_5;
            }
            else
            {
                //1103-8
                //1103-8a TODO
                
                //1103-8b TODO

                //1103-8c
                return;
            }
        }

        public bool CanPlayQuick()
        {
            throw new NotImplementedException();
        }

        public void PlayQuick()
        {
            throw new NotImplementedException();
        }

        public bool CanPlayQuickActivationAbitliy()
        {
            throw new NotImplementedException();
        }

        public void PlayQuickActivationAbitliy()
        {
            throw new NotImplementedException();
        }

        public bool CanPlaceEnergy()
        {
            throw new NotImplementedException();
        }
        public void PlaceEnergy()
        {
            throw new NotImplementedException();
        }

        public bool CanPlayNormal()
        {
            throw new NotImplementedException();
        }
        public void PlayNormal()
        {
            throw new NotImplementedException();
        }

        public bool CanPlayNormalActivationAbility()
        {
            throw new NotImplementedException();
        }
        public void PlayNormalActivationAbility()
        {
            throw new NotImplementedException();
        }
    }

    public enum PlayWindowActionType
    {
        Pass,
        PlayQuick,
        PlayQuickActivationAbitliy,
        PlaceEnergy,
        PlayNormal,
        PlayNormalActivationAbility,
    }

    public class DamageWindow
    {

    }
}
