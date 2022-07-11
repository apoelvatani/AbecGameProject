using AbecGameProject.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AbecGameProject.Abstract
{
    public abstract class BaseGamerManager : IGamerService
    {
        Gamer[] _gamers;
        Gamer[] _tempArray;

        public BaseGamerManager()
        {
            _gamers = new Gamer[0];
        }

        public virtual bool Add(Gamer gamer)
        {
            _tempArray = _gamers;
            _gamers = new Gamer[_gamers.Length + 1];
            for (int i = 0; i < _tempArray.Length; i++)
            {
                _gamers[i] = _tempArray[i];
            }
            _gamers[_gamers.Length - 1] = gamer;
            return true;
        }

        public void Remove(long nationalityId)
        {
            _tempArray = _gamers;
            _gamers = new Gamer[_gamers.Length - 1];
            for (int i = 0; i < _gamers.Length; i++)
            {
                if (_tempArray[i].NatioanlityId == nationalityId)
                {
                    for (int j = i; j < _gamers.Length; j++)
                    {
                        _gamers[j] = _tempArray[j + 1];
                    }
                    break;
                }
                _gamers[i] = _tempArray[i];
            }
        }

        public void Update(Gamer gamer)
        {
            for (int i = 0; i < _gamers.Length; i++)
            {
                if (_gamers[i].NatioanlityId == gamer.NatioanlityId)
                {
                    _gamers[i] = gamer;
                    break;
                }
            }
        }

        public Gamer[] GetGamers()
        {
            return _gamers;
        }
    }
}
