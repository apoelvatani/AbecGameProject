using AbecGameProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbecGameProject.Abstract
{
    public interface IGamerCheckService
    {
        bool CheckIfRealGamer(Gamer gamer);
    }
}
