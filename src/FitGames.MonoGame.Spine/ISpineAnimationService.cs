using Spine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGames.MonoGame.Spine
{
    public interface ISpineAnimationService
    {
        void Render(Skeleton skeleton);
    }
}
