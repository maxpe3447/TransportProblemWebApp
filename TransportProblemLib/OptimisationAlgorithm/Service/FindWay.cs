using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportProblemLib.OptimisationAlgorithm.Service
{
    internal class FindWay
    {
        private Point _root;
        private Point _begining;
        private FindWay _father;
        private FindWay[] _childrens;
        private Point[] _mAllowed;
        private bool _flag;
        public FindWay(int x, int y, bool flag, Point[] mAllowed, Point Beg, FindWay Father)
        {
            _begining = Beg;
            _flag = flag;
            _root = new Point(x, y);
            _mAllowed = mAllowed;
            _father = Father;
        }
        public bool BuildTree()
        {
            Point[] ps = new Point[_mAllowed.Length];
            int Count = 0;
            for (int i = 0; i < _mAllowed.Length; i++)
            {
                if (_flag)
                {
                    if (_root.Y == _mAllowed[i].Y)
                    {
                        Count++;
                        ps[Count - 1] = _mAllowed[i];
                    }
                }
                else
                  if (_root.X == _mAllowed[i].X)
                {
                    Count++;
                    ps[Count - 1] = _mAllowed[i];
                }
            }
            FindWay fwu = this;
            _childrens = new FindWay[Count];
            int k = 0;
            for (int i = 0; i < Count; i++)
            {
                if (ps[i] == _root)
                {
                    continue;
                }
                if (ps[i] == _begining)
                {
                    while (fwu != null)
                    {
                        _mAllowed[k] = fwu._root;
                        fwu = fwu._father;
                        k++;
                    };
                    for (; k < _mAllowed.Length; k++)
                    {
                        _mAllowed[k] = new Point(-1, -1);
                    }
                    return true;
                }

                if (!Array.TrueForAll(ps, p => (p.X == 0 && p.Y == 0)))
                {
                    _childrens[i] = new FindWay(ps[i].X, ps[i].Y, !_flag, _mAllowed, _begining, this);
                    bool result = _childrens[i].BuildTree();
                    if (result)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
