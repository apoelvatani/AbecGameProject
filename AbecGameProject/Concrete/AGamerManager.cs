using AbecGameProject.Abstract;
using AbecGameProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AbecGameProject.Concrete
{
    public class AGamerManager : BaseGamerManager
    {
        IGamerCheckService _gamerCheckService;
        public AGamerManager(IGamerCheckService gamerCheckService) // Dependies injection
        {
            _gamerCheckService = gamerCheckService;
        }

        public override bool Add(Gamer gamer)
        {
            if(_gamerCheckService.CheckIfRealGamer(gamer))
            {
                base.Add(gamer);
                return true;
            }
            else
            {
                MessageBox.Show("Not a valid Gamer...!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        
    }
}
