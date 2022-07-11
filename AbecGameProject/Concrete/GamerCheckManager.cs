using AbecGameProject.Abstract;
using AbecGameProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbecGameProject.Concrete
{
    public class GamerCheckManager : IGamerCheckService
    {
        // Uygulama Mernis'e bağlı kalmadan çalışabilsin diye oluşturulmuştur.
        public bool CheckIfRealGamer(Gamer gamer)
        {
            return true;
        }
    }
}
