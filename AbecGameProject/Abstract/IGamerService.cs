using AbecGameProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbecGameProject.Abstract
{
    public interface IGamerService
    {
        bool Add(Gamer gamer);
        void Update(Gamer gamer);
        void Remove(long nationalityId);
        Gamer[] GetGamers();
    }
}
